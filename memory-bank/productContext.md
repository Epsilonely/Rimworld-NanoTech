# Product Context: NanoTech

## Problem Solved

RimWorld's vanilla shield belt is a separate equipment slot item and has mechanical limitations. This mod introduces a **wearable armor piece** (apparel slot) that provides personal energy shielding — combining the protection concept of a shield belt with the wearability of armor.

## Target User

RimWorld mod users who want:
- A high-tech, lore-friendly personal shield integrated as armor
- More tactical options for colonist protection in combat
- Nanotech/sci-fi aesthetic in their playthrough

## UX Goals

### For the Player
- **Clear status visibility** — The shield status gizmo shows energy level at a glance (fillable bar + value text).
- **Predictable behavior** — Shield breaks visibly (effects + sound), resets on a timer.
- **No slot confusion** — Conflicts with shield belts are resolved automatically on equip (moved to inventory).

### For the Colonist
- **Passive protection** — No player action required; shield absorbs damage automatically.
- **EMP awareness** — EMP weapons drain the shield faster (but at a reduced 0.5x rate).
- **Recovery** — After breaking, the shield resets autonomously after cooldown.

## Key Behaviors

| Behavior | Description |
|---|---|
| Equip | Moves any existing shield belt to pawn's inventory |
| Damage | Absorbed by shield energy; energy reduced proportionally |
| EMP Damage | Absorbed at 0.5x energy loss rate |
| Break | Shield shatters at 0 energy; explosion flash + dust puff effects play |
| Reset | After `startingTicksToReset` (default 1600), shield restores to `energyOnReset` |
| Visual | Shield bubble drawn at configurable draw size (min/max radius) |
| Shake | Visual shake on each hit for feedback |
| Gizmo | Shown in bottom bar; renders energy as colored fill bar with numeric values |

## Differentiators vs. Vanilla Shield Belt

- Occupies apparel slot (not separate equipment slot)
- Nanotech theme / sci-fi aesthetic
- Configurable energy and reset properties via XML Defs
- Richer visual effect on break
