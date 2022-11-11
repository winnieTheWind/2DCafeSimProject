using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;



public class InstantiateItems : MonoBehaviour
{
    [SerializeField] public GameObject deskToSpawn;
    [SerializeField] public GameObject tableToSpawn;

    public GameObject prefabTransform;

    bool hasPurchased = false;

    Tilemap map;

    List<ItemsData> purchaseItemList = new List<ItemsData>();

    int time = 0;
    public struct ItemsData
    {
        public string name;
        public int quantity;
    }

    public static List<ShopManager.ItemsData> itemData;

    GameObject panel;
    GameObject closeShopButton;


    void Start()
    {
        map = GameObject.Find("Grid/Ground").GetComponent<Tilemap>();
        ShopManager.SendItemsListEvent += GetPurchasedItems;
        panel = GameObject.Find("Canvas/Panel");
    }
    public void GetPurchasedItems(object sender, ShopManager.PurchaseItemsEventArgs e)
    {
        StartCoroutine(InstantiateGameObjectsSequence(e.itemData));
    }
    GameObject InstantiateDesk(GameObject objectToSpawn, Transform position, Quaternion quaternion)
    {
        return prefabTransform;
    }

    IEnumerator InstantiateGameObjectsSequence(List<ShopManager.ItemsData> itemsData)
    {
        panel.SetActive(false);
        foreach (var item in itemsData)
        {
            yield return StartCoroutine(PlaceObject(item.name, item.quantity, itemsData));
            yield return null;
        }

        itemsData.Clear();
        Debug.Log(itemsData.Count);
        if (itemsData.Count == 0)
        {
            panel.SetActive(true);
        }
    }

    IEnumerator PlaceObject(string name, int quantity, List<ShopManager.ItemsData> itemsData)
    {
        if (name == "Desk")
        {
            yield return StartCoroutine(SpawnObject(deskToSpawn, name, quantity, itemsData));
            yield return null;
        }
        else if (name == "Table")
        {
            yield return StartCoroutine(SpawnObject(tableToSpawn, name, quantity, itemsData));
            yield return null;
        }
    }

    IEnumerator SpawnObject(GameObject objectToSpawn, string name, int quantity, List<ShopManager.ItemsData> itemsData)
    {
        GameObject obj = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        var counter = 0;
        while (counter < quantity)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int vec = map.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
            Vector3 vec2 = map.GetCellCenterWorld(vec);
            obj.transform.position = vec2;

            if (Mouse.current.leftButton.wasPressedThisFrame == true)
            {
                counter++;
                Instantiate(objectToSpawn, vec2, Quaternion.identity);

            }
            yield return null;
        }
        if (counter == quantity)
        {
            obj.SetActive(false);
        }
    }
}
