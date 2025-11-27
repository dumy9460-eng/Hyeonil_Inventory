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
    private Character currentCharacter;

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
            if (slot != null)
            {
                Destroy(slot.gameObject);
            }
        }
        slots.Clear();

        // 새 슬롯 생성
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, gridContainer);
            UISlot slot = slotObj.GetComponent<UISlot>();

            if (slot != null)
            {
                slots.Add(slot);
            }
            else
            {
                Debug.LogError("UISlot component not found on prefab!");
            }
        }
    }

    // 인벤토리 업데이트
    public void UpdateInventory(Character character)
    {
        if (character == null)
        {
            Debug.LogError("Character is null!");
            return;
        }

        currentCharacter = character;

        // 아이템 개수 표시
        itemCountText.text = $"{character.Inventory.Count} / 120";

        // 슬롯이 부족하면 생성
        if (slots.Count < character.Inventory.Count)
        {
            int slotsToCreate = character.Inventory.Count - slots.Count;
            for (int i = 0; i < slotsToCreate; i++)
            {
                GameObject slotObj = Instantiate(slotPrefab, gridContainer);
                UISlot slot = slotObj.GetComponent<UISlot>();
                if (slot != null)
                {
                    slots.Add(slot);
                }
            }
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

    // 인벤토리 새로고침 (장착 상태 변경 시)
    public void RefreshInventory()
    {
        if (currentCharacter != null)
        {
            foreach (UISlot slot in slots)
            {
                if (slot.gameObject.activeSelf)
                {
                    slot.RefreshUI();
                }
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
