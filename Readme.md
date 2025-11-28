\# 스파르타 던전 - Unity 인벤토리 시스템



Unity 2022.3.62f 기반 RPG 인벤토리 시스템 구현 프로젝트



\##  프로젝트 개요



C# 텍스트 RPG를 Unity UGUI 기반으로 전환한 인벤토리 시스템입니다.

MainScene 내에서 3개의 UI(MainMenu, Status, Inventory)를 전환하며 동작합니다.



\##  주요 기능



\### 1. UI 네비게이션 시스템

\- \*\*MainScene\*\*: 캐릭터 정보(ID, 레벨, 골드, 경험치) 표시

\- \*\*Status\*\*: 캐릭터 스탯 확인 (공격력, 방어력, 체력, 치명타)

\- \*\*Inventory\*\*: 아이템 목록 및 장착 관리 (3x3 그리드)

\- 각 UI 간 버튼 클릭으로 자유로운 전환 가능



\### 2. JSON 기반 데이터 관리

\- \*\*Newtonsoft.Json\*\* 패키지 사용

\- `Resources/Items.json`에서 아이템 데이터 로드

\- 아이템 속성: 이름, 타입(Weapon/Armor/Accessory), 스탯 보너스, 아이콘



\### 3. 인벤토리 시스템

\- \*\*동적 슬롯 생성\*\*: 아이템 개수에 따라 UISlot 프리팹 자동 생성

\- \*\*아이템 장착/해제\*\*: 

&nbsp; - 아이템 클릭 시 장착/해제 토글

&nbsp; - 노란색 배지(아이템 아이콘 우측에 노란색 사각형으로 구현)로 장착 상태 표시

&nbsp; - 같은 타입 중복 장착 방지

\- \*\*Console 로그\*\*: 장착/해제 시 "\[장착] 낡은 검" 형태로 로그 출력



\### 4. 스탯 시스템

\- \*\*기본 스탯 + 장비 보너스\*\* 자동 계산

\- 장착한 아이템의 보너스가 실시간으로 Status UI에 반영

\- 예: 기본 공격력 10 + 검 보너스 5 = 총 공격력 15



\##  기술 스택



\- \*\*Unity\*\*: 2022.3.62f

\- \*\*UI\*\*: UGUI (Canvas, ScrollView, Grid Layout)

\- \*\*데이터\*\*: Newtonsoft.Json

\- \*\*패턴\*\*: Singleton (GameManager, UIManager, DataManager)

\- \*\*아키텍처\*\*: MVC 패턴 기반 분리



\##  프로젝트 구조

```

Assets/

├── 00Scenes/

│   └── MainScene.unity

├── 01Scripts/

│   ├── GameManager.cs       # 게임 전체 관리

│   ├── DataManager.cs       # JSON 데이터 로드

│   ├── UIManager.cs         # UI 전환 관리

│   ├── Character.cs         # 캐릭터 데이터 클래스

│   ├── Item.cs              # 아이템 데이터 클래스

│   ├── UIMainMenu.cs        # 메인 화면 UI

│   ├── UIStatus.cs          # 스탯 화면 UI

│   ├── UIInventory.cs       # 인벤토리 UI

│   └── UISlot.cs            # 아이템 슬롯

├── 02Prefabs/

│   └── UISlot.prefab        # 인벤토리 슬롯 프리팹

├── Resources/

│   ├── Items.json           # 아이템 데이터

│   └── ItemIcons/           # 아이템 아이콘 스프라이트

└── Sprites/

&nbsp;   ├── Characters/          # 캐릭터 이미지

&nbsp;   └── Items/               # 아이템 아이콘

```



\##  핵심 구현 내용



\### JSON 데이터 로드

```csharp

// DataManager.cs

TextAsset jsonFile = Resources.Load("Items");

ItemDatabase itemDatabase = JsonConvert.DeserializeObject(jsonFile.text);

```



\### 아이템 장착 로직

```csharp

// UISlot.cs

if (character.EquippedItems.Contains(currentItem))

{

&nbsp;   character.UnEquip(currentItem);

&nbsp;   Debug.Log($"\[장착 해제] {currentItem.Name}");

}

else

{

&nbsp;   if (character.CanEquip(currentItem))

&nbsp;   {

&nbsp;       character.Equip(currentItem);

&nbsp;       Debug.Log($"\[장착] {currentItem.Name}");

&nbsp;   }

}

```



\### 스탯 계산

```csharp

// Character.cs

public int TotalAttack => BaseAttack + GetEquipmentBonus("Attack");

public int TotalDefense => BaseDefense + GetEquipmentBonus("Defense");

```



\##  실행 방법



1\. Unity 2022.3.62f 설치

2\. 프로젝트 열기

3\. Newtonsoft.Json 패키지 설치 (Package Manager)

4\. MainScene 열기

5\. Play 버튼 클릭



\## 개발 노트



\- \*\*싱글톤 패턴\*\*: GameManager, UIManager, DataManager

\- \*\*데이터 클래스\*\*: Character, Item은 MonoBehaviour 상속 없음

\- \*\*동적 UI 생성\*\*: Instantiate로 슬롯 프리팹 생성

\- \*\*Script Execution Order\*\*: DataManager(-200) → GameManager(-100) → UIManager(-50)

&nbsp;	매니저들 간의 실행 순서 설정.

