# Active Context: NanoTech

## Current Status

**Date**: 2026-02-21
**Phase**: Content updates complete — ready for build & deploy

## Recent Work (2026-02-21 Session 2)

### Passive Stats (partial — XML only)
- `MoveSpeed` +0.48 → applied via `equippedStatOffsets` (XML)
- `WorkSpeedGlobal`, `AimingDelayFactor`, `ImmunityGainSpeed` → deferred (requires C# StatPart, decision pending)

### HP Self-Repair (C# complete)
- Added to `CompNanoShieldSuit.CompTick()`
- Restores 1% of MaxHitPoints every 2500 ticks (~42s) while worn
- Uses `IsHashIntervalTick(2500)` — performance safe

### Description & Documentation (complete)
- `About.xml` — detailed mod description with feature list
- `NanoShieldArmor.xml` — in-game English description updated
- `NanoShieldSuit.xml` (Korean) — Korean translation updated
- `README.md` — created at project root

### Previous Session (2026-02-21 Session 1)
- Balance patch complete (shield nerfs, vacuum buffs)
- Rename complete (NanoTech namespace)

## Active Decisions / Open Questions

- `WorkSpeedGlobal`, `AimingDelayFactor`, `ImmunityGainSpeed` — C# StatPart implementation: pending decision
- Crafting difficulty — WorkToMake, costList, research cost increase: not yet done
- Post-build auto-copy event: not configured

## Next Steps (Suggested)

1. VS Release build → generate `NanoTech.dll`
2. Copy `NanoTech.dll` → `Assemblies/` (delete old `NanoShieldArmor.dll`)
3. In-game test (shield, HP repair, move speed, descriptions)
4. Crafting difficulty adjustment (optional)
