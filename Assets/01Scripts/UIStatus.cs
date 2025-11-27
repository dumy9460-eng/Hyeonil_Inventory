using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatus : MonoBehaviour
{
    [Header("Stat Values")]
    [SerializeField] private TextMeshProUGUI attackValueText;
    [SerializeField] private TextMeshProUGUI defenseValueText;
    [SerializeField] private TextMeshProUGUI hpValueText;
    [SerializeField] private TextMeshProUGUI criticalValueText;

    [Header("Button")]
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(GoBack);
    }

    // 캐릭터 정보 업데이트
    public void UpdateCharacterInfo(Character character)
    {
        if (character == null)
        {
            Debug.LogError("Character is null!");
            return;
        }

        // 장비 보너스 계산
        int attackBonus = character.TotalAttack - character.BaseAttack;
        int defenseBonus = character.TotalDefense - character.BaseDefense;
        int hpBonus = character.TotalHP - character.BaseHP;
        int criticalBonus = character.TotalCritical - character.BaseCritical;

        // 스탯 표시 (보너스가 있으면 +표시)
        attackValueText.text = attackBonus > 0
            ? $"{character.TotalAttack}"
            : character.TotalAttack.ToString();

        defenseValueText.text = defenseBonus > 0
            ? $"{character.TotalDefense}"
            : character.TotalDefense.ToString();

        hpValueText.text = hpBonus > 0
            ? $"{character.TotalHP}"
            : character.TotalHP.ToString();

        criticalValueText.text = criticalBonus > 0
            ? $"{character.TotalCritical}"
            : character.TotalCritical.ToString();
    }

    private void GoBack()
    {
        gameObject.SetActive(false);
        UIManager.Instance.MainMenu.gameObject.SetActive(true);
        UIManager.Instance.MainMenu.UpdateCharacterInfo(GameManager.Instance.Player);
    }
}
