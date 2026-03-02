# Active Context: NanoTech

## Current Status

**Date**: 2026-03-02
**Phase**: Feature development — Shield belt AI conflict resolved via Harmony

## Recent Work (2026-03-02 Session — continued)

### Shield Belt AI Conflict Fix (complete)
- **Problem**: AI-controlled pawns wearing NanoShieldSuit would repeatedly attempt to equip shield belts from inventory
- **Root cause**: `RemoveShieldBelt()` moves belt to inventory → AI sees it and queues `JobGiver_OptimizeApparel` job → equips again → 60-tick poll removes it again (infinite loop)
- **Solution**: Harmony Postfix on `JobGiver_OptimizeApparel.TryGiveJob()` — nullifies the job before AI acts on it

**Implementation**:
- New file: `NanoTech/Patch_ShieldBeltOptimize.cs`
- Class: `Patch_ShieldBeltOptimize` — `[HarmonyPatch(typeof(JobGiver_OptimizeApparel), "TryGiveJob")]`
- Postfix: if `__result.targetA.Thing.def.defName == ShieldBeltDefName` AND pawn wears `NanoShieldSuit` → `__result = null`
- Auto-registered by `harmony.PatchAll()` in `NanoTechHarmony` — no manual wiring needed
- `NanoShieldSuit.ShieldBeltDefName` and `NanoShieldSuitDefName` changed from `private` → `public const` for cross-class access
- Existing 60-tick `RemoveShieldBelt` poll in `Tick()` retained as fallback safety net

### Biological Aging Suppression — Reworked to Harmony (complete)
- **Problem**: Previous `HediffComp_NanoAgeless` approach suppressed aging for children too
- **New approach**: Harmony `Prefix` patch on `Pawn_AgeTracker.AgeTickInterval(int delta)`
- File: `NanoTech/HediffComp_NanoAgeless.cs` (repurposed)
- `NanoTechHarmony` static class — `[StaticConstructorOnStartup]`, `harmony.PatchAll()`
- Condition: `__instance.Adult == true` AND pawn has `NanoSuitProtection` Hediff → skip tick
- `pawn` field accessed via `Traverse.Create(__instance).Field("pawn").GetValue<Pawn>()`

### Harmony Dependency (complete)
- NuGet: `Lib.Harmony.Ref` 2.4.2
- `About.xml`: `brrainz.harmony` modDependency added
- `packages/` gitignored

**RimWorld 1.6 API discoveries**:
- `AgeTick()` does NOT exist — use `AgeTickInterval(int delta)` (public)
- `TickBiologicalAge()` is private
- `Pawn_AgeTracker.pawn` is private — use `Traverse`
- `Pawn_AgeTracker.Adult` is public and race-aware

## Recent Work (2026-02-28 Session)

### Foam Damage Shield Bug Fix (complete)
- Fix: `CompNanoShieldSuit.PostPreApplyDamage()` — skip if `!dinfo.Def.harmsHealth`

### Korean Translation (complete)
- NanoHelmet Korean translation added

## Active Decisions / Open Questions

- `WorkSpeedGlobal`, `AimingDelayFactor`, `ImmunityGainSpeed` — C# StatPart: still pending
- Crafting difficulty — WorkToMake, costList, research cost: not yet done

## Next Steps (Suggested)

1. Release build → in-game test
2. Verify: AI pawn with NanoShieldSuit does NOT attempt to equip shield belt
3. Verify: adult colonist with suit+helmet does not age; child does age normally
4. Crafting difficulty adjustment (optional)
