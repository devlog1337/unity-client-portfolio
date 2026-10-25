Here is the result of "fetch" for the Page with URL https://app.notion.com/p/3e94e725f3128156b1e2dd5d1424b91e as of 2026-09-28T06:15:50.718Z:
<page url="https://app.notion.com/p/3e94e725f3128156b1e2dd5d1424b91e" icon="🎯">
<ancestor-path>
<parent-page url="https://app.notion.com/p/3dc4e725f3128006b157f997f907d631" title="Daily Study"/>
</ancestor-path>
<properties>
{"title":"슈퍼센트 Unity 클라이언트 면접 완전 암기 학습지"}
</properties>
<iconMetadata>{"type":"emoji","emoji":"🎯"}</iconMetadata>
<content>
<callout icon="🎯" color="blue_bg">
<b>목표:</b> 이 페이지를 보지 않고 설명할 수 있을 정도로 암기한다. 단어만 외우는 것이 아니라 <b>정의 → 왜 쓰는지 → 장단점 → 내 경험</b> 순서로 말할 수 있어야 한다.

<b>기준:</b> 슈퍼센트 Unity 클라이언트 경력직에서 요구될 가능성이 높은 C#, 자료구조/알고리즘, Unity 최적화, 메모리/GC, 아키텍처, 모바일, Git/CI/CD, AI 개발도구, 프로젝트 경험 설명을 한 번에 대비한다.
</callout>

## 0. 면접 답변 공식 — 무조건 이것부터 암기

### 0-1. 기술 질문 = 정·이·원·장·경

- <b>정의:</b> 무엇인가?
- <b>이유:</b> 왜 사용하는가?
- <b>원리:</b> 내부적으로 어떻게 동작하는가?
- <b>장단점:</b> 어떤 비용과 trade-off가 있는가?
- <b>경험:</b> 실제 프로젝트에서 어디에 썼는가?

<b>암기 문장</b>

> 기술을 설명할 때는 “무엇인지”만 말하지 않는다. <b>왜 선택했고, 어떤 비용이 있으며, 실제로 어떤 문제를 해결했는지</b>까지 말한다.

### 0-2. 경험 질문 = 문·원·판·구·결·회

- <b>문:</b> 문제가 무엇이었나.
- <b>원:</b> 원인을 어떻게 찾았나.
- <b>판:</b> 왜 그 해결책을 선택했나.
- <b>구:</b> 어떻게 구현했나.
- <b>결:</b> 결과가 어떻게 바뀌었나.
- <b>회:</b> 다시 한다면 무엇을 개선할 것인가.

### 0-3. 모르는 질문 대응

<b>암기 답변</b>

> 정확한 내부 구현까지는 확신하기 어렵습니다. 제가 알고 있는 범위에서는 ○○로 이해하고 있습니다. 실제로 확인해야 한다면 Unity Profiler, 공식 문서 또는 최소 재현 프로젝트로 검증하겠습니다.

<b>금지:</b> 아는 척, 장황한 추측, 질문과 상관없는 경험 늘어놓기.

---

<toggle heading="h2">
1\. C# 핵심 기본기

- [ ] 아래 항목을 전부 30초 이내 설명 가능

### 1-1. 값 형식과 참조 형식

<b>값 형식:</b> <code>struct</code>, <code>int</code>, <code>float</code>, <code>bool</code>, <code>enum</code> 등. 값을 직접 가진다.

<b>참조 형식:</b> <code>class</code>, <code>string</code>, 배열, delegate 등. 객체를 가리키는 참조를 가진다.

<b>중요:</b> “값 형식은 무조건 Stack, 참조 형식은 무조건 Heap”이라고 말하면 위험하다. 저장 위치는 컨텍스트와 런타임 최적화에 따라 달라질 수 있다. 핵심 차이는 <b>복사 의미와 값/참조 semantics</b>다.

<b>면접 답변</b>

> 값 형식은 대입 시 값 자체가 복사되고, 참조 형식은 객체를 가리키는 참조가 복사됩니다. Unity에서는 큰 struct를 반복 복사하면 비용이 생길 수 있고, class 객체를 반복 생성하면 managed allocation과 GC 비용을 고려해야 합니다.

### 1-2. class vs struct

- <code>class</code>: 참조 semantics, 상속 가능, null 가능.
- <code>struct</code>: 값 semantics, 작은 데이터 묶음에 적합, 상속 불가(인터페이스 구현 가능).
- 큰 struct는 복사 비용이 커진다.
- mutable struct는 예측하기 어려운 버그를 만들 수 있어 주의한다.

<b>꼬리질문:</b> Vector3는 왜 struct인가?

> 좌표처럼 작고 독립적인 값 객체이며 값 복사가 자연스럽기 때문이다. 다만 반복문에서 불필요한 복사가 발생하지 않는지도 고려한다.

### 1-3. Boxing / Unboxing

<b>Boxing:</b> 값 형식을 <code>object</code>나 인터페이스 타입으로 감싸 heap 객체처럼 다루는 과정.

<b>Unboxing:</b> boxed object에서 값 형식으로 다시 꺼내는 과정.

<b>왜 중요?</b> 반복되는 Boxing은 allocation과 GC 압력을 만들 수 있다.

<b>예시:</b> non-generic collection, object 기반 API, interface 사용 상황에서 값 형식 boxing 여부 확인.

### 1-4. string

- <code>string</code>은 참조 형식이지만 <b>immutable</b>.
- 문자열 변경처럼 보여도 실제로는 새로운 문자열이 생성될 수 있다.
- 반복 연결은 <code>StringBuilder</code> 고려.
- Update에서 문자열 포맷을 매 프레임 갱신하면 allocation 발생 여부를 확인한다.

### 1-5. const vs readonly

- <code>const</code>: 컴파일 타임 상수, 선언과 동시에 값 결정.
- <code>readonly</code>: 런타임에 생성자에서 결정 가능, 이후 변경 불가.

### 1-6. interface vs abstract class

<b>interface:</b> 계약/역할을 정의. 여러 인터페이스 구현 가능.

<b>abstract class:</b> 공통 상태와 구현을 공유하면서 확장 지점을 제공.

<b>면접 답변</b>

> 공통 데이터와 기본 구현까지 공유해야 하면 abstract class, 서로 다른 계층의 객체에 동일한 역할 계약만 부여하려면 interface를 우선 고려합니다.

### 1-7. virtual / override / new

- <code>virtual</code>: 자식이 재정의 가능.
- <code>override</code>: 부모 virtual/abstract 멤버 재정의.
- <code>new</code>: 부모 멤버를 숨김. 다형성 관점에서 혼동 가능하므로 의도를 명확히 해야 함.

### 1-8. delegate / Action / Func / event

- <code>delegate</code>: 메서드를 값처럼 참조하는 타입.
- <code>Action</code>: 반환값 없음.
- <code>Func</code>: 마지막 generic 인자가 반환형.
- <code>event</code>: 외부에서 임의 호출/대입을 제한한 delegate 기반 발행 구조.

<b>Unity 실무:</b> UI 버튼, 상태 변경 알림, 이벤트 기반 decoupling.

<b>주의:</b> 이벤트 구독 해제 누락 → 객체가 예상보다 오래 살아있거나 중복 호출 발생.

### 1-9. IEnumerable / IEnumerator

- <code>IEnumerable<T></code>: 순회 가능한 컬렉션을 표현.
- <code>IEnumerator<T></code>: 현재 위치를 가지고 실제 순회를 수행.
- <code>foreach</code>는 내부적으로 Enumerator 패턴을 사용한다.

### 1-10. LINQ 지연 실행

<code>Where</code>, <code>Select</code> 등은 보통 즉시 결과 컬렉션을 만들지 않고 <b>열거 시점에 실행</b>된다.

<code>ToList</code>, <code>ToArray</code>로 materialize하면 그 시점에 평가된다.

<b>면접 핵심</b>

> LINQ는 표현력이 좋지만 Unity hot path에서 delegate, iterator, allocation 비용이 생기는지 측정해야 합니다. 가독성 때문에 무조건 금지하거나, 편하다고 무조건 사용하는 둘 다 피합니다.

### 1-11. ref / out / in

- <code>ref</code>: 이미 초기화된 변수를 참조 전달, 읽기/쓰기.
- <code>out</code>: 메서드가 값을 반드시 할당해서 반환하는 용도.
- <code>in</code>: readonly reference 전달, 큰 struct의 복사를 피할 때 고려.

### 1-12. Generic

<b>목적:</b> 타입 안정성과 코드 재사용.

<b>경험 연결:</b> pooling/generic 구조 통합 경험.

<b>면접 답변</b>

> 타입별로 중복되던 풀링 로직을 generic으로 통합하면 공통 생명주기와 관리 규칙을 한 곳에 둘 수 있습니다. 다만 지나친 추상화로 호출 흐름이 숨겨지지 않도록 실제 공통 책임이 있는 경우에만 일반화합니다.

### 1-13. Exception

- 예외는 비정상 흐름 처리용이지 일반 분기용이 아니다.
- <code>try/catch</code> 자체보다 throw가 발생했을 때 stack trace 생성 등 비용이 크다.
- 네트워크/파일/외부 입력 경계에서는 적절한 예외 처리가 필요.

### 1-14. async / await

- <code>async/await</code>는 비동기 코드를 순차적으로 읽히게 만드는 문법.
- <code>Task</code>가 곧 새 Thread라는 뜻은 아니다.
- Unity API는 기본적으로 Main Thread 접근을 전제로 하는 것이 많다.
- 취소는 <code>CancellationToken</code>, 실패는 예외/결과 타입, 중복 실행 방지가 필요.

<b>Coroutine과 차이</b>

> Coroutine은 Unity PlayerLoop에 맞춰 프레임 단위로 작업을 나눠 실행하는 구조이고 Thread가 아닙니다. async/await는 Task 기반 비동기 흐름 표현에 적합합니다. 둘 다 목적과 취소/생명주기를 고려해서 선택합니다.
</toggle>

---

<toggle heading="h2">
2\. 컬렉션과 시간복잡도

- [ ] 자료구조를 보고 평균 시간복잡도와 사용 이유를 말할 수 있음

