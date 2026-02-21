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
| `NanoTech/NanoShieldSuit.cs` | Main apparel class |
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
│   └── NanoShieldArmor.dll        ← 빌드 후 NanoTech.dll로 교체 필요
├── Defs\
│   ├── ResearchProjectDefs\
│   │   └── ResearchProjects.xml
│   └── ThingDefs\
│       └── NanoShieldArmor.xml    (thingClass: NanoTech.NanoShieldSuit)
├── Languages\
│   └── Korean\
│       └── DefInjected\
│           ├── ResearchProjectDefs\ResearchProjects.xml
│           └── ThingDefs\NanoShieldSuit.xml
└── Textures\
    └── Armor\NanoShieldSuit\      (NanoShieldSuit_*.png 다수)
```

## Build Configuration

- **Debug**: Full debug symbols, output to `NanoTech\bin\Debug\`
- **Release**: PDB debug info only, optimized, output to `NanoTech\bin\Release\`
- **Platform**: AnyCPU

## Dependencies

### Direct (System)
- `System`, `System.Core`, `System.Xml`, `System.Data`, `System.Net.Http`, `Microsoft.CSharp`

### RimWorld DLLs (HintPath: D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed\)
- `Assembly-CSharp.dll` — Verse, RimWorld 네임스페이스
- `UnityEngine.dll`
- `UnityEngine.CoreModule.dll`
- `UnityEngine.IMGUIModule.dll`
- `UnityEngine.TextRenderingModule.dll`

## RimWorld API Surface Used

| API | Usage |
|---|---|
| `Apparel` | Base class for NanoShieldSuit |
| `ThingComp` | Base class for CompNanoShieldSuit |
| `CompProperties` | Base class for CompProperties_NanoShieldSuit |
| `Gizmo` | Base class for Gizmo_NanoShieldSuitStatus |
| `DamageDefOf.EMP` | Special case for EMP energy multiplier |
| `DamageInfo` | Passed through `PostPreApplyDamage` |
| `EffecterDefOf.Shield_Break` | Visual effects on shield break |
| `FleckMaker` | Dust/particle effects on break |
| `StatDefOf.EnergyShieldEnergyMax` | Max energy stat |
| `StatDefOf.EnergyShieldRechargeRate` | Recharge rate stat |

## Setup Requirements

1. Visual Studio 2022 (or MSBuild 17+)
2. RimWorld installed at `D:\SteamLibrary\steamapps\common\RimWorld\`
3. .NET Framework 4.8 SDK
4. Reference paths already configured in `.csproj`

## Deployment

빌드 후 `NanoTech\bin\Release\NanoTech.dll`을 `D:\SteamLibrary\steamapps\common\RimWorld\Mods\NanoShieldArmor\Assemblies\`에 복사.
기존 `NanoShieldArmor.dll` 삭제 필요.

> Post-build 이벤트 자동 복사는 아직 미설정.
