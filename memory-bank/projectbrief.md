# Project Brief: NanoTech

## Project Identity

- **Name**: NanoTech
- **Type**: RimWorld Mod — C# DLL (Class Library)
- **Source**: `c:\Users\yoong\source\repos\NanoTech\`
- **Solution**: `NanoTech.sln` (Visual Studio 2022)
- **Assembly Name**: `NanoTech`
- **Assembly Version**: 1.0.0.0
- **Deployed Mod Folder**: `D:\SteamLibrary\steamapps\common\RimWorld\Mods\NanoShieldArmor\`

## Core Purpose

A RimWorld mod that adds a **nano shield suit** apparel item. When worn, it surrounds the colonist with an energy-based shield bubble that absorbs incoming damage. The shield recharges automatically and has a cooldown reset period when broken.

## Core Requirements

1. **Shield Energy System** — Energy-based absorption of incoming damage; shield breaks at 0 energy.
2. **Recharge & Reset Mechanic** — Shield auto-resets after a configurable cooldown (default ~27 seconds / 1600 ticks).
3. **EMP Resistance** — EMP damage is absorbed at half rate (0.5x energy loss multiplier).
4. **Shield Belt Conflict Resolution** — Automatically moves existing shield belts to inventory when equipped.
5. **Visual Feedback** — Shield bubble drawn around the wearer; shake effect on damage; explosion/dust effects on break.
6. **UI Gizmo** — In-game status display showing shield energy as a fillable bar with percentage.
7. **Save/Load Persistence** — Shield energy and state persists through save/load.

## Project Scope

- Single apparel item (NanoShieldSuit) with component-based logic
- No external mod dependencies beyond RimWorld base game
- Targets RimWorld's .NET Framework 4.8 runtime
- Build output: `NanoTech.dll` consumed by RimWorld's mod loader
- XML Defs located in deployed mod folder (`D:\SteamLibrary\steamapps\common\RimWorld\Mods\NanoShieldArmor\`)
