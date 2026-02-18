using Verse;
using RimWorld;
using System.Linq;


namespace NanoShieldArmor
{
    public class NanoShieldArmor : Apparel
    {
        private const string ShieldBeltDefName = "Apparel_ShieldBelt";

        private void RemoveShieldBelt(Pawn pawn, bool notify)
        {
            var shieldBelt = pawn.apparel.WornApparel
                .FirstOrDefault(a => a.def.defName == ShieldBeltDefName);
            if (shieldBelt != null)
            {
                pawn.apparel.Remove(shieldBelt);
                pawn.inventory.innerContainer.TryAdd(shieldBelt);
                if (notify)
                    Messages.Message("NanoShieldArmor replaced existing shield belt.", MessageTypeDefOf.NeutralEvent);
            }
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            if (!respawningAfterLoad)
            {
                var shield = GetComp<CompNanoShieldArmor>();
                if (shield != null)
                {
                    shield.Energy = shield.EnergyMax;
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref unequipEnergy, "unequipEnergy", 0f);
            Scribe_Values.Look(ref wasUnequipped, "wasUnequipped", false);
        }

        private float unequipEnergy;
        private bool wasUnequipped;

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);

            RemoveShieldBelt(pawn, notify: true);

            if (wasUnequipped)
            {
                var shield = GetComp<CompNanoShieldArmor>();
                if (shield != null)
                {
                    shield.Energy = unequipEnergy;
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
                unequipEnergy = shield.Energy;
                wasUnequipped = true;
            }
        }

        protected override void Tick()
        {
            base.Tick();

            if (Wearer != null && this.IsHashIntervalTick(60))
            {
                RemoveShieldBelt(Wearer, notify: false);
            }
        }

        // 보호막 벨트를 착용중이면 이 갑옷은 데미지를 흡수하지 않음
        public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
        {
            if (Wearer != null && Wearer.apparel.WornApparel.Any(a => a.def.defName == ShieldBeltDefName))
            {
                return false;
            }
            return base.CheckPreAbsorbDamage(dinfo);
        }
    }
}
