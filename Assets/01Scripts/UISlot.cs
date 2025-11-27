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
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnSlotClick);
        }
    }

    // 아이템 설정
    public void SetItem(Item item, Character owner)
    {
        currentItem = item;
        character = owner;

        if (item != null)
        {
            // TODO: 실제 아이콘 스프라이트 로드 (STEP 6에서 구현)
            // itemIcon.sprite = Resources.Load<Sprite>($"ItemIcons/{item.IconName}");

            // 임시: 아이콘 색상으로 아이템 타입 표시
            switch (item.Type)
            {
                case "Weapon":
                    itemIcon.color = new Color(1f, 0.7f, 0.7f); // 연한 빨강
                    break;
                case "Armor":
                    itemIcon.color = new Color(0.7f, 0.7f, 1f); // 연한 파랑
                    break;
                case "Accessory":
                    itemIcon.color = new Color(1f, 1f, 0.7f); // 연한 노랑
                    break;
                default:
                    itemIcon.color = Color.white;
                    break;
            }

            RefreshUI();
        }
    }

    // UI 갱신
    public void RefreshUI()
    {
        if (currentItem == null || character == null)
            return;

        // 장착 여부 확인
        bool isEquipped = character.EquippedItems.Contains(currentItem);

        if (equipBadge != null)
        {
            equipBadge.SetActive(isEquipped);
        }
    }

    // 슬롯 클릭 이벤트
    public void OnSlotClick()
    {
        if (currentItem == null || character == null)
            return;

        // 장착/해제 토글
        if (character.EquippedItems.Contains(currentItem))
        {
            // 장착 해제
            character.UnEquip(currentItem);
            Debug.Log($"{currentItem.Name} 장착 해제");
        }
        else
        {
            // 장착 시도
            if (character.CanEquip(currentItem))
            {
                character.Equip(currentItem);
                Debug.Log($"{currentItem.Name} 장착");
            }
            else
            {
                Debug.Log($"{currentItem.Name} 장착 불가 (같은 타입이 이미 장착됨)");
            }
        }

        // UI 갱신
        RefreshUI();

        // 전체 인벤토리 새로고침
        UIManager.Instance.Inventory.RefreshInventory();

        // Status UI가 활성화되어 있다면 업데이트
        if (UIManager.Instance.Status.gameObject.activeSelf)
        {
            UIManager.Instance.Status.UpdateCharacterInfo(character);
        }
    }
}
