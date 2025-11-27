using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventory : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private Transform gridContainer;

    [Header("Prefab")]
    [SerializeField] private GameObject slotPrefab;

    [Header("Button")]
    [SerializeField] private Button backButton;

    private List<UISlot> slots = new List<UISlot>();

    private void Start()
    {
        backButton.onClick.AddListener(GoBack);
    }

    // 인벤토리 초기화
    public void InitInventoryUI(int slotCount)
    {
        // 기존 슬롯 제거
        foreach (UISlot slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();

        // 새 슬롯 생성
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, gridContainer);
            UISlot slot = slotObj.GetComponent<UISlot>();
            slots.Add(slot);
        }
    }

    // 인벤토리 업데이트
    public void UpdateInventory(Character character)
    {
        // 아이템 개수 표시
        itemCountText.text = $"{character.Inventory.Count} / 120";

        // 슬롯이 없으면 초기화
        if (slots.Count == 0)
        {
            InitInventoryUI(character.Inventory.Count);
        }

        // 각 슬롯에 아이템 할당
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < character.Inventory.Count)
            {
                slots[i].SetItem(character.Inventory[i], character);
                slots[i].gameObject.SetActive(true);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    private void GoBack()
    {
        gameObject.SetActive(false);
        UIManager.Instance.MainMenu.gameObject.SetActive(true);
        UIManager.Instance.MainMenu.UpdateCharacterInfo(GameManager.Instance.Player);
    }
}
