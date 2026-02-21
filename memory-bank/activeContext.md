# Active Context: NanoTech

## Current Status

**Date**: 2026-02-21
**Phase**: Balance adjustments in progress (vacuum buffs + shield nerfs complete)

## Recent Work (2026-02-21 Balance Patch)

### Vacuum Environment Buffs (XML complete)
- `Insulation_Cold`: 50 → **80**
- `Insulation_Heat`: 50 → **80**
- `VacuumResistance`: none → **+0.45** (added to `equippedStatOffsets`)
  - Auto-ignored if Odyssey DLC is not present (no extra C# check needed)

### Shield Nerfs (XML + C# complete)
- `EnergyShieldEnergyMax`: 1.8 → **1.3**
- `EnergyShieldRechargeRate`: 0.0006 → **0.0005**
- `energyLossPerDamage`: 0.002 → **0.01** (synced in XML and CompProperties_NanoShieldSuit.cs)
- `startingTicksToReset`: 1600 → **2400** (synced in XML and CompProperties_NanoShieldSuit.cs)
- `energyOnReset`: 0.5 (unchanged)

### Reference: Shield Belt Stats (for comparison)
- EnergyMax: 1.1 / RechargeRate: 0.13 / energyLossPerDamage: 0.033 / reset: 3200 ticks

## Active Decisions / Open Questions

### Passive Stats Implementation (pending)
- `MoveSpeed` +0.48 → **XML only (equippedStatOffsets)** — feasible
- `WorkSpeedGlobal`, `AimingDelayFactor`, `ImmunityGainSpeed` → **XML not supported; requires C# StatPart**
- `BleedRateMultiplier` → **stat does not exist in RimWorld 1.6**
- Direction not yet decided (XML only vs. full C# implementation)

### Open Items
1. **Passive stats** — MoveSpeed XML only, or implement others via C#?
2. **Crafting difficulty** — WorkToMake, costList, research cost increase pending
3. **Description** — lore text update pending
4. **DLL deployment** — build NanoTech.dll and replace old NanoShieldArmor.dll in Assemblies/
5. **Post-build event** — auto-copy DLL on build (undecided)
6. **Nano Helmet** — planned for future development

## Next Steps (Suggested)

1. Decide and apply passive stat approach
2. Increase crafting difficulty (WorkToMake, costList, research cost)
3. Update description text (lore flavor)
4. VS Release build → deploy DLL → in-game test
