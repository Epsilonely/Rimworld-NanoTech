# NanoTech — RimWorld Mod

A RimWorld 1.6 mod that adds the **Nano Shield Suit**, a high-tech combat armor with an integrated personal energy shield.

## Features

- **Personal Energy Shield** — Absorbs incoming damage. Automatically recharges between engagements. Shatters at 0 energy and reactivates after a cooldown (~40 seconds).
- **Self-Repairing Hull** — Nanobots restore 1% of max HP every ~42 seconds while worn.
- **Enhanced Mobility** — +0.48 move speed while equipped.
- **Superior Insulation** — Cold/Heat insulation 80 each. Vacuum resistance +0.45 (Odyssey DLC).
- **EMP Resistance** — EMP damage drains shield energy at 50% rate.
- **Shield Belt Conflict Resolution** — Automatically moves any equipped shield belt to inventory on equip.

## Stats

| Stat | Value |
|---|---|
| Max HP | 500 |
| Sharp Armor | 2.33 |
| Blunt Armor | 1.4 |
| Heat Armor | 1.1 |
| Cold Insulation | 80 |
| Heat Insulation | 80 |
| Vacuum Resistance | +0.45 |
| Move Speed | +0.48 |
| Shield Energy Max | 1.3 |
| Shield Recharge Rate | 0.0005 / tick |
| Energy Loss per Damage | 0.01 |
| Reset Cooldown | 2400 ticks (~40s) |
| Energy on Reset | 50% |

## Requirements

- RimWorld 1.6
- No external mod dependencies
- Odyssey DLC recommended (for Vacuum Resistance stat)

## Installation

1. Copy the `NanoShieldArmor` folder to:
   ```
   <RimWorld>\Mods\NanoShieldArmor\
   ```
2. Enable the mod in the RimWorld mod manager.
3. Research **NanoTech** to unlock the crafting recipe.
4. Craft at a **Fabrication Bench** (Crafting skill 15 required).

## Building from Source

### Requirements
- Visual Studio 2022
- .NET Framework 4.8 SDK
- RimWorld installed at `D:\SteamLibrary\steamapps\common\RimWorld\`

### Build & Deploy
1. Open `NanoTech.sln` in Visual Studio 2022.
2. Build in **Release** configuration.
3. Copy `NanoTech\bin\Release\NanoTech.dll` to:
   ```
   <RimWorld>\Mods\NanoShieldArmor\Assemblies\
   ```
4. Delete the old `NanoShieldArmor.dll` from that folder.

## Project Structure

```
NanoTech/
├── NanoTech.sln
├── NanoTech/
│   ├── NanoShieldSuit.cs               # Apparel class
│   ├── CompNanoShieldSuit.cs           # Shield component logic
│   ├── CompProperties_NanoShieldSuit.cs # Component properties
│   └── Gizmo_NanoShieldSuitStatus.cs   # UI gizmo (energy bar)
└── memory-bank/                        # Development context docs
```

## Author

**Epsilonely**
- Package ID: `Epsilonely.NanoTech`
- Steam Workshop ID: (pending)
