using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;
using Verse.AI;

namespace NanoTech
{
    [HarmonyPatch(typeof(JobGiver_OptimizeApparel), "TryGiveJob")]
    static class Patch_ShieldBeltOptimize
    {
        static void Postfix(Pawn pawn, ref Job __result)
        {
            if (__result == null) return;

            if (__result.targetA.Thing?.def.defName == NanoShieldSuit.ShieldBeltDefName
                && pawn.apparel.WornApparel.Any(a => a.def.defName == NanoShieldSuit.NanoShieldSuitDefName))
            {
                __result = null;
            }
        }
    }
}
