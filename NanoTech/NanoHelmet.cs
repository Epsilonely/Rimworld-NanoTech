using UnityEngine;
using Verse;
using RimWorld;
using System.Linq;

namespace NanoTech
{
    public class NanoHelmet : Apparel
    {
        private const string NanoShieldSuitDefName = "NanoShieldSuit";
        private static readonly HediffDef NanoSuitProtectionDef = HediffDef.Named("NanoSuitProtection");

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);

            // 슈트도 착용 중이면 Hediff 부여
            bool wearingSuit = pawn.apparel.WornApparel.Any(a => a.def.defName == NanoShieldSuitDefName);
            if (wearingSuit && pawn.health.hediffSet.GetFirstHediffOfDef(NanoSuitProtectionDef) == null)
                pawn.health.AddHediff(NanoSuitProtectionDef);
        }

        public override void Notify_Unequipped(Pawn pawn)
        {
            base.Notify_Unequipped(pawn);

            // 헬멧을 벗으면 무조건 Hediff 제거
            var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(NanoSuitProtectionDef);
            if (hediff != null)
                pawn.health.RemoveHediff(hediff);
        }

        protected override void Tick()
        {
            base.Tick();

            // HP 자가 수복: 착용 중일 때 2500틱마다 최대 HP의 1% 회복
            if (Wearer != null && this.IsHashIntervalTick(2500))
            {
                int healAmount = Mathf.Max(1, Mathf.RoundToInt(MaxHitPoints * 0.01f));
                HitPoints = Mathf.Min(HitPoints + healAmount, MaxHitPoints);
            }
        }
    }
}