<table fit-page-width="true" header-row="true">
<tr>
<td>
자료구조
</td>
<td>
핵심
</td>
<td>
대표 복잡도
</td>
<td>
사용 상황
</td>
</tr>
<tr>
<td>
Array
</td>
<td>
고정 크기, 연속 인덱스
</td>
<td>
index O(1), 검색 O(N)
</td>
<td>
크기가 고정되고 빠른 index 접근
</td>
</tr>
<tr>
<td>
List
</td>
<td>
가변 배열
</td>
<td>
index O(1), 뒤 Add 평균 O(1), 중간 삽입/삭제 O(N)
</td>
<td>
순서 있는 가변 목록
</td>
</tr>
<tr>
<td>
Dictionary
</td>
<td>
Hash 기반 key-value
</td>
<td>
평균 검색/추가 O(1)
</td>
<td>
ID→데이터, 카운트, 캐시
</td>
</tr>
<tr>
<td>
HashSet
</td>
<td>
중복 없는 Hash 집합
</td>
<td>
평균 검색/추가 O(1)
</td>
<td>
중복 확인, 방문 체크
</td>
</tr>
<tr>
<td>
Stack
</td>
<td>
LIFO
</td>
<td>
Push/Pop O(1)
</td>
<td>
되돌리기, DFS, popup stack
</td>
</tr>
<tr>
<td>
Queue
</td>
<td>
FIFO
</td>
<td>
Enqueue/Dequeue O(1)
</td>
<td>
BFS, 순차 작업
</td>
</tr>
<tr>
<td>
PriorityQueue
</td>
<td>
우선순위 기준
</td>
<td>
삽입/삭제 O(log N)
</td>
<td>
최소/최대 우선 처리
</td>
</tr>
</table>

### Dictionary 원리

- key의 hash code를 이용해 bucket 후보를 찾는다.
- 충돌이 발생할 수 있어 equality 비교가 필요하다.
- custom key는 <code>Equals</code>와 <code>GetHashCode</code> 일관성이 중요하다.

<b>꼬리질문: Dictionary가 항상 O(1)인가?</b>

> 평균적으로 O(1)이고 최악은 충돌 상황에 따라 악화될 수 있습니다. 그래서 hash 품질과 key의 equality 구현이 중요합니다.

### List에서 Remove가 왜 O(N)?

> 중간 요소를 제거하면 뒤의 요소들을 당겨 순서를 유지해야 하기 때문입니다.
</toggle>

---

<toggle heading="h2">
3\. 알고리즘 / 코딩테스트 핵심

- [ ] 문제 키워드만 보고 풀이 패턴 선택 가능

### 3-1. 문제를 받자마자 5단계

1. 입력 크기 확인.
2. 시간복잡도 상한 추정.
3. 필요한 자료구조 선택.
4. 작은 예제로 손으로 검증.
5. 경계값/중복/빈 입력/overflow 확인.

### 3-2. 암기 매핑

- 중복 확인 → <code>HashSet</code>
- 값/횟수/ID 매핑 → <code>Dictionary</code>
- 정렬 후 양끝/구간 → Two Pointer
- 연속 구간 → Sliding Window
- 구간 합 반복 → Prefix Sum
- 정렬된 탐색 공간 → Binary Search
- 연결 영역/완전 탐색 → DFS/BFS
- 가중치 없는 최단거리 → BFS
- 중복 부분 문제 → DP/Memoization
- 모든 선택 → Backtracking
- 매 순간 최선이 전체 최적임을 증명 가능 → Greedy
- 최소값/최대값을 계속 꺼냄 → Heap/PriorityQueue

### 3-3. DFS vs BFS

<b>DFS:</b> 깊게 탐색. 재귀/Stack. 연결 요소, 경로 탐색, 백트래킹.

<b>BFS:</b> 가까운 것부터 탐색. Queue. 가중치가 같은 최단거리.

<b>중요:</b> 방문 처리는 보통 <b>Queue/Stack에 넣는 순간</b> 해서 중복 삽입을 막는다.

### 3-4. Binary Search

조건: 탐색 기준이 단조성을 가져야 한다.

<b>암기 코드</b>

```cs
int left = 0;
int right = arr.Length - 1;
while (left <= right)
{
    int mid = left + (right - left) / 2;
    if (arr[mid] == target) return mid;
    if (arr[mid] < target) left = mid + 1;
    else right = mid - 1;
}
return -1;
```

### 3-5. Two Sum 패턴

```cs
var map = new Dictionary<int, int>();
for (int i = 0; i < nums.Length; i++)
{
    int need = target - nums[i];
    if (map.TryGetValue(need, out int index))
        return new[] { index, i };
    map[nums[i]] = i;
}
```

<b>복잡도:</b> 시간 O(N), 공간 O(N).

### 3-6. BFS 기본형

```cs
var q = new Queue<int>();
var visited = new bool[n];
q.Enqueue(start);
visited[start] = true;
while (q.Count > 0)
{
    int cur = q.Dequeue();
    foreach (var next in graph[cur])
    {
        if (visited[next]) continue;
        visited[next] = true;
        q.Enqueue(next);
    }
}
```

### 3-7. 재귀 설명

재귀에는 반드시:

- 종료 조건
- 문제 크기 감소
- 필요 시 상태 복구(backtracking)

가 있어야 한다.
</toggle>

---

<toggle heading="h2">
4\. Unity 생명주기 / 기본 구조

- [ ] Awake/OnEnable/Start/Update/FixedUpdate/LateUpdate/OnDisable/OnDestroy를 설명 가능

### 4-1. 핵심 생명주기

- <code>Awake</code>: 자기 자신의 참조 캐싱, 내부 초기화.
- <code>OnEnable</code>: 활성화될 때마다 실행. 이벤트 구독 위치로 자주 사용.
- <code>Start</code>: 첫 Update 전에 한 번. 다른 객체 초기화 이후 필요한 연결에 사용 가능.
- <code>Update</code>: 매 프레임 일반 로직/입력.
- <code>FixedUpdate</code>: 고정 timestep, Rigidbody 물리 연산.
- <code>LateUpdate</code>: Update 이후, 카메라 추적 등.
- <code>OnDisable</code>: 비활성화 시. 이벤트 구독 해제에 자주 사용.
- <code>OnDestroy</code>: 파괴 직전 최종 정리.

<b>주의:</b> Script Execution Order에 과도하게 의존하면 결합도가 높아질 수 있다.

### 4-2. Update vs FixedUpdate

> 입력은 Update에서 읽고, Rigidbody 물리 반영은 FixedUpdate에서 처리하는 것이 일반적입니다. FixedUpdate는 렌더 프레임과 1:1이 아닙니다.

### 4-3. deltaTime

프레임 수가 아니라 <b>시간 기반</b>으로 움직이기 위해 사용한다.

<code>position += direction * speed * Time.deltaTime</code>.

### 4-4. MonoBehaviour

Unity 엔진 생명주기, GameObject/Component 시스템에 연결되는 기본 클래스.

모든 로직을 MonoBehaviour에 넣지 말고 순수 C# 객체로 분리하면 테스트성과 재사용성이 좋아진다.

### 4-5. ScriptableObject

GameObject에 붙지 않는 UnityEngine.Object 데이터 컨테이너.

공유 설정/마스터 데이터/에디터 데이터에 유용.

<b>주의:</b> 런타임 상태를 asset에 잘못 저장하면 에디터에서 값이 남거나 공유 상태 문제를 만들 수 있다.

### 4-6. SerializeField

private 필드를 Inspector에 노출하면서 외부 코드의 직접 접근은 막을 수 있다.

### 4-7. Collider / Rigidbody / Trigger

- Collider: 충돌 영역.
- Rigidbody: 물리 시뮬레이션 참여.
- Trigger: 물리적으로 밀어내기보다 이벤트 감지.

### 4-8. Coroutine

- Thread가 아니다.
- Unity PlayerLoop에서 실행을 양보했다가 조건이 되면 이어서 수행.
- <code>WaitForSeconds</code>, <code>yield return null</code> 등.
- 객체 disable/destroy와 Coroutine 생명주기를 고려해야 한다.
</toggle>

---

<toggle heading="h2">
5\. Unity 메모리와 GC — 최우선

- [ ] Profiler에서 GC.Alloc 원인을 추적하는 절차를 설명 가능

### 5-1. Managed vs Native

Unity 앱 메모리는 단순히 C# heap 하나가 아니다.

- <b>Managed memory:</b> C# 객체, GC가 관리.
- <b>Native memory:</b> Texture, Mesh, AudioClip 등 Unity native 엔진 리소스.
- GPU 메모리 등도 별도 고려.

<b>핵심:</b> C# 참조가 사라졌다고 Texture native memory가 즉시 원하는 시점에 반환된다고 단정하면 안 된다. UnityEngine.Object 생명주기와 로드 시스템의 release 규칙을 확인한다.

### 5-2. GC가 문제인 이유

managed allocation이 누적되면 GC가 불필요 객체를 탐색/회수한다. 이 과정에서 frame spike가 발생할 수 있다.

### 5-3. GC.Alloc 대표 원인

- 매 프레임 <code>new</code>
- 문자열 연결/포맷
- LINQ
- closure/lambda capture
- boxing
- 일부 Enumerator/API
- 배열/컬렉션의 반복 생성

<b>면접 한 줄</b>

> GC 최적화는 “GC를 없애는 것”이 아니라 <b>hot path에서 반복 allocation을 줄이고 실제 Profiler로 spike 원인을 확인하는 것</b>입니다.

### 5-4. Object Pooling

반복 <code>Instantiate/Destroy</code> 비용을 줄이기 위해 객체를 재사용.

<b>장점:</b> 생성/파괴 비용 감소, allocation 감소, spike 완화.

<b>단점:</b> 풀 크기/상태 초기화/미사용 메모리/복잡도 증가.

<b>중요:</b> Pooling은 만능이 아니다. 생성 빈도가 낮은 객체까지 풀링하면 오히려 복잡도만 늘어난다.

### 5-5. Texture 메모리

텍스처 메모리는 해상도, 포맷, mipmap 등에 크게 영향받는다.

예: 무압축 RGBA32는 대략 <code>width × height × 4 bytes</code> 수준의 기본 메모리가 필요하고 mipmap이 있으면 더 증가한다.

### 5-6. 내 실전 사례 — 이미지 메모리 크래시

<b>문제:</b> 동적 이미지 사용 중 메모리 사용량이 누적되어 크래시 발생.

<b>원인 확인:</b> 동일/다수 이미지 리소스의 생명주기와 release 시점을 추적.

<b>판단:</b> 이미지별 고유 key로 소유/캐시 상태를 추적하고 객체 파괴 시 release하도록 변경.

<b>구현:</b> unique key 기반 관리 + <code>OnDestroy</code>/명시적 release 경로 정리.

