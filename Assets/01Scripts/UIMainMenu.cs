using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Character Info")]
    [SerializeField] private TextMeshProUGUI jobText;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Image expBarFill;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("Buttons")]
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;

    private void Start()
    {
        // 버튼 이벤트 연결
        statusButton.onClick.AddListener(OpenStatus);
        inventoryButton.onClick.AddListener(OpenInventory);
    }

    // 캐릭터 정보 업데이트
    public void UpdateCharacterInfo(Character character)
    {
        if (character == null)
        {
            Debug.LogError("Character is null!");
            return;
        }

        jobText.text = character.Job;
        characterNameText.text = character.Name;
        levelText.text = $"LV {character.Level}";

        // 경험치 표시
        expText.text = $"{character.Exp} / {character.MaxExp}";
        expBarFill.fillAmount = character.MaxExp > 0 ? (float)character.Exp / character.MaxExp : 0f;

        descriptionText.text = character.Description;

        // 골드 표시 (천 단위 콤마)
        goldText.text = character.Gold.ToString("N0");
    }

    private void OpenStatus()
    {
        gameObject.SetActive(false);
        UIManager.Instance.Status.gameObject.SetActive(true);
        UIManager.Instance.Status.UpdateCharacterInfo(GameManager.Instance.Player);
    }

    private void OpenInventory()
    {
        gameObject.SetActive(false);
        UIManager.Instance.Inventory.gameObject.SetActive(true);
        UIManager.Instance.Inventory.UpdateInventory(GameManager.Instance.Player);
    }
}
