using Verse;

namespace NanoTech
{
    public class CompProperties_NanoShieldSuit : CompProperties
    {
        public int startingTicksToReset = 1600;

        public float minDrawSize = 1.2f;

        public float maxDrawSize = 1.55f;

        public float energyLossPerDamage = 0.001f;

        public float energyOnReset = 0.5f;

        public CompProperties_NanoShieldSuit()
        {
            compClass = typeof(CompNanoShieldSuit);
        }
    }
}
