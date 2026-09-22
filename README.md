# Unity Client Portfolio — 강우혁

Unity/C# 클라이언트 개발 경험을 공개 가능한 형태로 정리한 코드 포트폴리오입니다.

회사 프로젝트의 소스 코드는 포함하지 않습니다.
실무에서 해결한 문제와 사용한 개념을 독립적인 샘플 코드로 재구현했습니다.
Assets/HowlyDog_SelectedCode에는 개인 프로젝트 Howly Dog의 실제 코드 중 공개 가능한 일부만 포함합니다.

## Profile

- Unity Client Developer / Client Part Lead
- Unity / C# / uGUI / REST API / JSON / Firebase
- Unity Profiler 기반 메모리·성능 이슈 분석
- DOTween / RenderTexture / Runtime Texture Management
- iOS Build / TestFlight / App Store Connect
- 개인 게임 Howly Dog App Store 출시

## Samples

| Sample | 설명 | 실무 연결 |
| --- | --- | --- |
| GenericObjectPool<T> | 제네릭 기반 오브젝트 풀 | 타입별 중복 풀링 로직 공통화 |
| PopupStackController | Top Popup만 입력 허용 | 팝업 전환 중 뒤쪽 UI 중복 클릭 방지 |
| RemoteTextureStore | Reference-counted Texture Lease | 동적 이미지 메모리 생명주기/소유권 관리 |
| RestApiClient | JSON REST 요청 + 공통 결과 처리 | HTTPS API 연동 및 예외 처리 |
| RenderTexturePresenter | Camera → RenderTexture → RawImage | UI 내부 3D 오브젝트 표시 |
| YieldCache | Coroutine YieldInstruction 캐싱 | Howly Dog 실제 프로젝트에서 사용한 공개 코드 |

## Howly Dog

개인 개발 모바일 게임으로, 기획부터 Unity 클라이언트 구현, Firebase 연동, iOS 빌드 및 App Store 심사/출시까지 직접 진행했습니다.

App Store:
https://apps.apple.com/kr/app/%ED%95%98%EC%9A%B8%EB%A6%AC%EB%8F%84%EA%B7%B8/id6795132117

공개 범위는 전체 프로젝트가 아닌 선별 코드만 포함합니다.

## Repository Policy

- 회사/KLP/CatchU의 비공개 소스 코드 및 내부 데이터는 포함하지 않습니다.
- 샘플 코드는 실무에서 경험한 문제를 설명하기 위해 새로 작성한 코드입니다.
- Howly Dog는 개인 프로젝트이며 Assets/HowlyDog_SelectedCode의 일부 코드만 실제 프로젝트에서 선별했습니다.

자세한 설계 의도는 docs/ARCHITECTURE.md를 참고하세요.