<b>결과:</b> 수정 후 크래시가 사라짐.

<b>면접 답변 예시</b>

> 동적으로 로드한 이미지가 화면 전환 후에도 해제되지 않아 메모리가 누적되는 문제가 있었습니다. 먼저 재현 구간에서 메모리 증가 패턴을 확인했고, 이미지마다 고유 키를 부여해 참조와 해제 시점을 추적하도록 바꿨습니다. 객체 생명주기가 끝날 때 release가 반드시 호출되도록 정리한 뒤 동일 시나리오를 반복 테스트했고 크래시가 사라졌습니다. 이 경험 이후에는 로드 기능을 만들 때 load보다 ownership과 release 규칙을 먼저 설계합니다.

<b>예상 꼬리질문</b>

- 왜 GC만 돌리면 해결되지 않나?
- 누가 Texture의 owner인가?
- reference count를 쓰면 어떤 문제가 생길 수 있나?
- cache eviction 기준은 어떻게 잡을 것인가?
</toggle>

---

<toggle heading="h2">
6\. Unity 성능 최적화

- [ ] CPU/GPU/Memory 병목을 구분하는 순서를 설명 가능

### 6-1. 최적화 원칙

<b>측정 → 병목 분류 → 원인 좁히기 → 수정 → 동일 조건 재측정.</b>

> 최적화는 추측으로 하지 않습니다. Unity Profiler와 실제 타깃 디바이스를 기준으로 CPU, Rendering, Memory, GC Alloc을 먼저 구분합니다.

### 6-2. Update 비용

많은 MonoBehaviour가 개별 Update를 가지면 호출 오버헤드가 누적될 수 있다.

- 필요 없는 Update 제거.
- 이벤트 기반 전환.
- 주기 낮춰도 되는 작업은 매 프레임 하지 않기.

### 6-3. GetComponent

한 번 찾으면 되는 참조를 Update에서 반복 검색하지 말고 캐싱.

단, “GetComponent는 절대 쓰면 안 된다”가 아니라 호출 빈도와 구간이 중요하다.

### 6-4. Instantiate / Destroy

빈번한 생성/파괴는 CPU, allocation, native object 관리 비용을 만들 수 있다. 반복 객체는 Pooling 검토.

### 6-5. Draw Call

CPU가 GPU에 렌더 명령을 전달하는 단위 비용.

Material/상태 변경, 오브젝트 수 등에 영향을 받는다.

관련 개념: batching, instancing, SRP Batcher.

### 6-6. Overdraw

같은 픽셀이 여러 번 그려지는 현상. 모바일 UI/투명 오브젝트에서 특히 비용이 커질 수 있다.

### 6-7. Canvas Rebuild

Unity UI에서 Canvas 내 요소 변경이 layout/geometry rebuild를 유발할 수 있다.

- 자주 변하는 UI와 정적인 UI 분리.
- LayoutGroup/ContentSizeFitter 남용 확인.
- 전체 Canvas가 불필요하게 dirty되는지 확인.

### 6-8. UI 3D 표시 경험

<b>문제:</b> 일반 UI 위에 3D 오브젝트를 안정적으로 표현해야 함.

<b>해결:</b> 별도 Camera → RenderTexture → RawImage, DOTween으로 표현 제어.

<b>면접 포인트:</b> RenderTexture의 해상도/메모리 비용, 카메라 렌더 비용, 갱신 빈도까지 말할 것.
</toggle>

---

<toggle heading="h2">
7\. Asset / 비동기 로딩 / 캐시

- [ ] Addressables의 Load보다 Release가 중요하다는 이유를 설명 가능

### 7-1. Resources의 한계

간단하지만 규모가 커지면 빌드 포함/관리/메모리/업데이트 전략이 불편해질 수 있다.

### 7-2. AssetBundle

에셋을 별도 번들로 묶어 런타임 로드 가능. dependency와 unload 관리가 중요.

### 7-3. Addressables

Address/label 기반 비동기 asset 관리 계층. 내부적으로 AssetBundle 등을 활용할 수 있다.

<b>핵심:</b> handle/참조 관리, release, 실패, 중복 요청, 캐시 정책.

### 7-4. 캐시 정책

캐시는 “저장한다”보다 다음 질문이 중요하다.

- key는 무엇인가?
- invalidation은 언제 하나?
- 최대 용량은?
- 오래된 데이터 eviction은?
- 실패하면 fallback은?

### 7-5. 내 경험 — 원격 Texture 캐시

<b>상황:</b> 캐릭터/배너 이미지가 서버 마스터데이터와 함께 증가하면서 매 실행마다 전부 다운로드하면 네트워크/로딩 비용이 커질 수 있었음.

<b>방향:</b> 필요한 시점 로드 + 캐시, 보유/랭킹/프로필 등 사용 지점 중심, 미보유는 default image.

<b>면접 핵심:</b> 무조건 prefetch가 아니라 실제 사용 패턴에 맞춰 eager/lazy loading을 선택했다는 점.
</toggle>

---

<toggle heading="h2">
8\. 아키텍처 / 설계

- [ ] SOLID 각 항목을 한 문장으로 설명하고 실제 예를 들 수 있음

### 8-1. SRP

한 클래스가 변경되는 이유를 하나의 책임 축으로 제한.

### 8-2. OCP

기존 코드를 반복 수정하기보다 확장으로 새로운 동작을 추가할 수 있게 설계.

### 8-3. LSP

자식 타입이 부모 타입의 계약을 깨지 않고 대체 가능해야 한다.

### 8-4. ISP

사용하지 않는 거대한 인터페이스보다 역할별 작은 인터페이스.

### 8-5. DIP

상위 정책이 구체 구현에 직접 강결합되지 않고 추상에 의존.

### 8-6. MVC / MVVM

<b>MVC:</b> Model, View, Controller 책임 분리.

<b>MVVM:</b> View와 ViewModel 사이 binding 중심. UI 상태와 표현 로직 분리에 유리.

<b>중요:</b> 패턴 이름을 쓰는 것이 목적이 아니다. 변경 이유가 다른 코드를 분리하고 테스트/확장 비용을 낮추는 것이 목적.

### 8-7. ECS

Entity = ID, Component = 데이터, System = 로직 중심의 data-oriented 구조.

대량의 유사 객체 처리에서 cache-friendly한 접근과 병렬화에 유리할 수 있다.

<b>금지 답변:</b> “ECS가 무조건 MonoBehaviour보다 빠르다.”

### 8-8. Singleton / Manager

장점: 접근 쉬움, 전역 서비스 표현 간단.

단점: 의존성 숨김, 테스트 어려움, 초기화 순서 문제, 거대한 Manager로 비대화 가능.

<b>내 프로젝트 연결:</b> ManagerTexture, ManagerAdMob 등의 전역 서비스는 책임을 좁게 유지하고 외부 API를 명확히 해야 함.

### 8-9. Event 기반 구조

발행자와 구독자의 직접 참조를 줄일 수 있다.

단점은 호출 흐름 추적 어려움, 구독 해제 누락, 이벤트 폭발.

### 8-10. Popup Stack 경험

<b>문제:</b> 팝업이 겹칠 때 뒤 UI 입력이 통과하거나 닫힘 순서가 꼬임.

<b>해결:</b> stack으로 최상단 popup을 관리하고 raycast block 정책을 통합.

<b>왜 Stack?</b> 마지막에 열린 팝업이 먼저 닫히는 LIFO 요구사항과 정확히 일치.
</toggle>

---

<toggle heading="h2">
9\. 슈퍼센트에서 특히 연결해서 말할 내 강점

- [ ] 아래 6개 경험을 1분 버전 / 3분 버전으로 각각 설명 가능

1. <b>메모리 크래시 해결</b> — 동적 Texture ownership/release.
2. <b>Pooling + Generic 통합</b> — 중복 구조 제거와 재사용.
3. <b>3D-on-UI</b> — RenderTexture + RawImage + DOTween.
4. <b>Popup Stack</b> — LIFO와 raycast block.
5. <b>하울리 도그 실제 출시</b> — Unity + Firebase Functions + 로그인/랭킹/광고/운영.
6. <b>마스터데이터/원격 Texture/구버전 호환</b> — 신규 API 분리, 안전한 배포.

<callout icon="💡" color="yellow_bg">
슈퍼센트 면접에서는 “Unity를 몇 년 썼는가”만 말하지 말고 <b>빠르게 만들고, 실제로 출시하고, 운영 문제를 고쳐본 경험</b>을 강조한다. 하울리 도그는 개인 프로젝트가 아니라 로그인·서버·랭킹·광고·운영툴·마스터데이터까지 실제 서비스 흐름을 경험했다는 증거로 사용한다.
</callout>
</toggle>

---

<toggle heading="h2">
10\. 모바일 / Android / iOS 실무

- [ ] Unity Editor에서는 되는데 실기기에서 안 되는 이유를 체계적으로 설명 가능

### 10-1. 실기기 검증 원칙

에디터와 모바일은 메모리, GPU, 파일 시스템, 권한, 네트워크, OS 생명주기가 다르다.

<b>반드시 실제 타깃 기기에서 검증한다.</b>

### 10-2. Android 기본

- Gradle이 Android 빌드 파이프라인을 담당.
- Manifest에 권한/Activity/Provider/SDK 설정 등이 들어감.
- AAB는 Play 배포에 일반적으로 사용.
- ABI, minSdk/targetSdk, IL2CPP 설정 확인.
- 네이티브 플러그인 충돌 시 dependency/version/manifest merge 확인.

### 10-3. iOS 기본

- Unity에서 Xcode project 생성 후 Xcode가 실제 iOS 빌드를 수행.
- Provisioning, Signing, Bundle Identifier, Capability 확인.
- ATS, 권한 description, Info.plist 수정이 필요한 플러그인 확인.
- App Store 심사에서는 기능뿐 아니라 로그인, 계정삭제, 광고, 개인정보 동작까지 실제 기기에서 확인.

### 10-4. IL2CPP

C# IL을 C++로 변환해 native binary로 빌드하는 backend.

AOT 환경 특성 때문에 reflection/generic 일부 사용 패턴은 주의.

### 10-5. 모바일 메모리 문제 접근

1. 특정 화면 진입/이탈 반복 재현.
2. Memory Profiler/Profiler로 증가 자원 확인.
3. managed/native/texture/audio/mesh 구분.
4. owner와 release 경로 추적.
5. 동일 시나리오 반복 후 baseline으로 돌아오는지 검증.

