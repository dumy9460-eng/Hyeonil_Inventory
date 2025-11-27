using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private ItemDatabase itemDatabase;

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

        LoadItemData();
    }

    // JSON에서 아이템 데이터 로드
    private void LoadItemData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Items");

        if (jsonFile == null)
        {
            Debug.LogError("Items.json 파일을 찾을 수 없습니다! Resources 폴더에 있는지 확인하세요.");
            return;
        }

        try
        {
            itemDatabase = JsonConvert.DeserializeObject<ItemDatabase>(jsonFile.text);
            Debug.Log($"아이템 데이터 로드 완료: {itemDatabase.items.Count}개");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"JSON 파싱 에러: {e.Message}");
        }
    }

    // 아이템 ID로 아이템 가져오기
    public Item GetItemByName(string itemName)
    {
        if (itemDatabase == null || itemDatabase.items == null)
        {
            Debug.LogError("아이템 데이터베이스가 로드되지 않았습니다!");
            return null;
        }

        foreach (Item item in itemDatabase.items)
        {
            if (item.Name == itemName)
            {
                return item;
            }
        }

        Debug.LogWarning($"아이템을 찾을 수 없습니다: {itemName}");
        return null;
    }

    // 모든 아이템 리스트 가져오기
    public ItemDatabase GetAllItems()
    {
        return itemDatabase;
    }
}
