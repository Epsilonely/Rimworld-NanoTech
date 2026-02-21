# Progress: NanoTech

## What Works

### C# Code (완성)
- [x] `NanoShieldSuit` apparel class — 네임스페이스 `NanoTech`
- [x] `CompNanoShieldSuit` component — 에너지 관리, 데미지 흡수, break/reset 로직
- [x] Shield belt conflict resolution — 인벤토리로 이동 (RemoveShieldBelt helper)
- [x] EMP damage multiplier (0.5x)
- [x] Visual shield bubble (`CompDrawWornExtras`)
- [x] Break effects (explosion flash, dust puffs)
- [x] Shake/impact visual on damage hit
- [x] Reset timer (startingTicksToReset = 1600)
- [x] Passive energy recharge (IsHashIntervalTick(60) 기반)
- [x] Save/load persistence (`PostExposeData`)
- [x] `Gizmo_NanoShieldSuitStatus` UI bar
- [x] `CompProperties_NanoShieldSuit` with XML-configurable properties
- [x] RimWorld 1.6 호환 (`protected override void Tick()`)
- [x] DEV gizmo commands (Break, Clear Reset)

### XML Defs (완성)
- [x] `About.xml` — packageId: Epsilonely.NanoTech, name: NanoTech
- [x] `ThingDefs/NanoShieldArmor.xml` — thingClass: NanoTech.NanoShieldSuit ✓
- [x] `ResearchProjectDefs/ResearchProjects.xml` — defName: NanoTech
- [x] `Languages/Korean/` — 한국어 번역 완료
- [x] 텍스처 — NanoShieldSuit 방향별 이미지 완비

### 이름 변경 (2026-02-21 완료)
- [x] 솔루션/프로젝트 파일명 → NanoTech
- [x] 네임스페이스 → NanoTech
- [x] 클래스명 → NanoShieldSuit, CompNanoShieldSuit 등
- [x] XML thingClass / compClass 참조 → NanoTech.* 로 수정

## Remaining Tasks

### 빌드 & 배포
- [ ] VS에서 Release 빌드 실행
- [ ] `NanoTech.dll` → `Assemblies/` 폴더 복사 (기존 `NanoShieldArmor.dll` 삭제)
- [ ] (선택) Post-build 이벤트로 자동 배포 설정

### 테스트
- [ ] 게임 내 테스트: 장착, 데미지 흡수, shield break/reset, gizmo 표시
- [ ] EMP 상호작용 테스트
- [ ] 밸런스 검토 (energyLossPerDamage=0.002, EnergyMax=1.8, reset=1600틱)

### Nice to Have
- [ ] README.md
- [ ] Steam Workshop 출시 설정

## Known Issues

| Issue | Severity | Notes |
|---|---|---|
| DLL 미교체 | Blocking | 빌드 후 Assemblies에 NanoTech.dll 넣고 NanoShieldArmor.dll 삭제 필요 |

## Status Summary

**Overall**: C# 코드 및 XML Defs 완성. 빌드 후 배포 및 게임 내 테스트만 남음.

**C# Code**: 100% complete
**XML Defs**: 100% complete
**Build & Deploy**: 0% (빌드 미실행)
**Testing**: 0%
