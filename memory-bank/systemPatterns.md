# System Patterns: NanoTech

## Architecture Overview

The mod follows RimWorld's standard **Component-Based architecture**. Logic is split across C# classes:

```
NanoShieldSuit (Apparel)
  └── CompNanoShieldSuit (ThingComp)
        └── CompProperties_NanoShieldSuit (CompProperties)
  └── Gizmo_NanoShieldSuitStatus (Gizmo)

NanoHelmet (Apparel)
```

## Namespace

All classes use namespace: `NanoTech`

## Class Responsibilities

### `NanoShieldSuit : Apparel`
**File**: NanoTech/NanoShieldSuit.cs

- `const ShieldBeltDefName = "Apparel_ShieldBelt"`
- `const NanoHelmetDefName = "NanoHelmet"`
- `static readonly HediffDef NanoSuitProtectionDef`
- `RemoveShieldBelt(Pawn, bool notify)` — Moves belt to inventory
- `IsFullSetEquipped(Pawn)` — Returns true if NanoHelmet is also worn
- `UpdateProtectionHediff(Pawn)` — Adds/removes NanoSuitProtection based on full set check
- `SpawnSetup()` — Sets energy to max on first spawn
- `Notify_Equipped(Pawn)` — RemoveShieldBelt + UpdateProtectionHediff + restore energy
- `Notify_Unequipped(Pawn)` — Removes NanoSuitProtection Hediff unconditionally + saves energy
- `protected override void Tick()` — Calls RemoveShieldBelt every 60 ticks
- `CheckPreAbsorbDamage()` — Disables damage absorption if shield belt is worn
- `ExposeData()` — Persists unequipEnergy and wasUnequipped

### `NanoHelmet : Apparel`
**File**: NanoTech/NanoHelmet.cs

- `const NanoShieldSuitDefName = "NanoShieldSuit"`
- `static readonly HediffDef NanoSuitProtectionDef`
- `Notify_Equipped(Pawn)` — Applies NanoSuitProtection Hediff if suit is also worn
- `Notify_Unequipped(Pawn)` — Removes NanoSuitProtection Hediff unconditionally
- `protected override void Tick()` — HP self-repair: 1% MaxHP every 2500 ticks while worn

### `CompNanoShieldSuit : ThingComp`
**File**: NanoTech/CompNanoShieldSuit.cs

- Public API: `Energy`, `IsBroken`, `TicksToReset`, `EnergyMax`, `IsApparel`
- `PostExposeData()` — Saves/loads energy, broken, ticksToReset
- `CompTick()` — Handles broken reset countdown; recharges energy every 60 ticks; HP self-repair every 2500 ticks
- `PostPreApplyDamage()` — Absorbs damage; applies 0.5x multiplier for EMP; breaks at 0 energy
- `Break()` — Calculates safe scale for effects, plays effects, sets broken flag
- `Reset()` — Restores energy to energyOnReset value
- `CompDrawWornExtras()` — Renders shield bubble (size proportional to energy, shake on hit)
- `CompGetWornGizmosExtra()` / `CompGetGizmosExtra()` — Provides UI gizmo + DEV commands

### `CompProperties_NanoShieldSuit : CompProperties`
**File**: NanoTech/CompProperties_NanoShieldSuit.cs

| Property | Default | Description |
|---|---|---|
| `startingTicksToReset` | 2400 | Ticks to reset after shield break |
| `minDrawSize` | 1.2f | Minimum shield bubble radius |
| `maxDrawSize` | 1.55f | Maximum shield bubble radius |
| `energyLossPerDamage` | 0.01f | Energy loss per 1 damage |
| `energyOnReset` | 0.5f | Energy restored on reset (0–1 scale) |

### `Gizmo_NanoShieldSuitStatus : Gizmo`
**File**: NanoTech/Gizmo_NanoShieldSuitStatus.cs

- UI-only; displays fillable energy bar (current/max)

## XML ↔ C# Connection

| XML | C# Type |
|---|---|
| `<thingClass>NanoTech.NanoShieldSuit</thingClass>` | `NanoShieldSuit : Apparel` |
| `<thingClass>NanoTech.NanoHelmet</thingClass>` | `NanoHelmet : Apparel` |
| `<li Class="NanoTech.CompProperties_NanoShieldSuit">` | `CompProperties_NanoShieldSuit` |

## Wetness Immunity Pattern

**Pattern**: Hediff-based ThoughtDef nullification (no Harmony required)

1. Define a custom `HediffDef` (`NanoSuitProtection`)
2. Patch target `ThoughtDef` (`SoakingWet`) via `PatchOperationAdd` to add `<nullifyingHediffs>`
3. Apply Hediff via C# `Notify_Equipped` when condition met; remove via `Notify_Unequipped`
4. Condition: both NanoShieldSuit AND NanoHelmet must be worn simultaneously

**Why not Harmony**: `ThoughtWorker_Wet` does not exist in RimWorld 1.6. `TryGainMemoryFast` has multiple overloads causing `AmbiguousMatchException`. Hediff nullification is the clean, stable solution.

## Key Design Patterns

### EMP Special Casing
```csharp
float energyLoss = dinfo.Def == DamageDefOf.EMP
    ? dinfo.Amount * Props.energyLossPerDamage * 0.5f
    : dinfo.Amount * Props.energyLossPerDamage;
```

### Safe Scale Calculation on Break
```csharp
float scale = Mathf.Lerp(Props.minDrawSize, Props.maxDrawSize, Mathf.Clamp01(energy / EnergyMax));
```

### Performance — Throttled Stat Lookup
```csharp
if (parent.IsHashIntervalTick(60))
{
    float rechargeRate = parent.GetStatValue(StatDefOf.EnergyShieldRechargeRate);
    energy += rechargeRate;
}
```

### HP Self-Repair Pattern
```csharp
if (Wearer != null && this.IsHashIntervalTick(2500))
{
    int healAmount = Mathf.Max(1, Mathf.RoundToInt(MaxHitPoints * 0.01f));
    HitPoints = Mathf.Min(HitPoints + healAmount, MaxHitPoints);
}
```
