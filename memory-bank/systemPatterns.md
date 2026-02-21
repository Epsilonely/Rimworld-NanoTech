# System Patterns: NanoTech

## Architecture Overview

The mod follows RimWorld's standard **Component-Based architecture**. Logic is split across four C# classes:

```
NanoShieldSuit (Apparel)
  └── CompNanoShieldSuit (ThingComp)
        └── CompProperties_NanoShieldSuit (CompProperties)
  └── Gizmo_NanoShieldSuitStatus (Gizmo)
```

## Namespace

All classes use namespace: `NanoTech`

## Class Responsibilities

### `NanoShieldSuit : Apparel`
**File**: NanoTech/NanoShieldSuit.cs

- `const ShieldBeltDefName = "Apparel_ShieldBelt"`
- `RemoveShieldBelt(Pawn, bool notify)` — 벨트를 인벤토리로 이동 (notify=true면 메시지 출력)
- `SpawnSetup()` — 첫 스폰 시 에너지 최대로 설정
- `Notify_Equipped(Pawn)` — RemoveShieldBelt 호출, 재착용 시 에너지 복원
- `Notify_Unequipped(Pawn)` — 탈착 시 에너지 저장
- `protected override void Tick()` — 60틱마다 RemoveShieldBelt 호출 (엣지케이스)
- `CheckPreAbsorbDamage()` — 벨트 착용 중이면 데미지 흡수 비활성화
- `ExposeData()` — unequipEnergy, wasUnequipped 저장

### `CompNanoShieldSuit : ThingComp`
**File**: NanoTech/CompNanoShieldSuit.cs

- Public API: `Energy`, `IsBroken`, `TicksToReset`, `EnergyMax`, `IsApparel`
- `PostExposeData()` — energy, broken, ticksToReset 저장/로드
- `CompTick()` — broken 시 reset 카운트다운; IsHashIntervalTick(60)마다 에너지 충전
- `PostPreApplyDamage()` — 데미지 흡수; EMP 0.5x; 에너지 0 이하 시 Break()
- `Break()` — Clamp01(energy/EnergyMax)로 안전한 scale 계산, 이펙트, broken 설정
- `Reset()` — energyOnReset으로 복구
- `CompDrawWornExtras()` — 쉴드 버블 렌더링 (에너지 비례 크기, 피격 시 흔들림)
- `CompGetWornGizmosExtra()` / `CompGetGizmosExtra()` — Gizmo 제공 + DEV 커맨드

### `CompProperties_NanoShieldSuit : CompProperties`
**File**: NanoTech/CompProperties_NanoShieldSuit.cs

| Property | Default | Description |
|---|---|---|
| `startingTicksToReset` | 1600 | 쉴드 파괴 후 리셋까지 틱 수 |
| `minDrawSize` | 1.2f | 쉴드 버블 최소 반경 |
| `maxDrawSize` | 1.55f | 쉴드 버블 최대 반경 |
| `energyLossPerDamage` | 0.001f | 데미지 1당 에너지 소모 |
| `energyOnReset` | 0.5f | 리셋 시 복구 에너지 (0~1) |

### `Gizmo_NanoShieldSuitStatus : Gizmo`
**File**: NanoTech/Gizmo_NanoShieldSuitStatus.cs

- UI-only; 에너지 fillable bar 표시 (현재값 / 최대값)

## XML ↔ C# 연결

| XML 값 | C# 타입 |
|---|---|
| `<thingClass>NanoTech.NanoShieldSuit</thingClass>` | `NanoShieldSuit : Apparel` |
| `<li Class="NanoTech.CompProperties_NanoShieldSuit">` | `CompProperties_NanoShieldSuit` |

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
