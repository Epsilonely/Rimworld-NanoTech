using Verse;

namespace NanoShieldArmor
{
    public class CompProperties_NanoShieldArmor : CompProperties
    {
        public int startingTicksToReset = 1600;

        public float minDrawSize = 1.2f;

        public float maxDrawSize = 1.55f;

        public float energyLossPerDamage = 0.001f;

        public float energyOnReset = 0.5f;

        public bool blocksRangedWeapons = false;

        public CompProperties_NanoShieldArmor()
        {
            compClass = typeof(CompNanoShieldArmor);
        }
    }
}
