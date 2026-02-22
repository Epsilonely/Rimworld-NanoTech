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
- [x] HP self-repair on suit — 1% MaxHP every 2500 ticks
- [x] `NanoHelmet : Apparel` class — HP self-repair + Hediff equip/unequip logic
- [x] `NanoSuitProtection` Hediff applied only when suit + helmet both worn

### XML Defs (complete)
- [x] `About.xml` — packageId: Epsilonely.NanoTech, name: NanoTech
- [x] `ThingDefs/NanoTechApparel.xml` — NanoShieldSuit + NanoHelmet definitions
- [x] `ResearchProjectDefs/ResearchProjects.xml` — defName: NanoTech
- [x] `HediffDefs/NanoSuitProtection.xml` — wetness immunity Hediff
- [x] `Patches/SoakingWet_Nullify.xml` — patches SoakingWet ThoughtDef with nullifyingHediffs
- [x] `Languages/Korean/` — Korean translations complete (suit, helmet, hediff)
- [x] Textures — NanoShieldSuit + NanoHelmet sprites

### Balance Patch (2026-02-21 complete)
- [x] `Insulation_Cold` / `Insulation_Heat`: 50 → 80
- [x] `VacuumResistance`: +0.45 (suit), +0.55 (helmet)
- [x] `EnergyShieldEnergyMax`: 1.8 → 1.3
- [x] `EnergyShieldRechargeRate`: 0.0006 → 0.0005
- [x] `energyLossPerDamage`: 0.002 → 0.01
- [x] `startingTicksToReset`: 1600 → 2400

### Passive Stats & Features (complete)
- [x] `MoveSpeed` +0.48 via `equippedStatOffsets` (XML, suit)
- [x] HP self-repair — suit: 1% MaxHP / 2500 ticks; helmet: same
- [x] Wetness immunity — `NanoSuitProtection` Hediff nullifies `SoakingWet` thought when full set worn

## Remaining Tasks

### Passive Stats (deferred)
- [ ] `WorkSpeedGlobal`, `AimingDelayFactor`, `ImmunityGainSpeed` (requires C# StatPart — pending decision)

### Crafting Difficulty
- [ ] Increase `WorkToMake` (currently 8000)
- [ ] Replace `costList` with appropriate high-tier materials
- [ ] Increase research cost and add prerequisites
- [ ] Confirm `techLevel` is Spacer

### Build & Deploy
- [ ] Run Release build in VS
- [ ] Copy `NanoTech.dll` → `Assemblies/`
- [ ] (Optional) Set up post-build event for auto-deploy

### Testing
- [ ] Wetness immunity: confirm no SoakingWet debuff when suit+helmet worn in rain or water
- [ ] Wetness resumes when either piece is removed
- [ ] NanoHelmet HP self-repair (2500 ticks)
- [ ] NanoSuitProtection Hediff visible in health tab (Korean: 나노 프로텍션)
- [ ] Shield break/reset, gizmo, EMP, vacuum tests

### Nice to Have
- [ ] Steam Workshop release setup

## Known Issues

None currently blocking (DLL deploy is a manual step, not a bug).

## Status Summary

| Area | Status |
|---|---|
| C# Code | 100% complete |
| XML Defs | 100% complete |
| Wetness Immunity | 100% complete |
| NanoHelmet | 100% complete |
| Passive Stats | Partial (MoveSpeed done, others deferred) |
| Crafting Difficulty | 0% |
| Build & Deploy | Pending (manual step) |
| Testing | 0% |
