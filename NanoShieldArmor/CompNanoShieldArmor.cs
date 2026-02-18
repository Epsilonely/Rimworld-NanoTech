using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace NanoShieldArmor
{
    [StaticConstructorOnStartup]
    public class CompNanoShieldArmor : ThingComp
    {
        protected float energy;
        protected int ticksToReset = -1;
        private int lastAbsorbDamageTick = -9999;
        private Vector3 impactAngleVect;
        private bool broken;

        public float Energy => energy;
        public float GetEnergy() => energy;
        public void SetEnergy(float value) => energy = value;
        public bool IsBroken => broken;
        public void SetBroken(bool value) => broken = value;
        public int GetTicksToReset() => ticksToReset;
        public void SetTicksToReset(int value) => ticksToReset = value;
        public float EnergyMax => parent.GetStatValue(StatDefOf.EnergyShieldEnergyMax);

        public CompProperties_NanoShieldArmor Props => (CompProperties_NanoShieldArmor)props;

        private static readonly Material BubbleMat = MaterialPool.MatFrom("Other/ShieldBubble", ShaderDatabase.Transparent);

        private bool ShouldDisplay
        {
            get
            {
                Pawn wearer = (parent as Apparel)?.Wearer;
                return wearer?.Spawned == true && !wearer.Dead && !wearer.Downed;
            }
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            if (!respawningAfterLoad || Scribe.mode == LoadSaveMode.Inactive)
            {
                energy = EnergyMax;
            }
        }

        public override IEnumerable<Gizmo> CompGetWornGizmosExtra()
        {
            foreach (Gizmo item in base.CompGetWornGizmosExtra())
            {
                yield return item;
            }

            if (IsApparel)
            {
                foreach (Gizmo gizmo in GetGizmos())
                {
                    yield return gizmo;
                }
            }

            if (!DebugSettings.ShowDevGizmos)
            {
                yield break;
            }

            Command_Action command_Action = new Command_Action();
            command_Action.defaultLabel = "DEV: Break";
            command_Action.action = Break;
            yield return command_Action;
            if (ticksToReset > 0)
            {
                Command_Action command_Action2 = new Command_Action();
                command_Action2.defaultLabel = "DEV: Clear reset";
                command_Action2.action = delegate
                {
                    ticksToReset = 0;
                };
                yield return command_Action2;
            }
        }

        protected Pawn PawnOwner
        {
            get
            {
                if (parent is Apparel apparel)
                {
                    return apparel.Wearer;
                }

                if (parent is Pawn result)
                {
                    return result;
                }

                return null;
            }
        }

        public bool IsApparel => parent is Apparel;
        private bool IsBuiltIn => !IsApparel;

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo item in base.CompGetGizmosExtra())
            {
                yield return item;
            }

            if (!IsBuiltIn)
            {
                yield break;
            }

            foreach (Gizmo gizmo in GetGizmos())
            {
                yield return gizmo;
            }
        }

        private IEnumerable<Gizmo> GetGizmos()
        {
            if ((PawnOwner.Faction == Faction.OfPlayer || (parent is Pawn pawn && pawn.RaceProps.IsMechanoid)) && Find.Selector.SingleSelectedThing == PawnOwner)
            {
                Gizmo_NanoShieldArmorStatus gizmo_EvoShieldStatus = new Gizmo_NanoShieldArmorStatus();
                gizmo_EvoShieldStatus.shield = this;
                yield return gizmo_EvoShieldStatus;
            }
        }

        public override void CompTick()
        {
            base.CompTick();

            if (broken)
            {
                if (ticksToReset > 0)
                {
                    ticksToReset--;
                }
                if (ticksToReset <= 0)
                {
                    Reset();
                }
            }
            else if (energy < EnergyMax)
            {
                float rechargeRate = parent.GetStatValue(StatDefOf.EnergyShieldRechargeRate);
                float energyGain = rechargeRate / 60f;
                energy += energyGain;

                if (energy > EnergyMax)
                {
                    energy = EnergyMax;
                }
            }
        }

        public override void CompDrawWornExtras()
        {
            if (!broken && ShouldDisplay && energy > 0f)
            {
                float num = Mathf.Lerp(1.2f, 1.55f, energy / EnergyMax);
                Vector3 drawPos = (parent as Apparel).Wearer.Drawer.DrawPos;
                drawPos.y = AltitudeLayer.MoteOverhead.AltitudeFor();

                int ticksSinceAbsorb = Find.TickManager.TicksGame - lastAbsorbDamageTick;
                if (ticksSinceAbsorb < 8)
                {
                    float num2 = (float)(8 - ticksSinceAbsorb) / 8f * 0.05f;
                    drawPos += impactAngleVect * num2;
                }

                float angle = Rand.Range(0, 360);
                Vector3 s = new Vector3(num, 1f, num);
                Matrix4x4 matrix = default(Matrix4x4);
                matrix.SetTRS(drawPos, Quaternion.AngleAxis(angle, Vector3.up), s);
                Graphics.DrawMesh(MeshPool.plane10, matrix, BubbleMat, 0);
            }
        }

        public override void PostPreApplyDamage(ref DamageInfo dinfo, out bool absorbed)
        {
            absorbed = false;

            if (broken || (parent as Apparel)?.Wearer == null)
            {
                return;
            }

            // if (dinfo.Def.isRanged || dinfo.Def.isExplosive || dinfo.Def == DamageDefOf.EMP)
            // {
            float energyLoss = dinfo.Def == DamageDefOf.EMP
                ? dinfo.Amount * Props.energyLossPerDamage * 0.5f
                : dinfo.Amount * Props.energyLossPerDamage;

            energy -= energyLoss;
            if (energy < 0f)
            {
                Break();
            }
            absorbed = true;
            lastAbsorbDamageTick = Find.TickManager.TicksGame;
            impactAngleVect = Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle);
            return;
            // }
        }

        private void Reset()
        {
            broken = false;
            ticksToReset = -1;
            energy = Props.energyOnReset;
        }

        private void Break()
        {
            Pawn wearer = (parent as Apparel)?.Wearer;
            if (wearer?.Spawned == true)
            {
                float scale = Mathf.Lerp(Props.minDrawSize, Props.maxDrawSize, energy);
                EffecterDefOf.Shield_Break.SpawnAttached(parent, parent.MapHeld, scale);
                FleckMaker.Static(wearer.TrueCenter(), wearer.Map, FleckDefOf.ExplosionFlash, 12f);
                for (int i = 0; i < 6; i++)
                {
                    FleckMaker.ThrowDustPuff(wearer.TrueCenter() + Vector3Utility.HorizontalVectorFromAngle(Rand.Range(0, 360)) * Rand.Range(0.3f, 0.6f), wearer.Map, Rand.Range(0.8f, 1.2f));
                }
            }

            energy = 0f;
            broken = true;
            ticksToReset = Props.startingTicksToReset;
        }
    }
}
