using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject uiMainMenu;
    [SerializeField] private GameObject uiStatus;
    [SerializeField] private GameObject uiInventory;

    // 프로퍼티로 접근 가능하도록
    public UIMainMenu MainMenu { get; private set; }
    public UIStatus Status { get; private set; }
    public UIInventory Inventory { get; private set; }

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
        }

        // UI 컴포넌트 가져오기
        MainMenu = uiMainMenu.GetComponent<UIMainMenu>();
        Status = uiStatus.GetComponent<UIStatus>();
        Inventory = uiInventory.GetComponent<UIInventory>();
    }
}
