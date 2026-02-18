# System Patterns: NanoShieldArmor

## Architecture Overview

The mod follows RimWorld's standard **Component-Based architecture**. Logic is split across four C# classes:

```
NanoShieldArmor (Apparel)
  └── CompNanoShieldArmor (ThingComp)
        └── CompProperties_NanoShieldArmor (CompProperties)
  └── Gizmo_NanoShieldArmorStatus (Gizmo)
```

## Class Responsibilities

### `NanoShieldArmor : Apparel`
**File**: [NanoShieldArmor/NanoShieldArmor.cs](NanoShieldArmor/NanoShieldArmor.cs)

- Entry point for equipment lifecycle events
- `const ShieldBeltDefName` — `"Apparel_ShieldBelt"` 단일 상수로 관리
- `RemoveShieldBelt(Pawn, bool notify)` — 방어막 벨트 제거 헬퍼 (notify=true면 메시지 출력)
- `Notify_Equipped(Pawn)` — `RemoveShieldBelt()` 호출 (notify:true), 재착용 시 에너지 복원
- `Notify_Unequipped(Pawn)` — 탈착 시 에너지 저장 (`unequipEnergy`)
- `protected override void Tick()` — 60틱마다 `RemoveShieldBelt()` 호출 (notify:false, 엣지케이스 대응)
- `CheckPreAbsorbDamage()` — 방어막 벨트 착용 중이면 데미지 흡수 비활성화
- `ExposeData()` — `unequipEnergy`, `wasUnequipped`만 저장 (shield 상태는 Comp가 담당)

### `CompNanoShieldArmor : ThingComp`
**File**: [NanoShieldArmor/CompNanoShieldArmor.cs](NanoShieldArmor/CompNanoShieldArmor.cs)

- **Core shield logic** — energy management, damage absorption, reset timer
- **Public API**: `Energy` (get/set), `IsBroken` (get/set), `TicksToReset` (get/set), `EnergyMax` (readonly)
- `PostExposeData()` — `energy`, `broken`, `ticksToReset` 저장/로드 (컴포넌트가 직접 담당)
- `CompTick()` — broken 시 reset 카운트다운; 아닐 때 `IsHashIntervalTick(60)`마다 에너지 충전
- `PostPreApplyDamage()` — 데미지 흡수; EMP는 0.5x; 에너지 0 이하 시 `Break()` 호출
- `Break()` — 이펙트 재생 (`Mathf.Clamp01(energy / EnergyMax)`로 안전한 scale 계산), broken 상태 설정
- `Reset()` — `energyOnReset`으로 복구, broken 해제
- `CompDrawWornExtras()` — 쉴드 버블 렌더링 (에너지 비례 크기, 피격 시 흔들림)

### `CompProperties_NanoShieldArmor : CompProperties`
**File**: [NanoShieldArmor/CompProperties_NanoShieldArmor.cs](NanoShieldArmor/CompProperties_NanoShieldArmor.cs)

- Data-only class; all values set from XML Defs
- Key properties:

| Property | Default | Description |
|---|---|---|
| `startingTicksToReset` | 1600 | 쉴드 파괴 후 리셋까지 틱 수 (~27초) |
| `minDrawSize` | 1.2f | 쉴드 버블 최소 반경 |
| `maxDrawSize` | 1.55f | 쉴드 버블 최대 반경 |
| `energyLossPerDamage` | 0.001f | 데미지 1당 에너지 소모량 |
| `energyOnReset` | 0.5f | 리셋 시 복구 에너지 (0~1 범위) |

### `Gizmo_NanoShieldArmorStatus : Gizmo`
**File**: [NanoShieldArmor/Gizmo_NanoShieldArmorStatus.cs](NanoShieldArmor/Gizmo_NanoShieldArmorStatus.cs)

- UI-only; reads energy from `CompNanoShieldArmor`
- Renders a fillable bar showing shield energy percentage
- No game logic; purely presentational

## Key Design Patterns

### 1. RimWorld Component Pattern
- `ThingComp` + `CompProperties` pair is the standard RimWorld extension point
- Properties defined in XML Defs, behavior in C# component

### 2. Delegation Pattern
- `NanoShieldArmor` (Apparel) delegates all shield logic to `CompNanoShieldArmor`
- Apparel class is a thin lifecycle adapter

### 3. Data Persistence
- `NanoShieldArmor.ExposeData()` — Apparel 자체 필드(`unequipEnergy`, `wasUnequipped`)만 저장
- `CompNanoShieldArmor.PostExposeData()` — 컴포넌트가 자신의 상태(`energy`, `broken`, `ticksToReset`) 직접 저장
- RimWorld 컴포넌트 시스템이 `PostExposeData()`를 자동 호출하므로 Apparel에서 중복 저장 불필요

### 4. Shield Belt Conflict Resolution
```csharp
// const로 defName 관리
private const string ShieldBeltDefName = "Apparel_ShieldBelt";

// 헬퍼 메서드로 중복 제거
private void RemoveShieldBelt(Pawn pawn, bool notify) { ... }

// Notify_Equipped: 메시지 출력
RemoveShieldBelt(pawn, notify: true);

// Tick (60틱마다, 엣지케이스): 조용히 제거
RemoveShieldBelt(Wearer, notify: false);
```

### 5. EMP Special Casing
```csharp
float energyLoss = dinfo.Def == DamageDefOf.EMP
    ? dinfo.Amount * Props.energyLossPerDamage * 0.5f
    : dinfo.Amount * Props.energyLossPerDamage;
```

### 6. Safe Scale Calculation on Break
```csharp
// Break() 호출 시점에 energy는 이미 음수 → Clamp01 필수
float scale = Mathf.Lerp(Props.minDrawSize, Props.maxDrawSize, Mathf.Clamp01(energy / EnergyMax));
```

### 7. Performance — Throttled Stat Lookup
```csharp
// CompTick()에서 매 틱 GetStatValue() 호출 방지
if (parent.IsHashIntervalTick(60))
{
    float rechargeRate = parent.GetStatValue(StatDefOf.EnergyShieldRechargeRate);
    energy += rechargeRate;
}
```

## Namespace

All classes use namespace: `NanoShieldArmor`