### 10-6. 앱 Pause / Resume

모바일은 전화, 홈 이동, 화면 잠금 등으로 중단/복귀가 자주 발생.

<code>OnApplicationPause</code>, <code>OnApplicationFocus</code> 등에 민감한 시스템을 검토한다.

게임 타이머, 네트워크, 광고 SDK, 오디오 상태 등을 복구해야 할 수 있다.
</toggle>

---

<toggle heading="h2">
11\. 네트워크 / 서버 연동 / 운영 안정성

- [ ] API 요청 실패와 중복 요청을 어떻게 설계할지 답변 가능

### 11-1. HTTP 기본

- GET: 조회
- POST: 생성/처리
- PUT: 전체 교체
- PATCH: 부분 수정
- DELETE: 삭제

### 11-2. 클라이언트 요청에서 필요한 것

- timeout
- loading 상태
- 성공/실패 UI
- retry 정책
- 취소
- 중복 요청 방지
- idempotency 여부
- 인증 만료 처리

### 11-3. 중복 요청 방지 경험

<b>상황:</b> 프레임 드랍/연타로 재화 사용 API가 중복 호출될 위험.

<b>해결 방향:</b> 요청 중 flag, 버튼 interactable 제어, request ID/idempotency key, 서버 중복 처리 방어.

<b>면접 답변</b>

> 클라이언트에서 버튼을 잠그는 것만으로는 완전한 보안이 되지 않습니다. UX 차원에서는 요청 중 재호출을 막고, 재화처럼 중요한 변경은 서버에서도 동일 요청의 중복 처리를 막는 구조가 필요합니다.

### 11-4. 서버를 신뢰하고 클라이언트를 신뢰하지 않는 이유

클라이언트 데이터는 변조 가능하다. 재화, 점수, 구매 결과, 권한, 밴 상태 등은 서버가 최종 판단해야 한다.

### 11-5. Access Token / Refresh Token

- Access Token: 짧은 수명, API 인증.
- Refresh Token: 새로운 Access Token 발급용, 더 민감하게 관리.
- 밴/로그아웃/보안 상황에서 서버가 refresh token을 폐기할 수 있어야 함.

### 11-6. 구버전 호환

<b>내 경험:</b> 실제 배포 앱을 깨지 않기 위해 신규 마스터데이터 API를 기존 endpoint와 분리.

<b>면접 답변</b>

> 이미 배포된 앱은 즉시 업데이트되지 않기 때문에 서버 변경 시 기존 contract를 깨면 안 됩니다. 신규 기능은 versioned endpoint 또는 새로운 contract로 추가하고, 구버전이 기존 API를 계속 사용할 수 있게 유지했습니다.
</toggle>

---

<toggle heading="h2">
12\. Git / 협업 / CI-CD

- [ ] merge, rebase, cherry-pick 차이를 설명 가능

### 12-1. Git 기본

- commit: 변경의 논리 단위.
- branch: 독립 작업 흐름.
- merge: 두 history 결합.
- rebase: base를 옮겨 history를 선형화.
- cherry-pick: 특정 commit만 다른 branch에 적용.
- revert: 기존 commit의 반대 변경을 새로운 commit으로 기록.

### 12-2. merge vs rebase

> merge는 실제 분기 history를 보존하고 merge commit을 만들 수 있습니다. rebase는 commit 기반을 다시 쌓아 history를 정리하지만 이미 공유된 branch를 무분별하게 rebase하면 다른 사람 history를 깨뜨릴 수 있습니다.

### 12-3. conflict 해결

1. 어느 변경이 의도된 최신 동작인지 이해.
2. 양쪽 코드를 기계적으로 섞지 않음.
3. 해결 후 컴파일/테스트.
4. 위험 변경은 코드리뷰.

### 12-4. 좋은 commit

- 하나의 목적.
- 리뷰 가능한 크기.
- 메시지에 “무엇을/왜”가 드러남.
- unrelated formatting 변경 최소화.

### 12-5. CI/CD

<b>CI:</b> push/PR마다 build, test, static check 등을 자동 수행.

<b>CD:</b> 검증된 build를 배포 단계까지 자동화.

Unity에서는:

- batchmode build
- 테스트 실행
- 플랫폼별 build
- artifact 보관
- 배포 signing secret 관리

등을 자동화할 수 있다.

### 12-6. 내 경험 연결

미니PC에서 Unity batchmode/Xvfb smoke test 환경을 구축해 UI 변경 후 자동 확인 가능한 기반을 만들었다.

<b>강점으로 말하는 방식</b>

> 수동 확인만 반복하지 않고 반복 가능한 검증 절차를 만드는 편입니다. Unity Linux batchmode와 가상 디스플레이 환경으로 최소 smoke test를 돌릴 수 있게 구성한 경험이 있습니다.
</toggle>

---

<toggle heading="h2">
13\. AI 개발도구 질문 대비

- [ ] “AI로 코딩하면 본인이 한 게 맞나요?”에 방어적이지 않게 답변 가능

### 13-1. 좋은 답변의 핵심

AI는 <b>코드 생성기</b>가 아니라 생산성 도구로 설명한다.

책임은 개발자에게 있다.

<b>암기 답변</b>

> AI 도구는 반복 코드 작성, 탐색, 리팩터링 후보 제안, 문서화 속도를 높이는 데 사용합니다. 하지만 요구사항 정의, 구조 결정, 코드리뷰, 실제 실행 검증, 성능과 보안 판단은 제가 책임집니다. 생성된 코드는 그대로 신뢰하지 않고 diff와 호출 경로를 확인하고 테스트합니다.

### 13-2. 바이브 코딩 질문

<b>나쁜 답:</b> “AI가 거의 다 해줬습니다.”

<b>좋은 답:</b>

> 구현 속도를 높이기 위해 AI를 적극 사용하지만, 작업을 작은 단위로 나누고 기존 architecture와 contract를 먼저 확인시킵니다. 변경 후에는 git diff, compile, smoke test, 실기기 테스트를 거칩니다. 특히 서버 배포나 재화 로직처럼 위험한 변경은 backward compatibility와 rollback 가능성을 먼저 봅니다.

### 13-3. AI 코드의 위험

- 존재하지 않는 API hallucination
- 요구사항 일부 누락
- 과도한 리팩터링
- 기존 contract 파괴
- 보안/동시성 문제
- 테스트 없이 “동작한다” 가정

### 13-4. AI 사용 역량을 강점으로 바꾸는 문장

> 중요한 것은 AI를 썼는지가 아니라 <b>어떤 문제를 정의했고, 결과물을 검증할 수 있는 기술적 기준을 갖고 있는지</b>라고 생각합니다.
</toggle>

---

<toggle heading="h2">
14\. 리더십 / 코드리뷰 / 협업 질문

- [ ] 파트장 경험을 “관리”가 아니라 “개발 생산성 향상” 관점으로 설명 가능

### 14-1. 코드리뷰 기준

1. 요구사항 충족 여부.
2. 버그/경계값.
3. 책임 분리와 의존성.
4. 유지보수성/이름.
5. 성능은 필요한 구간만.
6. 취향은 팀 컨벤션으로 분리.

### 14-2. 주니어 피드백

> 정답 코드를 대신 작성하기보다 문제 원인, 선택지, trade-off를 설명하고 스스로 수정하게 하는 편입니다. 반복되는 문제는 Notion 문서와 공통 규칙으로 남겨 팀 전체 비용을 줄였습니다.

### 14-3. 의견 충돌

> 개인 선호로 결정하지 않고 요구사항, 유지보수 비용, 성능 측정치, 일정 리스크 같은 기준으로 비교합니다. 결론이 나면 팀 결정에 맞추고 기록을 남깁니다.

### 14-4. 내 경험

- 최대 4명 클라이언트 팀 리딩.
- 개발자 총 7명 교육 경험.
- 코드리뷰, QA, Notion 교육자료.
- naming convention과 공통 구조 정리.

### 14-5. “리더인데 직접 개발도 했나요?”

<b>암기 답변</b>

> 파트장 역할과 동시에 실제 클라이언트 구현을 계속 담당했습니다. 공통 시스템과 난도가 높은 이슈를 직접 해결하고, 팀원 구현은 리뷰와 QA로 품질을 맞추는 형태였습니다.
</toggle>

---

<toggle heading="h2">
15\. 내 프로젝트 경험 — 면접용 완성 답변

- [ ] 각 답변을 외우지 않고 키워드만 보고 자연스럽게 1분 설명 가능

### 15-1. 메모리 크래시

<b>30초 버전</b>

> 동적으로 이미지를 로드하는 화면에서 사용 시간이 길어질수록 메모리가 증가하고 크래시가 발생했습니다. 이미지별 고유 키로 리소스의 생명주기를 추적하도록 바꾸고 객체가 제거될 때 release되는 경로를 정리했습니다. 반복 시나리오로 검증한 뒤 크래시가 사라졌습니다. 이후에는 로드 기능을 설계할 때 owner와 release 시점을 먼저 정의합니다.

<b>꼬리질문 대비:</b>

- Profiler에서 무엇을 봤나?
- 캐시를 없애면 되지 않나?
- 중복 texture는 어떻게 처리했나?
- release 후 참조하는 객체가 있으면?

### 15-2. Pooling / Generic

> 타입별로 유사한 생성/반환 코드가 흩어져 있어 수정 포인트가 많았습니다. 공통 생명주기를 generic pool로 통합해 타입 안정성을 유지하면서 중복을 제거했습니다. 다만 모든 객체를 풀링하지 않고 생성 빈도와 비용이 실제로 의미 있는 객체에 적용했습니다.

### 15-3. 3D-on-UI

> 2D UI 레이어 안에서 3D 모델을 안정적으로 보여줘야 해서 별도 카메라의 출력을 RenderTexture로 받고 RawImage에 표시했습니다. 연출은 DOTween으로 분리했습니다. 이 방식은 UI hierarchy와 3D rendering을 분리할 수 있지만 RenderTexture 메모리와 추가 카메라 렌더 비용이 있어 해상도와 갱신 빈도를 관리해야 합니다.

### 15-4. Popup Stack

> 여러 팝업이 중첩되면서 뒤 UI에 클릭이 전달되고 닫힘 순서가 꼬이는 문제가 있었습니다. 마지막에 열린 팝업이 먼저 닫히는 요구사항에 맞춰 stack으로 관리하고 최상위 팝업 외 raycast를 차단했습니다. 이후 popup별 임의 처리보다 공통 정책으로 동작하게 만들었습니다.

