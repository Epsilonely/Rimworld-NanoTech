using HarmonyLib;
using Verse;

namespace NanoTech
{
    [StaticConstructorOnStartup]
    public static class NanoTechHarmony
    {
        static NanoTechHarmony()
        {
            var harmony = new Harmony("Epsilonely.NanoTech");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Pawn_AgeTracker), nameof(Pawn_AgeTracker.AgeTickInterval))]
    static class Patch_AgeTickInterval_NanoAgeless
    {
        static bool Prefix(Pawn_AgeTracker __instance, int delta)
        {
            if (!__instance.Adult) return true;

            Pawn pawn = Traverse.Create(__instance).Field("pawn").GetValue<Pawn>();
            if (pawn == null) return true;

            if (pawn.health?.hediffSet?.HasHediff(NanoShieldSuit.NanoSuitProtectionDef) == true)
                return false;

            return true;
        }
    }
}
