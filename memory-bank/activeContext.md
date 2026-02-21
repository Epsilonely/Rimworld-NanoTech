# Active Context: NanoTech

## Current Status

**Date**: 2026-02-21
**Phase**: 프로젝트 이름 변경 완료, 게임 내 테스트 준비 단계

## Recent Work

- **프로젝트 이름 변경** (NanoShieldArmor → NanoTech):
  - 솔루션: `NanoShieldArmor.sln` → `NanoTech.sln`
  - 프로젝트: `NanoShieldArmor.csproj` → `NanoTech.csproj`
  - 네임스페이스: `NanoShieldArmor` → `NanoTech`
  - 클래스명: `NanoShieldArmor` → `NanoShieldSuit`, `CompNanoShieldArmor` → `CompNanoShieldSuit` 등
  - AssemblyName: `NanoShieldArmor` → `NanoTech`

- **XML Defs 업데이트** (2026-02-21):
  - `NanoShieldArmor.xml`의 `thingClass`: `NanoShieldArmor.NanoShieldArmor` → `NanoTech.NanoShieldSuit`
  - `NanoShieldArmor.xml`의 `li Class`: `NanoShieldArmor.CompProperties_NanoShieldArmor` → `NanoTech.CompProperties_NanoShieldSuit`
  - `About.xml`: 이미 `NanoTech` 이름으로 설정됨 (packageId: Epsilonely.NanoTech)
  - `ResearchProjects.xml`: 이미 `NanoTech` 이름으로 설정됨

## Current Focus

XML 이름 변경 완료. 다음 단계는 빌드 후 DLL 교체 및 게임 내 테스트.

## Active Decisions / Open Questions

1. **DLL 교체** — 빌드 후 `NanoTech.dll`을 Assemblies 폴더에 넣고 기존 `NanoShieldArmor.dll` 삭제 필요
2. **Post-build 이벤트** — 자동 DLL 복사 설정 여부 미결정
3. **모드 폴더명** — 현재 `NanoShieldArmor`로 되어 있음 (필요 시 `NanoTech`로 변경 가능)

## Next Steps (Suggested)

1. VS에서 Release 빌드 실행 (`Ctrl+Shift+B`)
2. `NanoTech\bin\Release\NanoTech.dll` → `D:\SteamLibrary\steamapps\common\RimWorld\Mods\NanoShieldArmor\Assemblies\` 복사
3. 기존 `NanoShieldArmor.dll` 삭제
4. RimWorld 실행 → 게임 내 테스트
5. (선택) Post-build 이벤트로 자동 배포 설정
