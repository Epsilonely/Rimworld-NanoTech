# Tech Context: NanoTech

## Tech Stack

| Layer | Technology |
|---|---|
| Language | C# |
| Framework | .NET Framework 4.8 |
| Build System | MSBuild / Visual Studio 2022 |
| Output | Class Library (.dll) → `NanoTech.dll` |
| Game Engine | Unity (via RimWorld) |
| Mod Target | RimWorld 1.6 |

## Project Files

| File | Purpose |
|---|---|
| `NanoTech.sln` | Visual Studio 2022 solution |
| `NanoTech/NanoTech.csproj` | C# project, targets .NET 4.8 |
| `NanoTech/NanoShieldSuit.cs` | Main suit apparel class |
| `NanoTech/NanoHelmet.cs` | Helmet apparel class |
| `NanoTech/CompNanoShieldSuit.cs` | Shield component logic |
| `NanoTech/CompProperties_NanoShieldSuit.cs` | Component properties |
| `NanoTech/Gizmo_NanoShieldSuitStatus.cs` | UI gizmo |
| `NanoTech/Properties/AssemblyInfo.cs` | Assembly metadata |

## Deployed Mod Structure

```
D:\SteamLibrary\steamapps\common\RimWorld\Mods\NanoShieldArmor\
├── About\
│   ├── About.xml                  (packageId: Epsilonely.NanoTech)
│   └── Preview.png
├── Assemblies\
│   └── NanoTech.dll
├── Defs\
│   ├── HediffDefs\
│   │   └── NanoSuitProtection.xml
│   ├── ResearchProjectDefs\
│   │   └── ResearchProjects.xml
│   └── ThingDefs\
│       └── NanoTechApparel.xml    (NanoShieldSuit + NanoHelmet)
├── Languages\
│   └── Korean\
│       └── DefInjected\
│           ├── HediffDefs\NanoSuitProtection.xml
│           ├── ResearchProjectDefs\ResearchProjects.xml
│           └── ThingDefs\NanoShieldSuit.xml
├── Patches\
│   └── SoakingWet_Nullify.xml    (patches SoakingWet ThoughtDef)
└── Textures\
    └── Armor\
        ├── NanoShieldSuit\
        └── NanoHelmet\
```

## Build Configuration

- **Debug**: Full debug symbols, output to `NanoTech\bin\Debug\`
- **Release**: PDB debug info only, optimized, output to `NanoTech\bin\Release\`
- **Platform**: AnyCPU

## Dependencies

### Direct (System)
- `System`, `System.Core`, `System.Xml`, `System.Data`, `System.Net.Http`, `Microsoft.CSharp`

### RimWorld DLLs (HintPath: D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed\)
- `Assembly-CSharp.dll` — Verse, RimWorld namespaces
- `UnityEngine.dll`
- `UnityEngine.CoreModule.dll`
- `UnityEngine.IMGUIModule.dll`
- `UnityEngine.TextRenderingModule.dll`

> Note: Harmony (Lib.Harmony NuGet) was added then removed — not needed for current implementation.

## RimWorld API Surface Used

| API | Usage |
|---|---|
| `Apparel` | Base class for NanoShieldSuit, NanoHelmet |
| `ThingComp` | Base class for CompNanoShieldSuit |
| `CompProperties` | Base class for CompProperties_NanoShieldSuit |
| `Gizmo` | Base class for Gizmo_NanoShieldSuitStatus |
| `DamageDefOf.EMP` | Special case for EMP energy multiplier |
| `DamageInfo` | Passed through `PostPreApplyDamage` |
| `EffecterDefOf.Shield_Break` | Visual effects on shield break |
| `FleckMaker` | Dust/particle effects on break |
| `StatDefOf.EnergyShieldEnergyMax` | Max energy stat |
| `StatDefOf.EnergyShieldRechargeRate` | Recharge rate stat |
| `HediffDef.Named()` | Lookup for NanoSuitProtection Hediff |
| `Pawn_HealthTracker.AddHediff()` | Apply NanoSuitProtection on equip |
| `Pawn_HealthTracker.RemoveHediff()` | Remove NanoSuitProtection on unequip |

## Setup Requirements

1. Visual Studio 2022 (or MSBuild 17+)
2. RimWorld installed at `D:\SteamLibrary\steamapps\common\RimWorld\`
3. .NET Framework 4.8 SDK
4. Reference paths already configured in `.csproj`

## Deployment

Build `NanoTech\bin\Release\NanoTech.dll` and copy to `D:\SteamLibrary\steamapps\common\RimWorld\Mods\NanoShieldArmor\Assemblies\`.

> Post-build auto-copy event not yet configured.
