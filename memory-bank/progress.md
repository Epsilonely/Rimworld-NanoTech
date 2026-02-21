# Progress: NanoTech

## What Works

### C# Code (complete)
- [x] `NanoShieldSuit` apparel class — namespace `NanoTech`
- [x] `CompNanoShieldSuit` component — energy management, damage absorption, break/reset logic
- [x] Shield belt conflict resolution — moves to inventory (RemoveShieldBelt helper)
- [x] EMP damage multiplier (0.5x)
- [x] Visual shield bubble (`CompDrawWornExtras`)
- [x] Break effects (explosion flash, dust puffs)
- [x] Shake/impact visual on damage hit
- [x] Reset timer (`startingTicksToReset`)
- [x] Passive energy recharge (IsHashIntervalTick(60) based)
- [x] Save/load persistence (`PostExposeData`)
- [x] `Gizmo_NanoShieldSuitStatus` UI bar
- [x] `CompProperties_NanoShieldSuit` with XML-configurable properties
- [x] RimWorld 1.6 compatible (`protected override void Tick()`)
- [x] DEV gizmo commands (Break, Clear Reset)

### XML Defs (complete)
- [x] `About.xml` — packageId: Epsilonely.NanoTech, name: NanoTech
- [x] `ThingDefs/NanoShieldArmor.xml` — thingClass: NanoTech.NanoShieldSuit
- [x] `ResearchProjectDefs/ResearchProjects.xml` — defName: NanoTech
- [x] `Languages/Korean/` — Korean translations complete
- [x] Textures — NanoShieldSuit directional sprites complete

### Rename (2026-02-21 complete)
- [x] Solution/project filenames → NanoTech
- [x] Namespace → NanoTech
- [x] Class names → NanoShieldSuit, CompNanoShieldSuit, etc.
- [x] XML thingClass / compClass references → NanoTech.*

### Balance Patch (2026-02-21 complete)
- [x] `Insulation_Cold` / `Insulation_Heat`: 50 → 80
- [x] `VacuumResistance`: +0.45 added (Odyssey DLC, auto-ignored if absent)
- [x] `EnergyShieldEnergyMax`: 1.8 → 1.3
- [x] `EnergyShieldRechargeRate`: 0.0006 → 0.0005
- [x] `energyLossPerDamage`: 0.002 → 0.01 (XML + C# synced)
- [x] `startingTicksToReset`: 1600 → 2400 (XML + C# synced)

### Passive Stats & New Features (2026-02-21 complete)
- [x] `MoveSpeed` +0.48 via `equippedStatOffsets` (XML)
- [x] HP self-repair — restores 1% of MaxHP every 2500 ticks while worn (C#)

### Content & Documentation (2026-02-21 complete)
- [x] `About.xml` — detailed mod description with feature list
- [x] `NanoShieldArmor.xml` — in-game English description updated
- [x] `NanoShieldSuit.xml` (Korean) — Korean translation updated
- [x] `README.md` — created at project root

## Remaining Tasks

### Passive Stats (deferred)
- [ ] `WorkSpeedGlobal`, `AimingDelayFactor`, `ImmunityGainSpeed` (requires C# StatPart — pending decision)

### Crafting Difficulty
- [ ] Increase `WorkToMake` (currently 8000)
- [ ] Replace `costList` with appropriate high-tier materials
- [ ] Increase research cost (currently 6000) and add prerequisites
- [ ] Change `techLevel` from Industrial to Spacer

### Build & Deploy
- [ ] Run Release build in VS
- [ ] Copy `NanoTech.dll` → `Assemblies/` (delete old `NanoShieldArmor.dll`)
- [ ] (Optional) Set up post-build event for auto-deploy

### Testing
- [ ] In-game test: equip, damage absorption, shield break/reset, gizmo display
- [ ] HP self-repair verification (every 2500 ticks, +5 HP)
- [ ] MoveSpeed +0.48 visible in stat panel
- [ ] EMP interaction test
- [ ] Vacuum resistance test (Odyssey DLC map)

### Nice to Have
- [ ] Nano Helmet (planned for future development)
- [ ] Steam Workshop release setup

## Known Issues

| Issue | Severity | Notes |
|---|---|---|
| DLL not deployed | Blocking | Build NanoTech.dll and replace NanoShieldArmor.dll in Assemblies/ |

## Status Summary

**Overall**: Content updates complete. Build & deploy and testing pending.

**C# Code**: 100% complete
**XML Defs**: 100% complete
**Passive Stats**: Partial (MoveSpeed done, others deferred)
**Content & Docs**: 100% complete
**Crafting Difficulty**: 0%
**Build & Deploy**: 0%
**Testing**: 0%