### 15-5. 하울리 도그 출시

> 개인 게임이지만 실제 서비스 기준으로 구현했습니다. Unity 클라이언트와 Firebase Functions 서버를 연결하고 Apple/Google 로그인, 랭킹, 계정삭제, 밴, 광고, 재화, 마스터데이터, 관리자 페이지까지 구성했습니다. iOS 심사를 직접 통과했고 실제 배포 이후에는 구버전 API를 유지하면서 신규 기능을 별도 endpoint로 확장했습니다.

### 15-6. 마스터데이터

> 캐릭터 수가 증가하면서 빌드에 박힌 데이터를 계속 업데이트하기 어려워 서버 마스터데이터로 옮겼습니다. 다만 이미 출시된 앱이 있어 기존 API는 유지하고 신규 버전에서 새로운 endpoint를 사용하게 해 backward compatibility를 지켰습니다. 이미지도 무조건 전체 prefetch하지 않고 사용 시점과 캐시 정책을 분리했습니다.

### 15-7. 서버 에러코드/로컬라이즈

> 서버에서 한국어 문장을 직접 반환하면 언어별 분기와 클라이언트 제어가 어려워 <code>NO_COIN</code> 같은 UPPER\_SNAKE\_CASE code로 통일하고 클라이언트에서 KR/EN/JP message로 변환하도록 정리했습니다. 서버 contract와 UI 문구를 분리한 경험입니다.
</toggle>

---

<toggle heading="h2">
16\. 슈퍼센트형 실무 질문 — 반드시 외울 40문답

- [ ] 질문만 보고 20\~40초 내 답변 가능

### C#

<b>Q1. 값 타입과 참조 타입 차이는?</b>

A. 핵심은 저장 위치 단정이 아니라 복사 semantics다. 값 타입은 값이 복사되고 참조 타입은 객체에 대한 참조가 복사된다.

<b>Q2. struct를 언제 쓰나?</b>

A. 작고 독립적인 값, 값 복사가 자연스러운 데이터. 큰 mutable struct는 복사와 예측 가능성 때문에 주의.

<b>Q3. Boxing이 왜 문제인가?</b>

A. 값 타입을 object/interface로 다루는 과정에서 allocation이 생길 수 있어 hot path에서 GC 압력을 만든다.

<b>Q4. interface와 abstract class 차이?</b>

A. 역할 계약만 필요하면 interface, 상태/공통 구현까지 공유하면 abstract class.

<b>Q5. event를 쓰는 이유?</b>

A. 발행자와 구독자의 직접 결합을 줄이면서 외부 임의 invoke를 제한하기 위해.

<b>Q6. LINQ를 Unity에서 피해야 하나?</b>

A. 무조건 아니다. hot path allocation/성능을 측정하고 가독성과 비용을 비교한다.

<b>Q7. async/await와 Coroutine 차이는?</b>

A. Coroutine은 PlayerLoop 기반 frame 분할, async/await는 Task 기반 비동기 흐름 표현. 둘 다 Thread와 동일하지 않다.

<b>Q8. Dictionary 평균 O(1)의 의미?</b>

A. Hash 기반 평균 복잡도이며 충돌 상황에서는 악화될 수 있다.

### Unity

<b>Q9. Awake와 Start 차이?</b>

A. Awake는 객체 자체 초기화/참조 캐싱, Start는 첫 Update 전 다른 객체와의 연결이 필요한 초기화에 주로 사용.

<b>Q10. Update와 FixedUpdate 차이?</b>

A. Update는 렌더 frame 기준, FixedUpdate는 고정 timestep 물리 처리.

<b>Q11. Coroutine은 Thread인가?</b>

A. 아니다. PlayerLoop에서 실행을 양보하고 재개하는 구조.

<b>Q12. ScriptableObject 장점?</b>

A. GameObject와 분리된 공유 데이터 asset으로 설정/마스터 데이터를 관리하기 좋다.

<b>Q13. Instantiate/Destroy가 왜 비싼가?</b>

A. managed/native object 생성, 초기화, hierarchy/engine 등록과 GC 등 비용이 반복될 수 있다.

<b>Q14. Object Pooling 단점?</b>

A. 메모리 상주, 상태 reset 버그, pool size 정책, 복잡도 증가.

<b>Q15. GetComponent를 매 frame 하면 안 되나?</b>

A. 반복 검색이 필요 없으면 캐싱한다. 금지가 아니라 빈도와 측정이 핵심.

### Memory / Performance

<b>Q16. GC spike가 뭔가?</b>

A. managed allocation 누적 후 GC가 객체를 탐색/회수하면서 frame time이 순간 증가하는 현상.

<b>Q17. GC.Alloc 줄이는 방법?</b>

A. hot path의 new/string/LINQ/closure/boxing/컬렉션 재생성을 Profiler로 찾아 재사용/캐싱.

<b>Q18. Texture 메모리 문제는 어떻게 찾나?</b>

A. 반복 재현 → Memory Profiler → native Texture 증가 확인 → owner/release path 추적 → 수정 후 baseline 복귀 검증.

<b>Q19. Draw Call이 뭔가?</b>

A. CPU가 GPU에 렌더 명령을 제출하는 단위 비용. batching/instancing/material 상태가 영향.

<b>Q20. Overdraw가 뭔가?</b>

A. 동일 픽셀이 여러 번 그려지는 것. 투명 UI/particle에서 GPU fill-rate 비용 증가.

<b>Q21. UI Canvas가 느릴 때?</b>

A. rebuild 범위, LayoutGroup, ContentSizeFitter, 자주 바뀌는 요소와 정적 요소 분리를 확인.

<b>Q22. 최적화 첫 단계?</b>

A. Profiler와 실제 기기로 병목 측정. 추측으로 시작하지 않는다.

### Architecture

<b>Q23. SOLID를 왜 쓰나?</b>

A. 패턴 자체가 목적이 아니라 변경 영향과 결합도를 낮추기 위해.

<b>Q24. Singleton 단점?</b>

A. 숨은 의존성, 테스트 어려움, 초기화 순서, 거대한 Manager 비대화.

<b>Q25. Event 기반 구조 단점?</b>

A. 흐름 추적 어려움, 구독 해제 누락, 순서 의존성.

<b>Q26. MVC/MVVM 차이?</b>

A. 둘 다 책임 분리이며 MVVM은 ViewModel과 binding 중심으로 UI state 표현에 유리.

<b>Q27. ECS가 왜 빠를 수 있나?</b>

A. data-oriented layout과 대량 순차 처리/병렬화에 유리한 구조를 만들 수 있기 때문. 항상 빠른 것은 아님.

### Network / Operation

<b>Q28. 중복 API 요청은 어떻게 막나?</b>

A. 클라 request-in-flight 잠금 + 서버 idempotency/중복 방어를 함께 고려.

<b>Q29. 재화 계산을 클라이언트에서 하면 안 되나?</b>

A. 변조 가능하므로 서버 authoritative가 되어야 한다.

<b>Q30. 앱 업데이트 없이 서버를 바꿀 때 주의점?</b>

A. 기존 contract 유지, versioned/new endpoint, backward compatibility.

<b>Q31. 캐시는 언제 invalidation하나?</b>

A. version/ETag/TTL/master revision 등 명확한 freshness 기준을 설계해야 한다.

<b>Q32. 실패한 네트워크 요청을 무조건 retry하면 되나?</b>

A. 아니다. idempotent 여부, 서버 오류 종류, backoff, 사용자 행동을 고려.

### Git / Team

<b>Q33. merge와 rebase 차이?</b>

A. merge는 분기 history 보존, rebase는 base 재작성으로 history 선형화. 공유 history rebase는 주의.

<b>Q34. 코드리뷰에서 가장 먼저 보는 것?</b>

A. 요구사항과 correctness. 그 다음 설계/유지보수/성능.

<b>Q35. 팀원 코드가 마음에 안 들면?</b>

A. 취향이 아니라 팀 기준, 버그 가능성, 유지보수 비용으로 설명.

### AI

<b>Q36. AI가 만든 코드를 신뢰하나?</b>

A. 신뢰하지 않고 diff, compile, test, 실제 동작으로 검증한다.

<b>Q37. AI를 쓰면 기본기가 떨어지지 않나?</b>

A. 판단 기준이 없으면 그렇다. 나는 구조/contract/성능/테스트 기준을 직접 가지고 검증 도구로 사용한다.

### 경험

<b>Q38. 가장 어려운 버그?</b>

A. Texture 메모리 누적 크래시 → ownership/release 추적 → 수정 후 크래시 제거.

<b>Q39. 가장 자신 있는 구조 개선?</b>

A. pooling/generic 통합 또는 popup stack. 문제와 trade-off까지 설명.

<b>Q40. 출시 경험에서 가장 많이 배운 점?</b>

A. 기능 구현보다 backward compatibility, 데이터 migration, 운영툴, 예외/실패 처리까지 포함해야 서비스가 된다.
</toggle>

---

<toggle heading="h2">
17\. 시스템 설계형 질문

- [ ] 답을 바로 코딩하지 않고 요구사항/데이터/생명주기부터 말할 수 있음

### 17-1. “캐릭터 300개 도감 시스템을 설계해보세요”

<b>답변 순서</b>

1. master data와 user owned data 분리.
2. characterId를 stable key로 사용.
3. 표시 이름/설명은 localization key 또는 언어 데이터 분리.
4. image는 address/URL만 master에 두고 필요 시 load/cache.
5. 보유/레벨/스킨 선택은 user data.
6. master version/revision으로 cache invalidation.
7. 삭제/비공개 시 이미 보유한 user data 처리 정책 정의.
8. 구버전 앱 compatibility 고려.

### 17-2. “Popup Manager 설계”

- Stack으로 열린 순서 관리.
- 중복 오픈 정책.
- top popup만 input.
- close callback/result.
- scene 변경 시 cleanup.
- async asset load 취소.

### 17-3. “Remote Image Loader 설계”

- URL/key → memory/disk cache.
- concurrent duplicate request 병합.
- timeout/cancel.
- placeholder/fallback.
- cache version/TTL.
- owner/release 또는 eviction.
- texture size 제한.

### 17-4. “Rewarded Ad 설계”

- load/show state machine.
- 중복 show 방지.
- reward callback은 한 번만 처리.
- 서버 보상이라면 transaction/idempotency.
- no-fill/fail/cancel UX.
- app pause/resume 고려.
</toggle>

---

<toggle heading="h2">
18\. 컬처핏 / 인성 면접 답변

