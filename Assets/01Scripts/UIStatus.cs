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
        attackValueText.text = character.TotalAttack.ToString();
        defenseValueText.text = character.TotalDefense.ToString();
        hpValueText.text = character.TotalHP.ToString();
        criticalValueText.text = character.TotalCritical.ToString();
    }

    private void GoBack()
    {
        gameObject.SetActive(false);
        UIManager.Instance.MainMenu.gameObject.SetActive(true);
        UIManager.Instance.MainMenu.UpdateCharacterInfo(GameManager.Instance.Player);
    }
}
