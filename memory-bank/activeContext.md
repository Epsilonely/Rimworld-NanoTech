# Active Context: NanoTech

## Current Status

**Date**: 2026-03-02
**Phase**: Feature development — Harmony-based aging suppression implemented

## Recent Work (2026-03-02 Session)

### Biological Aging Suppression — Reworked to Harmony (complete)
- **Problem**: Previous `HediffComp_NanoAgeless` approach had a bug — children wearing the suit would have aging suppressed too
- **Previous approach**: HediffComp `CompPostTick()` subtracting 250 ticks every 250 ticks — imprecise and applied to all ages
- **New approach**: Harmony `Prefix` patch on `Pawn_AgeTracker.AgeTickInterval()` — skips the method entirely when conditions met

**Implementation**:
- File: `NanoTech/HediffComp_NanoAgeless.cs` (repurposed — no longer contains HediffComp classes)
- `NanoTechHarmony` static class — `[StaticConstructorOnStartup]`, registers all patches via `harmony.PatchAll()`
- `Patch_AgeTickInterval_NanoAgeless` — Prefix patch on `Pawn_AgeTracker.AgeTickInterval(int delta)`
  - Returns `true` (allow aging) if pawn is not adult (`__instance.Adult == false`)
  - Returns `true` if pawn does not have `NanoSuitProtection` Hediff
  - Returns `false` (skip aging) if adult + NanoSuitProtection present
  - `pawn` field accessed via `Traverse.Create(__instance).Field("pawn").GetValue<Pawn>()` (private field)
  - Adult check: `__instance.Adult` property (race-agnostic, works for all species)

**RimWorld 1.6 API discoveries**:
- `AgeTick()` does NOT exist in RimWorld 1.6 — old decompile was outdated
- Correct public method: `AgeTickInterval(int delta)` — public, takes delta ticks
- `TickBiologicalAge()` exists but is **private** — cannot use with `nameof()`
- `pawn` field on `Pawn_AgeTracker` is private — must use `Traverse`
- `Adult` property exists and is public — race-aware adult check

**XML change**: `NanoSuitProtection.xml` — removed `<comps>` block (HediffCompProperties_NanoAgeless no longer exists)

### Harmony Dependency Added
- NuGet: `Lib.Harmony.Ref` 2.4.2 installed (reference-only, no DLL bundled)
- `.csproj`: `0Harmony` reference added via HintPath to `packages\Lib.Harmony.Ref.2.4.2\ref\netstandard2.0\0Harmony.dll`
- `packages/` added to `.gitignore`
- `NanoShieldSuit.NanoSuitProtectionDef` changed from `private` → `public static` (needed by Harmony patch class)
- **Player requirement**: Must install [Harmony mod](steam://url/CommunityFilePage/2009463077) (packageId: `brrainz.harmony`)
- `About.xml` modDependency for Harmony: **not yet added** (pending)

## Recent Work (2026-02-28 Session)

### Foam Damage Shield Bug Fix (complete)
- Bug: fire extinguisher foam (Extinguish damage, defaultDamage=999999) was breaking the shield
- Fix: `CompNanoShieldSuit.PostPreApplyDamage()` — skip absorption if `!dinfo.Def.harmsHealth`

### Korean Translation (complete)
- NanoHelmet Korean translation added to `Languages/Korean/DefInjected/ThingDefs/NanoShieldSuit.xml`

## Active Decisions / Open Questions

- `About.xml` Harmony dependency entry: not yet added
- `WorkSpeedGlobal`, `AimingDelayFactor`, `ImmunityGainSpeed` — C# StatPart: still pending
- Crafting difficulty — WorkToMake, costList, research cost: not yet done

## Next Steps (Suggested)

1. Add Harmony modDependency to `About.xml`
2. Release build → test aging suppression in-game
3. Verify: adult colonist with suit+helmet does not age; child does age normally
4. Crafting difficulty adjustment (optional)
