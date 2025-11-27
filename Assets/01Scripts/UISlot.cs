using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject equipBadge;

    private Item currentItem;
    private Character character;

    // 아이템 설정
    public void SetItem(Item item, Character owner)
    {
        currentItem = item;
        character = owner;

        // TODO: 실제 아이콘 스프라이트 로드
        // itemIcon.sprite = Resources.Load<Sprite>($"ItemIcons/{item.IconName}");

        RefreshUI();
    }

    // UI 갱신
    public void RefreshUI()
    {
        if (currentItem == null)
            return;

        // 장착 여부 확인
        bool isEquipped = character.EquippedItems.Contains(currentItem);
        equipBadge.SetActive(isEquipped);
    }

    // 슬롯 클릭 이벤트 (나중에 구현)
    public void OnSlotClick()
    {
        if (currentItem == null)
            return;

        // 장착/해제 토글
        if (character.EquippedItems.Contains(currentItem))
        {
            character.UnEquip(currentItem);
        }
        else
        {
            character.Equip(currentItem);
        }

        RefreshUI();

        // 인벤토리 UI 갱신
        UIManager.Instance.Inventory.UpdateInventory(character);
    }
}
