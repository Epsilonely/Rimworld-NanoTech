# Active Context: NanoTech

## Current Status

**Date**: 2026-02-28
**Phase**: Feature development in progress — NanoAgeless effect + foam damage fix added

## Recent Work (2026-02-28 Session)

### NanoAgeless Effect (complete)
- New file: `NanoTech/HediffComp_NanoAgeless.cs`
- Classes: `HediffCompProperties_NanoAgeless` + `HediffComp_NanoAgeless`
- Mechanism: `CompPostTick()` — every 250 ticks, `pawn.ageTracker.AgeBiologicalTicks -= 250`
- Effective result: biological aging suppressed (equivalent to Ageless gene)
- Condition: only active when `NanoSuitProtection` Hediff is present (suit + helmet both worn)
- `NanoSuitProtection.xml` updated: `HediffCompProperties_NanoAgeless` comp registered

### Foam Damage Shield Bug Fix (complete)
- Bug: fire extinguisher foam (Extinguish damage, defaultDamage=999999) was breaking the shield
- Root cause: shield absorbed all damage types including `harmsHealth=false` ones
- Fix: `CompNanoShieldSuit.PostPreApplyDamage()` — skip absorption if `!dinfo.Def.harmsHealth`
- Covers all non-harmful damage types (foam, stun, etc.) generically

### Korean Translation (complete)
- `Languages/Korean/DefInjected/ThingDefs/NanoShieldSuit.xml` — NanoHelmet Korean translation added
  - label: `나노 헬멧`
  - description: full Korean text including aging suppression mention

## Recent Work (2026-02-22 Session)

### Wetness Immunity System (complete)
- Approach: Hediff-based nullification (no Harmony required)
- `NanoSuitProtection` HediffDef created (`Defs/HediffDefs/NanoSuitProtection.xml`)
- `SoakingWet` ThoughtDef patched via `Patches/SoakingWet_Nullify.xml` using `PatchOperationAdd` to add `nullifyingHediffs`
- Hediff applied/removed in `NanoShieldSuit.Notify_Equipped/Unequipped`
- Initially: applied when suit is worn alone
- Updated: applied only when **both** NanoShieldSuit + NanoHelmet are worn simultaneously

### NanoHelmet Implementation (complete)
- New C# class: `NanoHelmet : Apparel` (`NanoTech/NanoHelmet.cs`)
- Registered in `NanoTech.csproj`
- XML `thingClass` set to `NanoTech.NanoHelmet` in `NanoTechApparel.xml`
- Features:
  - `Notify_Equipped`: applies `NanoSuitProtection` Hediff if suit is also worn
  - `Notify_Unequipped`: removes `NanoSuitProtection` Hediff unconditionally
  - `Tick()`: HP self-repair — restores 1% of MaxHP every 2500 ticks while worn (same as suit)

### NanoSuitProtection Hediff Logic
- Condition: BOTH NanoShieldSuit AND NanoHelmet must be worn
- `NanoShieldSuit.UpdateProtectionHediff(pawn)`: checks for NanoHelmet in worn apparel
- `NanoHelmet.Notify_Equipped`: checks for NanoShieldSuit in worn apparel
- Either piece unequipped → Hediff immediately removed

### Harmony Attempt (abandoned)
- Tried patching `ThoughtWorker_Wet` — class does not exist in RimWorld 1.6
- Tried patching `MemoryThoughtHandler.TryGainMemoryFast(ThoughtDef)` — method overloads caused AmbiguousMatchException, then null return
- Final solution: Hediff nullification via XML patch (no Harmony needed)
- Harmony NuGet package (Lib.Harmony 2.4.2) and references removed from project

### Korean Translation
- `Languages/Korean/DefInjected/HediffDefs/NanoSuitProtection.xml` created
- label: `나노 프로텍션`, description: full Korean text
- Stage label removed (empty stage `<li />` in HediffDef to avoid showing "active")

## Active Decisions / Open Questions

- `WorkSpeedGlobal`, `AimingDelayFactor`, `ImmunityGainSpeed` — C# StatPart: still pending
- Crafting difficulty — WorkToMake, costList, research cost: not yet done
- Post-build auto-copy event: not configured

## Next Steps (Suggested)

1. VS Release build → generate `NanoTech.dll`
2. Copy `NanoTech.dll` → `Assemblies/`
3. In-game test: wetness immunity (suit+helmet together), helmet HP repair, Hediff display
4. Crafting difficulty adjustment (optional)
