using Verse;
using RimWorld;
using System.Linq;


namespace NanoShieldArmor
{
    public class NanoShieldArmor : Apparel
    {
        public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
        {
            base.DeSpawn(mode);
        }

        private float savedEnergy;
        private bool savedBroken;
        private int savedTicksToReset;
        private bool hasStoredValues;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            if (!respawningAfterLoad)
            {
                var shield = GetComp<CompNanoShieldArmor>();
                if (shield != null)
                {
                    shield.SetEnergy(shield.EnergyMax);
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();

            var shield = GetComp<CompNanoShieldArmor>();
            if (Scribe.mode == LoadSaveMode.Saving && shield != null)
            {
                savedEnergy = shield.GetEnergy();
                savedBroken = shield.IsBroken;
                savedTicksToReset = shield.GetTicksToReset();
                hasStoredValues = true;
            }

            Scribe_Values.Look(ref savedEnergy, "savedEnergy", 0f);
            Scribe_Values.Look(ref savedBroken, "savedBroken", false);
            Scribe_Values.Look(ref savedTicksToReset, "savedTicksToReset", -1);
            Scribe_Values.Look(ref hasStoredValues, "hasStoredValues", false);
            Scribe_Values.Look(ref unequipEnergy, "unequipEnergy", 0f);
            Scribe_Values.Look(ref wasUnequipped, "wasUnequipped", false);

            if (Scribe.mode == LoadSaveMode.LoadingVars && hasStoredValues && shield != null)
            {
                shield.SetEnergy(savedEnergy);
                shield.SetBroken(savedBroken);
                shield.SetTicksToReset(savedTicksToReset);
            }
        }

        private float unequipEnergy;
        private bool wasUnequipped;

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);

            var shieldBelt = pawn.apparel.WornApparel
                .FirstOrDefault(a => a.def.defName == "Apparel_ShieldBelt");

            if (shieldBelt != null)
            {
                pawn.apparel.Remove(shieldBelt);
                pawn.inventory.innerContainer.TryAdd(shieldBelt);
                Messages.Message("EvoShield replaced existing shield belt.", MessageTypeDefOf.NeutralEvent);
            }

            if (wasUnequipped)
            {
                var shield = GetComp<CompNanoShieldArmor>();
                if (shield != null)
                {
                    shield.SetEnergy(unequipEnergy);
                }
                wasUnequipped = false;
            }
        }

        public override void Notify_Unequipped(Pawn pawn)
        {
            base.Notify_Unequipped(pawn);

            var shield = GetComp<CompNanoShieldArmor>();
            if (shield != null)
            {
                unequipEnergy = shield.GetEnergy();
                wasUnequipped = true;
            }
        }

        public override void Tick()
        {
            base.Tick();

            // 착용자가 있고, 60틱마다 확인 (성능 최적화)
            if (Wearer != null && Find.TickManager.TicksGame % 60 == 0)
            {
                var shieldBelt = Wearer.apparel.WornApparel
                    .FirstOrDefault(a => a.def.defName == "Apparel_ShieldBelt");

                if (shieldBelt != null)
                {
                    Wearer.apparel.Remove(shieldBelt);
                    Wearer.inventory.innerContainer.TryAdd(shieldBelt);
                    Messages.Message("EvoShield replaced existing shield belt.", MessageTypeDefOf.NeutralEvent);
                }
            }
        }

        // 보호막 벨트를 착용중이면 이 갑옷은 데미지를 흡수하지 않음
        public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
        {
            if (Wearer != null && Wearer.apparel.WornApparel.Any(a => a.def.defName == "Apparel_ShieldBelt"))
            {
                return false;
            }
            return base.CheckPreAbsorbDamage(dinfo);
        }
    }
}
