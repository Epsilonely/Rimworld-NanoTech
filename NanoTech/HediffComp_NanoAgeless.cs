using Verse;

namespace NanoTech
{
    public class HediffCompProperties_NanoAgeless : HediffCompProperties
    {
        public HediffCompProperties_NanoAgeless()
        {
            compClass = typeof(HediffComp_NanoAgeless);
        }
    }

    public class HediffComp_NanoAgeless : HediffComp
    {
        private const int IntervalTicks = 250;

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Pawn.IsHashIntervalTick(IntervalTicks))
            {
                Pawn.ageTracker.AgeBiologicalTicks -= IntervalTicks;
            }
        }
    }
}
