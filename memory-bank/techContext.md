# Tech Context: NanoShieldArmor

## Tech Stack

| Layer | Technology |
|---|---|
| Language | C# |
| Framework | .NET Framework 4.8 |
| Build System | MSBuild / Visual Studio 2022 |
| Output | Class Library (.dll) |
| Game Engine | Unity (via RimWorld) |
| Mod Target | RimWorld |

## Project Files

| File | Purpose |
|---|---|
| `NanoShieldArmor.sln` | Visual Studio 2022 solution |
| `NanoShieldArmor/NanoShieldArmor.csproj` | C# project, targets .NET 4.8 |
| `NanoShieldArmor/NanoShieldArmor.cs` | Main apparel class |
| `NanoShieldArmor/CompNanoShieldArmor.cs` | Shield component logic |
| `NanoShieldArmor/CompProperties_NanoShieldArmor.cs` | Component properties |
| `NanoShieldArmor/Gizmo_NanoShieldArmorStatus.cs` | UI gizmo |
| `NanoShieldArmor/Properties/AssemblyInfo.cs` | Assembly metadata |

## Build Configuration

- **Debug**: Full debug symbols, output to `bin\Debug\`
- **Release**: PDB debug info only, optimized, output to `bin\Release\`
- **Platform**: AnyCPU

## Dependencies

### Direct (System)
- `System`
- `System.Core`
- `System.Xml`
- `System.Data`
- `System.Net.Http`
- `Microsoft.CSharp`

### Implicit Runtime (RimWorld DLLs — not bundled in repo)
- **Verse** — Core RimWorld engine namespace (Pawn, Thing, ThingComp, Gizmo, etc.)
- **RimWorld** — Game-specific namespace (Apparel, ShieldBelt, DamageDefOf, etc.)
- **UnityEngine** — Graphics, Vector3, Quaternion, Material, Texture2D, Mesh, Graphics

> These DLLs are located in the RimWorld install directory and must be referenced locally.

## RimWorld API Surface Used

| API | Usage |
|---|---|
| `Apparel` | Base class for NanoShieldArmor |
| `ThingComp` | Base class for CompNanoShieldArmor |
| `CompProperties` | Base class for CompProperties_NanoShieldArmor |
| `Gizmo` | Base class for Gizmo_NanoShieldArmorStatus |
| `ShieldBelt` | Detected and removed on equip |
| `DamageDefOf.EMP` | Special case for EMP energy multiplier |
| `DamageInfo` | Passed through `PostPreApplyDamage` |
| `EffecterDef` | Visual effects on shield break |
| `FleckDef` | Dust/particle effects on break |
| `StatDef` | Stats lookup |

## Setup Requirements

1. Visual Studio 2022 (or MSBuild 17+)
2. RimWorld installed (for DLL references)
3. .NET Framework 4.8 SDK
4. Reference paths configured in `.csproj` pointing to local RimWorld DLLs

## Deployment

Compiled DLL must be placed in the mod's `Assemblies/` folder within the RimWorld Mods directory. XML Defs (not currently in this repo) define the in-game item and reference the C# types via `compClass`.

## Known Issues / Quirks

- `.csproj` references `Class1.cs` which does not exist — likely a stale reference from project creation, can be safely removed
- No XML Defs are present in this repository; they must exist separately or be created
- RimWorld DLL references are machine-local (not portable); developers must configure their own reference paths
