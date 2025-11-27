using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Character Player { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(InitializeGame());
    }

    private IEnumerator InitializeGame()
    {
        // DataManager 로드 대기
        yield return new WaitUntil(() => DataManager.Instance != null);
        yield return null;

        SetData();
        InitializeUI();
    }

    // 플레이어 데이터 초기화
    private void SetData()
    {
        // 캐릭터 생성
        Player = new Character(
            name: "Chad",
            job: "코딩노예",
            level: 10,
            exp: 9,
            maxExp: 12,
            gold: 20000,
            attack: 10,
            defense: 5,
            hp: 100,
            critical: 15,
            description: "코딩의 노예가 되지 10년째라 되는 미숙입니다. 오늘도 밤샘만 남아서 치킨을 시켜 먹도 모르던는 생각에 대떼릴 키고 잇내요."
        );

        // JSON에서 아이템 로드
        LoadItemsFromJSON();
    }

    // JSON에서 아이템 로드 및 추가
    private void LoadItemsFromJSON()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager가 없습니다!");
            return;
        }

        // 아이템 3개 추가 (JSON에서 가져오기)
        Item sword = DataManager.Instance.GetItemByName("낡은 검");
        Item armor = DataManager.Instance.GetItemByName("가죽 갑옷");
        Item ring = DataManager.Instance.GetItemByName("수련자 반지");

        if (sword != null) Player.AddItem(sword);
        if (armor != null) Player.AddItem(armor);
        if (ring != null) Player.AddItem(ring);

        // 기본 아이템 장착
        if (Player.Inventory.Count > 0) Player.Equip(Player.Inventory[0]);
        if (Player.Inventory.Count > 1) Player.Equip(Player.Inventory[1]);

        Debug.Log($"플레이어 인벤토리: {Player.Inventory.Count}개 아이템");
    }

    // UI 초기화
    private void InitializeUI()
    {
        if (UIManager.Instance != null && UIManager.Instance.MainMenu != null)
        {
            UIManager.Instance.MainMenu.UpdateCharacterInfo(Player);
        }
    }

    // 모든 UI 새로고침
    public void RefreshAllUI()
    {
        if (UIManager.Instance.MainMenu.gameObject.activeSelf)
        {
            UIManager.Instance.MainMenu.UpdateCharacterInfo(Player);
        }
        if (UIManager.Instance.Status.gameObject.activeSelf)
        {
            UIManager.Instance.Status.UpdateCharacterInfo(Player);
        }
        if (UIManager.Instance.Inventory.gameObject.activeSelf)
        {
            UIManager.Instance.Inventory.UpdateInventory(Player);
        }
    }
}
