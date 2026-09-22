# Architecture Notes

## 1. Generic Object Pool

문제:
타입별로 거의 동일한 Pool 클래스를 만들면 스크립트가 늘어나고 수정 지점이 분산됩니다.

접근:
GenericObjectPool<T> 하나로 생성/대여/반환을 공통화합니다.

설명 포인트:
- Instantiate/Destroy 반복 비용 감소
- IPoolable로 Rent/Return 시 상태 초기화
- 중복 Return 방지를 위해 대여 상태 추적

## 2. Popup Stack Input Control

문제:
팝업 등장 애니메이션 중 기존 화면이 Raycast를 받으면 빠른 연속 입력으로 같은 팝업이 중복 생성될 수 있습니다.

접근:
스택의 Top Popup만 interactable과 blocksRaycasts를 활성화합니다.

## 3. Runtime Texture Lifecycle

문제:
계속 새로운 원격 이미지를 로드하는 서비스에서 모든 Texture를 장기 캐시하면 메모리가 누적될 수 있습니다.
반대로 공유 Texture를 한 UI가 임의로 Destroy하면 다른 UI가 깨질 수 있습니다.

접근:
TextureLease를 통해 참조 수와 해제 책임을 명시합니다.

## 4. REST API Client

문제:
각 UI에서 UnityWebRequest를 직접 생성하면 헤더, JSON, 실패 처리가 반복됩니다.

접근:
공통 Client에서 요청 생성, 직렬화, 응답 및 실패를 처리합니다.
상태 변경 요청에는 requestId를 전달해 서버 멱등성 처리와 연결할 수 있습니다.

## 5. RenderTexture 3D-in-UI

별도 Camera의 결과를 RenderTexture로 출력하고 RawImage에 연결합니다.
Runtime RenderTexture의 생성과 해제 책임을 컴포넌트 내부에서 관리합니다.

## Public / Private Boundary

- CatchU/KLP 등 회사 프로젝트 코드는 공개하지 않습니다.
- 회사 경험은 독립적인 샘플 코드로 재구현했습니다.
- Howly Dog는 개인 프로젝트이며 Assets/HowlyDog_SelectedCode의 일부 코드만 실제 프로젝트에서 선별했습니다.
