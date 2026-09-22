# Interview Notes

## Texture Memory

상품 이미지처럼 계속 새로운 URL이 들어오는 서비스에서는 재사용보다 신규 로드 비율이 높아 캐시가 사실상 장기 보관소처럼 동작할 수 있습니다.
Profiler에서 Texture 메모리가 지속 증가하는 것을 확인한 뒤 화면 오브젝트와 Texture의 생명주기를 연결하는 방향으로 개선했습니다.

같은 Texture를 여러 UI가 공유할 때 한 UI의 해제가 다른 UI를 깨뜨릴 수 있으므로 소유권과 참조 상태를 분리해야 합니다.
이 저장소의 TextureLease는 그 아이디어를 공개 가능한 형태로 재구현한 예시입니다.

## Popup Input

단순 클릭 딜레이가 아니라 현재 어떤 Popup이 입력의 소유자인지를 명시합니다.
Push 즉시 이전 Popup의 Raycast를 차단해 애니메이션 길이와 무관하게 입력 규칙을 유지합니다.

## AI-assisted Development

AI 도구는 구현 속도를 높이는 보조 수단으로 사용합니다.
실제 프로젝트에 반영하기 전 호출 흐름, 데이터 변경, 실패 케이스, 메모리 생명주기를 검증합니다.
Howly Dog에서는 클라이언트, Firebase Functions, 운영 도구를 연결하고 App Store 출시까지 수행했습니다.