- [ ] 답변을 부정적으로 끝내지 않고 행동과 학습으로 마무리

### 18-1. 왜 슈퍼센트인가?

<b>핵심 구성:</b> 빠른 제작/출시 → 데이터 기반 개선 → 내 경험과 연결.

<b>암기 골격</b>

> 저는 실제로 빠르게 기능을 만들고 출시한 뒤 운영 중 문제를 개선하는 과정에서 가장 많이 성장했습니다. 슈퍼센트는 제작 주기가 빠르고 실제 지표를 보며 반복 개선하는 환경이라는 점이 제 경험과 잘 맞는다고 판단했습니다. 특히 Unity 클라이언트에서 성능, 운영 안정성, 반복 가능한 개발 구조를 더 높은 수준에서 경험하고 싶습니다.

### 18-2. 게임회사 경력이 짧거나 없다는 질문

> 회사 프로젝트의 도메인은 게임과 달랐지만 Unity 클라이언트의 메모리, UI, 풀링, 공통 구조, 팀 리딩 경험을 쌓았습니다. 별도로 하울리 도그를 실제 iOS에 출시하면서 로그인, 랭킹, 광고, 서버, 운영툴까지 직접 경험했습니다. 그래서 게임 제작 경험의 부족을 개인 출시와 운영 경험으로 보완해왔습니다.

### 18-3. 약점

<b>나쁜 답:</b> “코딩을 못합니다.”

<b>좋은 골격:</b> 실제 약점 + 개선 행동 + 현재 상태.

> 알고리즘 문제를 실무보다 늦게 체계적으로 준비한 편입니다. 그래서 지금은 매일 자료구조와 패턴을 정리하고 직접 구현하면서 보완하고 있습니다. 실무 문제 해결은 자신 있지만 코딩테스트에서도 같은 수준의 기본기를 보여주는 것이 현재 준비 목표입니다.

### 18-4. 빠른 일정이 괜찮나?

> 빠른 일정 자체보다 우선순위가 불명확한 상태가 더 위험하다고 생각합니다. 일정이 짧을수록 필수 범위와 나중에 개선할 범위를 구분하고, 위험한 부분은 최소 검증 절차를 자동화하려고 합니다.

### 18-5. 실패 경험

답변은 실패를 숨기지 말고 <b>왜 실패했는지 판단 기준이 생겼다</b>로 끝낸다.
</toggle>

---

<toggle heading="h2">
19\. 30초 자기소개 / 90초 자기소개

- [ ] 외운 티가 안 나게 말할 수 있음

### 30초

> Unity 클라이언트 개발 경력을 쌓으면서 최대 4명 팀의 파트장을 맡아 클라이언트 전체 개발과 코드리뷰, 교육을 진행했습니다. 실무에서는 동적 이미지 메모리 크래시 해결, pooling 구조 통합, RenderTexture 기반 3D UI 같은 문제를 해결했습니다. 최근에는 개인 게임 하울리 도그를 iOS에 직접 출시해 서버, 로그인, 랭킹, 광고, 운영까지 경험했습니다. 빠르게 구현하는 것뿐 아니라 실제 서비스에서 안정적으로 동작하도록 만드는 개발자가 강점입니다.

### 90초 구성

1. 현재 역할/연차.
2. 대표 기술 문제 2개.
3. 리더십.
4. 하울리 도그 출시.
5. 슈퍼센트 지원 연결.
</toggle>

---

<toggle heading="h2" color="yellow_bg">
20\. 마지막 1장 — 매일 소리 내어 읽는 암기문

### C#

값 타입은 값이 복사되고 참조 타입은 참조가 복사된다. 큰 struct 복사와 class allocation 비용을 구분한다. Boxing은 allocation을 만들 수 있다. string은 immutable이다. delegate는 메서드 참조, event는 외부 invoke를 제한한다. LINQ는 지연 실행과 allocation 가능성을 본다. async/await는 Thread가 아니며 Unity API의 Main Thread 제약을 기억한다.

### 자료구조

Array index O(1), List 중간 삽입삭제 O(N), Dictionary/HashSet 평균 O(1), Stack은 LIFO, Queue는 FIFO. 자료구조는 편해서가 아니라 요구되는 접근 패턴과 시간복잡도로 고른다.

### Unity

Awake는 내부 초기화, Start는 첫 Update 전 연결, Update는 프레임, FixedUpdate는 물리, LateUpdate는 후처리. Coroutine은 Thread가 아니다. MonoBehaviour에 모든 책임을 몰지 않는다.

### 최적화

최적화는 Profiler로 시작한다. CPU/GPU/Memory를 구분한다. hot path의 new, string, LINQ, closure, boxing, Instantiate/Destroy를 본다. Pooling은 재사용이 실제로 이득일 때 쓴다. Draw Call, Overdraw, Canvas Rebuild를 구분한다.

### 메모리

Managed와 Native를 구분한다. Texture는 owner와 release 정책을 반드시 가진다. Load를 만들기 전에 Release를 설계한다. 캐시는 key, invalidation, limit, eviction, fallback까지가 하나의 기능이다.

### 설계

SOLID와 MVC/MVVM/ECS는 이름을 쓰는 게 목적이 아니다. 변경 이유와 책임을 나누는 것이 목적이다. Singleton은 편하지만 숨은 의존성과 초기화 순서를 만든다. Event는 결합을 줄이지만 호출 추적과 구독 해제를 관리해야 한다.

### 네트워크

클라이언트를 신뢰하지 않는다. 재화와 권한은 서버가 최종 판단한다. timeout, retry, cancel, 중복 요청, idempotency를 고려한다. 출시된 구버전의 contract를 깨지 않는다.

### 협업

코드리뷰는 취향보다 correctness와 요구사항을 먼저 본다. 의견 충돌은 측정치, 유지보수 비용, 일정 리스크로 판단한다. 반복되는 실수는 문서와 공통 구조로 해결한다.

### AI

AI는 구현 속도를 높이는 도구이고 책임은 개발자에게 있다. 요구사항, architecture, diff, compile, test, 성능, 보안 검증은 내가 한다.

### 내 경험

메모리 크래시 → ownership/release. Pooling → generic 공통화. 3D UI → RenderTexture/RawImage. Popup → Stack/raycast. 하울리 도그 → 실제 iOS 출시/서버/광고/운영. 마스터데이터 → 신규 API와 구버전 호환. 이것을 모두 “문제 → 원인 → 판단 → 구현 → 결과 → 회고”로 말한다.
</toggle>

---

## 21. 최종 숙지 체크리스트

- [ ] 값/참조 타입을 30초 안에 설명한다.
- [ ] Boxing, delegate/event, LINQ, async/await를 설명한다.
- [ ] Array/List/Dictionary/HashSet/Stack/Queue 복잡도를 말한다.
- [ ] DFS/BFS/Binary Search/Two Pointer 문제를 직접 구현한다.
- [ ] Unity 생명주기를 순서와 용도로 설명한다.
- [ ] GC.Alloc 대표 원인 5개 이상을 말한다.
- [ ] Managed/Native/Texture memory 차이를 말한다.
- [ ] Object Pool 장단점을 말한다.
- [ ] Draw Call/Overdraw/Canvas Rebuild를 구분한다.
- [ ] Addressables/remote asset의 release/cache 정책을 설명한다.
- [ ] SOLID/MVC/MVVM/ECS를 짧게 설명한다.
- [ ] API 중복 요청과 서버 idempotency 차이를 말한다.
- [ ] 구버전 compatibility 전략을 말한다.
- [ ] merge/rebase/cherry-pick/revert 차이를 말한다.
- [ ] AI 코딩 도구 사용 방식을 책임 있게 설명한다.
- [ ] 메모리 크래시 사례를 1분에 설명한다.
- [ ] Pooling 리팩터 사례를 1분에 설명한다.
- [ ] 3D UI 사례를 1분에 설명한다.
- [ ] Popup Stack 사례를 1분에 설명한다.
- [ ] 하울리 도그 출시 경험을 1분에 설명한다.
- [ ] 자기소개 30초를 자연스럽게 말한다.
- [ ] 지원동기를 슈퍼센트 업무방식과 내 경험으로 연결한다.

<callout icon="✅" color="green_bg">
<b>합격 준비 기준:</b> 체크만 하지 말고 노션을 닫은 상태에서 질문을 랜덤으로 골라 <b>20\~60초 동안 말로 설명</b>한다. 기술 정의만 맞는 수준이 아니라 “왜, 비용, 실제 경험”까지 연결되면 숙지 완료로 본다.
</callout>

---

<toggle heading="h2">
22\. Unity 심화 꼬리질문 30개

- [ ] 답변을 외우기보다 핵심 원리를 설명 가능

<b>Q1. </b><b><code>Destroy</code></b><b>는 즉시 파괴되나?</b>

A. 일반적인 <code>Destroy</code>는 호출 즉시 C# 코드 흐름에서 native object를 완전히 제거하는 방식으로 이해하면 위험하다. 실제 파괴는 Unity의 생명주기 처리 시점에 반영된다. Editor 전용 즉시 파괴가 필요한 특수 상황에서는 <code>DestroyImmediate</code>가 있지만 런타임 일반 코드에서 남용하지 않는다.

<b>Q2. UnityEngine.Object의 null 체크가 일반 C# 객체와 왜 다른가?</b>

A. UnityEngine.Object는 native object와 managed wrapper가 연결되어 있고 Unity가 <code>==</code> 연산을 오버로드한다. native object가 파괴된 뒤 wrapper가 남은 상황에서 일반 C# null semantics와 다르게 보일 수 있다.

<b>Q3. </b><b><code>OnEnable</code></b><b>/</b><b><code>OnDisable</code></b><b>에서 event를 구독/해제하는 이유?</b>

A. 활성 상태와 구독 상태를 맞추기 쉽고 재활성화 때 중복 구독을 방지하기 좋기 때문이다. 객체의 실제 생명주기에 따라 Awake/OnDestroy 조합이 더 맞는 경우도 있어 일관성을 지킨다.

<b>Q4. Scene 전환 시 Manager를 어떻게 유지하나?</b>

A. <code>DontDestroyOnLoad</code>를 사용할 수 있지만 중복 생성, 초기화 순서, scene dependency 문제를 관리해야 한다. 모든 것을 전역 Manager로 만들기보다 정말 전역 수명인 서비스만 유지한다.

<b>Q5. </b><b><code>Time.timeScale = 0</code></b><b>이면 전부 멈추나?</b>

