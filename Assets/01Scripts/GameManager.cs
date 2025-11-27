using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Character Player { get; private set; }

    private void Awake()
    {
        // 싱글톤 패턴
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

        // 임시 아이템 추가 (STEP 6에서 JSON으로 대체 예정)
        AddTemporaryItems();
    }

    // 임시 아이템 데이터 (테스트용)
    private void AddTemporaryItems()
    {
        Player.AddItem(new Item(
            name: "낡은 검",
            type: "Weapon",
            description: "오래된 검이지만 쓸만하다.",
            attack: 5,
            defense: 0,
            hp: 0,
            critical: 2,
            iconName: "sword_01"
        ));

        Player.AddItem(new Item(
            name: "가죽 갑옷",
            type: "Armor",
            description: "기본적인 방어구.",
            attack: 0,
            defense: 8,
            hp: 10,
            critical: 0,
            iconName: "armor_01"
        ));

        Player.AddItem(new Item(
            name: "수련자 반지",
            type: "Accessory",
            description: "수련자를 위한 반지.",
            attack: 2,
            defense: 2,
            hp: 5,
            critical: 3,
            iconName: "ring_01"
        ));

        // 기본 아이템들 자동 장착
        if (Player.Inventory.Count > 0)
        {
            Player.Equip(Player.Inventory[0]); // 검 장착
        }
        if (Player.Inventory.Count > 1)
        {
            Player.Equip(Player.Inventory[1]); // 갑옷 장착
        }
    }

    // UI 초기화
    private void InitializeUI()
    {
        if (UIManager.Instance != null && UIManager.Instance.MainMenu != null)
        {
            UIManager.Instance.MainMenu.UpdateCharacterInfo(Player);
        }
        else
        {
            Debug.LogError("UIManager or MainMenu is null!");
        }
    }

    // 모든 UI 새로고침
    public void RefreshAllUI()
    {
        // 현재 활성화된 UI 확인 후 업데이트
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