A. 아니다. timeScale의 영향을 받지 않는 API/처리가 있다. UI animation이나 timer가 pause 중에도 돌아야 한다면 unscaled time 계열을 고려한다.

<b>Q6. Physics 처리를 Update에 넣으면 왜 문제인가?</b>

A. 렌더 frame rate와 physics fixed timestep이 다르기 때문에 힘/이동 계산이 불안정해질 수 있다. 입력 수집은 Update, Rigidbody 반영은 FixedUpdate로 분리하는 패턴이 일반적이다.

<b>Q7. </b><b><code>transform.position</code></b><b> 직접 변경과 Rigidbody 이동 차이?</b>

A. physics object를 transform으로 직접 순간이동시키면 physics simulation과 충돌 처리 의도와 어긋날 수 있다. 목적에 따라 Rigidbody API를 사용한다.

<b>Q8. </b><b><code>Resources.UnloadUnusedAssets</code></b><b>를 자주 호출하면 좋은가?</b>

A. 아니다. 무거운 정리 작업이 될 수 있어 임의로 반복 호출하는 것이 해결책이 아니다. 소유권과 release 구조를 먼저 바로잡는다.

<b>Q9. Addressables reference count가 0이면 항상 즉시 메모리가 완전히 줄어드나?</b>

A. 개별 asset/dependency/native/GPU 자원 정리 시점이 있어 단순 수치 하나로 즉시 반환을 단정하면 안 된다. handle release 규칙과 Profiler를 함께 본다.

<b>Q10. Memory leak과 높은 정상 메모리를 어떻게 구분하나?</b>

A. 동일 시나리오 진입/이탈을 반복했을 때 baseline으로 회복되는지, reference chain과 retained object가 계속 증가하는지 본다.

<b>Q11. CPU bound와 GPU bound를 어떻게 구분하나?</b>

A. Profiler/Frame Debugger/GPU profiler 등으로 CPU frame time과 GPU frame time을 비교하고 병목 stage를 좁힌다. 해상도를 낮췄을 때 성능이 크게 좋아지면 GPU fill-rate 계열 가능성도 본다.

<b>Q12. Batching이 무조건 좋은가?</b>

A. 아니다. 조건과 메모리/CPU trade-off가 있고 SRP Batcher, GPU Instancing, static batching이 서로 다른 목적과 제약을 가진다.

<b>Q13. Material을 런타임에 복제하면 어떤 문제가 있나?</b>

A. 의도치 않은 material instance 증가와 batching 깨짐, 메모리 증가가 생길 수 있다. sharedMaterial과 material의 차이를 이해하고 목적에 맞게 사용한다.

<b>Q14. UI에서 LayoutGroup이 왜 비쌀 수 있나?</b>

A. hierarchy 변경이나 size 변경이 layout rebuild를 연쇄적으로 유발할 수 있다. 자주 갱신되는 영역은 구조 단순화나 수동 layout을 고려한다.

<b>Q15. </b><b><code>Canvas</code></b><b>를 나누면 항상 빨라지나?</b>

A. 아니다. rebuild 범위는 줄일 수 있지만 Canvas 수 증가 자체의 렌더/관리 비용도 있다. 변경 빈도 기준으로 분리한다.

<b>Q16. Texture 압축 포맷을 왜 플랫폼별로 봐야 하나?</b>

A. GPU 지원 포맷과 품질/메모리/압축률이 플랫폼마다 다르기 때문이다. Android/iOS 타깃에서 실제 메모리와 품질을 검증한다.

<b>Q17. Mipmap은 왜 쓰나?</b>

A. 멀리 보이는 texture에서 작은 mip level을 사용해 sampling 품질과 cache 효율을 높일 수 있다. UI처럼 항상 원본 크기로 보는 texture에는 불필요할 수 있다.

<b>Q18. Sprite Atlas의 장점?</b>

A. 여러 sprite를 하나의 texture에 묶어 texture switch와 draw call 감소에 도움을 줄 수 있다. atlas 크기/메모리 상주 범위도 고려한다.

<b>Q19. </b><b><code>async void</code></b><b>를 왜 조심하나?</b>

A. 호출자가 완료/예외를 추적하기 어렵다. event handler처럼 필요한 경우 외에는 <code>Task</code> 반환을 우선한다.

<b>Q20. CancellationToken은 왜 필요한가?</b>

A. 화면을 닫았는데 이전 요청/로딩이 완료되어 이미 파괴된 UI를 수정하는 race를 막고 불필요한 작업을 중단하기 위해.

<b>Q21. Race Condition이란?</b>

A. 실행 순서에 따라 결과가 달라지는 문제. 비동기 요청의 최신 요청 ID, cancellation, state lock 등으로 제어한다.

<b>Q22. State Machine은 어디에 유용한가?</b>

A. 광고, 캐릭터 상태, UI flow처럼 가능한 상태와 전이가 명확한 시스템. boolean 여러 개보다 불가능한 상태 조합을 줄일 수 있다.

<b>Q23. Dependency Injection의 장점?</b>

A. 의존성을 외부에서 명시적으로 주입해 숨은 전역 의존성을 줄이고 테스트/교체를 쉽게 한다. 프레임워크 사용 자체가 목적은 아니다.

<b>Q24. Composition over Inheritance란?</b>

A. 상속 계층이 깊어지는 대신 작은 역할/컴포넌트를 조합해 동작을 구성하는 접근. 변경 영향과 재사용성을 개선할 수 있다.

<b>Q25. SerializeField로 Dictionary가 기본 Inspector에 바로 안 보이는 경우 어떻게 하나?</b>

A. Unity serialization 지원 범위를 이해하고 serializable pair/list, custom editor, wrapper 구조 등을 사용한다. 런타임 자료구조와 직렬화 표현을 분리할 수 있다.

<b>Q26. PlayerPrefs에 민감한 값을 저장해도 되나?</b>

A. 보안 저장소가 아니다. 설정값에는 편리하지만 재화/인증 비밀정보를 신뢰 가능한 저장소로 취급하면 안 된다.

<b>Q27. 모바일에서 앱 종료 이벤트를 믿고 저장하면 되나?</b>

A. OS가 항상 정상 종료 callback을 보장한다고 가정하면 안 된다. 중요한 데이터는 이벤트가 발생할 때 즉시/주기적으로 안전하게 저장하는 방향을 고려한다.

<b>Q28. 로그를 많이 남기면 어떤 문제가 있나?</b>

A. 문자열 allocation, I/O, 민감정보 노출, 성능 문제가 있을 수 있다. build configuration과 log level을 관리한다.

<b>Q29. </b><b><code>FindObjectOfType</code></b><b> 같은 검색 API를 자주 쓰면?</b>

A. scene 탐색 비용과 숨은 dependency를 만들 수 있다. 초기화 시 명시적 참조/DI/registry를 우선 고려한다.

<b>Q30. Profiler에서 한 번 느린 frame을 봤으면 원인 확정인가?</b>

A. 아니다. 재현 조건과 여러 sample, device 차이를 확인하고 수정 전후를 동일 조건에서 비교한다.
</toggle>

---

<toggle heading="h2">
23\. C# 심화 꼬리질문 20개

- [ ] 문법 암기보다 runtime 동작과 비용을 설명 가능

<b>Q1. </b><b><code>Equals</code></b><b>와 </b><b><code>GetHashCode</code></b><b> 관계?</b>

A. 두 객체가 Equals상 같다면 같은 hash code를 반환해야 hash collection이 올바르게 동작한다.

<b>Q2. </b><b><code>IEquatable<T></code></b><b>를 왜 구현하나?</b>

A. 특정 타입 equality를 명확하고 효율적으로 정의할 수 있고 generic collection에서 boxing을 줄이는 데 도움이 될 수 있다.

<b>Q3. Closure란?</b>

A. lambda/local function이 외부 변수를 캡처하는 구조. 캡처 객체 allocation과 예상치 못한 변수 생명주기 연장이 생길 수 있다.

<b>Q4. </b><b><code>readonly struct</code></b><b> 장점?</b>

A. 값 객체를 immutable하게 표현하고 의도치 않은 수정/방어적 복사를 줄이는 데 도움이 될 수 있다.

<b>Q5. </b><b><code>Span<T></code></b><b>가 뭔가?</b>

A. 연속 메모리 영역을 allocation 없이 slice처럼 다룰 수 있는 ref struct 계열. Unity/.NET profile과 사용 가능 범위를 확인해야 한다.

<b>Q6. </b><b><code>List<T>.Capacity</code></b><b>와 Count 차이?</b>

A. Count는 실제 요소 수, Capacity는 내부 배열이 재할당 없이 담을 수 있는 수. 예상 크기를 알면 초기 capacity를 지정해 resize allocation을 줄일 수 있다.

<b>Q7. List가 커질 때 무슨 일이 생기나?</b>

A. 내부 배열 capacity가 부족하면 더 큰 배열을 만들고 기존 요소를 복사한다. 빈번한 성장에서는 allocation/copy가 발생한다.

<b>Q8. </b><b><code>TryGetValue</code></b><b> 장점?</b>

A. ContainsKey 후 index 접근처럼 hash lookup을 중복하지 않고 존재 여부와 값을 한 번에 확인할 수 있다.

<b>Q9. </b><b><code>First()</code></b><b>와 </b><b><code>FirstOrDefault()</code></b><b> 차이?</b>

A. First는 없으면 예외, FirstOrDefault는 기본값. 기본값이 유효한 값일 수 있으므로 의미 구분에 주의한다.

<b>Q10. </b><b><code>Any()</code></b><b>와 Count 비교?</b>

A. 존재 여부만 필요하면 Any가 enumeration 전체를 끝까지 셀 필요 없이 판단할 수 있는 경우가 있다. 실제 collection type에 따라 비용을 이해한다.

<b>Q11. Covariance/Contravariance?</b>

A. generic interface/delegate에서 타입 호환 방향을 정의하는 개념. <code>out</code>은 생산, <code>in</code>은 소비 역할과 연결해 기억한다.

<b>Q12. </b><b><code>lock</code></b><b>은 언제 쓰나?</b>

A. 여러 thread가 공유 상태를 동시에 변경할 때 임계구역 보호. Main Thread 중심 Unity 코드에서 무조건 필요한 것은 아니며 deadlock/경합 비용을 고려한다.

<b>Q13. Thread-safe collection이 있으면 race가 전부 해결되나?</b>

A. 개별 collection operation의 안전성과 전체 business operation의 원자성은 다르다.

<b>Q14. Immutable data의 장점?</b>

A. 상태 변화 지점을 줄여 추론과 동시성 안전성을 개선한다. 대신 새 객체 생성 비용과 메모리 trade-off가 있다.

<b>Q15. </b><b><code>record</code></b><b>를 언제 쓰나?</b>

A. 값 중심 데이터 모델과 equality가 필요한 상황에서 유용한 C# 기능. 사용 Unity/C# language version 지원 여부를 확인한다.

<b>Q16. extension method 장단점?</b>

A. 기존 타입을 수정하지 않고 읽기 좋은 helper를 추가할 수 있지만 남용하면 API 위치와 의존성이 숨겨진다.

<b>Q17. Generic constraint를 왜 쓰나?</b>

A. 허용 타입 범위를 제한하고 컴파일 타임에 필요한 API 계약을 보장한다.

<b>Q18. </b><b><code>using</code></b><b>/IDisposable 의미?</b>

A. GC에만 맡기기 어려운 file/socket/native handle 같은 자원을 deterministic하게 정리하기 위한 패턴.

<b>Q19. finalizer를 왜 남용하면 안 되나?</b>

A. GC 처리 단계를 복잡하게 하고 회수 시점을 지연시킬 수 있다. IDisposable 패턴과 native resource ownership을 우선 설계한다.

<b>Q20. nullable reference type의 목적?</b>

A. compile-time 분석으로 null 가능성을 타입 정보에 표현해 NRE 위험을 줄이는 것. Unity 프로젝트 설정/버전과의 호환을 본다.
</toggle>

---

<toggle heading="h2">
24\. 코딩테스트 필수 연습 15문제

<b>규칙:</b> 답을 읽지 말고 먼저 종이에 복잡도와 자료구조를 적고 C#으로 직접 구현한다.

- [ ] 
  1. 배열에서 두 수의 합이 target인 index 찾기 — Dictionary — O(N)
- [ ] 
  1. 문자열의 중복 문자 여부 — HashSet — O(N)
- [ ] 
  1. 가장 많이 등장한 값 — Dictionary count — O(N)
- [ ] 
  1. 정렬 배열에서 target 찾기 — Binary Search — O(log N)
- [ ] 
  1. 정렬 배열 두 수 합 — Two Pointer — O(N)
- [ ] 
  1. 괄호 유효성 검사 — Stack — O(N)
- [ ] 
  1. Queue 직접 구현 또는 사용법 — FIFO
- [ ] 
  1. 연결 그래프 방문 — DFS — O(V+E)
- [ ] 
  1. 가중치 없는 최단거리 — BFS — O(V+E)
- [ ] 
  1. 2D grid 섬 개수 — DFS/BFS — O(R×C)
- [ ] 
  1. 연속 구간 최대 합 — DP/Kadane — O(N)
- [ ] 
  1. 중복 없는 가장 긴 substring — Sliding Window — O(N)
- [ ] 
  1. 구간 합 질의 — Prefix Sum — 전처리 O(N), 질의 O(1)
- [ ] 
  1. N개의 작업 중 우선순위 높은 것 처리 — PriorityQueue
- [ ] 
  1. 캐릭터 목록을 grade→level→id 기준 정렬 — comparer/LINQ와 복잡도 설명

### 반드시 말하면서 코딩할 것

1. “입력 최대 크기를 먼저 보겠습니다.”
2. “중복 확인이므로 HashSet이 적합합니다.”
3. “이 풀이 시간복잡도는 O(N), 공간복잡도는 O(N)입니다.”
4. “빈 입력/중복/경계값을 확인하겠습니다.”

### Unity형 코딩 문제 5개

- [ ] <code>ObjectPool<T></code> 최소 구현: Get/Return/Clear.
- [ ] Popup Stack: Push/Pop/Top/중복 방지.
- [ ] 캐릭터 ID→데이터 Dictionary 캐시.
- [ ] 중복 네트워크 요청 방지 wrapper.
- [ ] LRU에 가까운 remote texture cache의 핵심 인터페이스 설계.
</toggle>

---

<toggle heading="h2">
25\. 실전 화이트보드 설계 시험

### 문제 A — 1,000개 캐릭터 수집 게임

면접관이 “캐릭터가 1,000개가 되면 구조를 어떻게 바꿀 건가요?”라고 묻는다.

<b>반드시 언급:</b>

- master/user data 분리
- stable ID
- pagination/filter/search
- thumbnail lazy loading
- memory/disk cache
- pool/recycle scroll cell
- master revision
- inactive/deleted character policy
- analytics/logging

### 문제 B — 앱 시작 5초가 느리다

<b>답변 순서:</b>

1. startup profile 측정.
2. sync blocking 구간 확인.
3. login/master/image/SDK init 분리.
4. 반드시 필요한 데이터만 critical path.
5. 나머지는 lazy/background.
6. splash UX는 문제를 숨기는 용도가 아니라 실제 startup cost를 줄인 뒤 사용.

### 문제 C — 랭킹 화면 스크롤이 버벅인다

<b>확인:</b>

- instantiate 수
- cell pooling
- layout rebuild
- texture loading
- text update
- overdraw
- GC.Alloc
- 네트워크 pagination

### 문제 D — 광고 보상이 두 번 들어왔다

<b>확인:</b>

- SDK callback 중복
- show state
- callback 등록 중복
- 클라 reward flag
- server transaction ID/idempotency
- retry 정책

### 문제 E — 앱 업데이트 후 구버전 유저가 에러

<b>확인:</b>

- API contract breaking change
- master schema version
- optional/default field
- endpoint versioning
- minimum supported version 정책
- rollback 가능성
</toggle>

---

<toggle heading="h2">
26\. 압박 꼬리질문 대응

<b>“그건 그냥 검색해서 한 거 아닌가요?”</b>

> 검색이나 AI를 사용해 해결책 후보를 찾는 것은 당연히 활용합니다. 다만 실제 코드에 적용하려면 현재 구조와 생명주기, 성능 영향을 이해해야 합니다. 저는 적용 전후의 동작을 재현하고 diff와 테스트로 검증한 뒤 반영합니다.

<b>“왜 다른 방법은 안 썼죠?”</b>

> 당시 요구사항에서 가장 중요한 기준은 ○○였고, 대안 A는 △△ 비용, 대안 B는 □□ 리스크가 있었습니다. 그래서 현재 방식이 가장 단순하게 요구사항을 만족한다고 판단했습니다.

<b>“수치로 증명할 수 있나요?”</b>

> 당시 계측값을 정확히 기록하지 않은 부분은 수치를 만들지 않겠습니다. 확인된 결과는 크래시 재현이 사라진 것이고, 다시 한다면 Profiler capture와 전후 메모리 수치를 함께 남기겠습니다.

<b>“본인이 직접 한 게 어디까지죠?”</b>

> 요구사항 정리, 구조 판단, 구현/수정 방향 결정, 코드 검증과 배포 확인까지 제가 맡았습니다. AI 도구를 사용한 부분은 생성 속도 보조였고 최종 책임과 검증은 제가 했습니다.

<b>“게임 경력이 없는데 왜 뽑아야 하죠?”</b>

> 상용 Unity 클라이언트에서 실제 메모리와 UI 문제를 해결했고 팀 리딩 경험이 있습니다. 동시에 게임 도메인 부족을 보완하려고 하울리 도그를 직접 출시해 로그인, 광고, 랭킹, 서버 운영까지 경험했습니다. 그래서 Unity 실무 문제 해결 능력과 게임 출시 경험을 둘 다 보여드릴 수 있습니다.
</toggle>

---

## 27. 모의면접 랜덤 25문제

- [ ] 
  1. 자기소개 30초.
- [ ] 
  1. 가장 어려운 버그와 원인 추적 방법.
- [ ] 
  1. 값 타입/참조 타입.
- [ ] 
  1. GC.Alloc 원인 5개.
- [ ] 
  1. Texture 메모리 누수 조사 절차.
- [ ] 
  1. Pooling의 단점.
- [ ] 
  1. Dictionary 내부 원리.
- [ ] 
  1. event와 delegate 차이.
- [ ] 
  1. Coroutine과 async/await.
- [ ] 
  1. Awake/Start 차이.
- [ ] 
  1. CPU/GPU 병목 구분.
- [ ] 
  1. Draw Call/Overdraw 차이.
- [ ] 
  1. Canvas rebuild 최적화.
- [ ] 
  1. Addressables release.
- [ ] 
  1. MVC/MVVM/ECS.
- [ ] 
  1. Singleton 단점.
- [ ] 
  1. API 중복 요청 해결.
- [ ] 
  1. 구버전 API 호환.
- [ ] 
  1. merge/rebase 차이.
- [ ] 
  1. 코드리뷰 기준.
- [ ] 
  1. 팀원과 의견 충돌.
- [ ] 
  1. AI를 개발에 어떻게 사용하는가.
- [ ] 
  1. 하울리 도그에서 가장 어려웠던 설계.
- [ ] 
  1. 왜 슈퍼센트인가.
- [ ] 
  1. 입사 후 가장 먼저 강화하고 싶은 역량.

<b>합격선 자가채점</b>

- 0점: 답을 못함.
- 1점: 정의만 말함.
- 2점: 원리/장단점까지 말함.
- 3점: 실제 경험과 trade-off까지 연결.

<b>목표:</b> 25문제 평균 <b>2.5점 이상</b>, 핵심 1\~18번은 모두 3점에 가깝게 만든다.

---

## 28. 7일 회독 루틴

<b>1일차:</b> C# + 컬렉션. Q1\~Q8 구두 답변.

<b>2일차:</b> 알고리즘 + 코테 1\~5 직접 구현.

<b>3일차:</b> Unity lifecycle + Memory/GC. Profiler 설명 연습.

<b>4일차:</b> Rendering/UI/Asset/Architecture.

<b>5일차:</b> Network/Mobile/Git/AI/협업.

<b>6일차:</b> 내 경험 7개를 1분씩 녹음해서 들어보기. 군더더기 제거.

<b>7일차:</b> 랜덤 25문제 + 코테 2문제 + 시스템 설계 1문제. 노션을 보지 않는다.

<callout icon="🎯" color="red_bg">
<b>최종 원칙:</b> 암기 완료는 “읽으면 아는 것”이 아니다. <b>질문만 보고 30초 안에 구조를 잡고, 1분 안에 핵심을 말하며, 꼬리질문에서 원리와 trade-off를 방어할 수 있는 상태</b>다.
</callout>
</content>
</page>