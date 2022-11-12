using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class InstantiateItems : MonoBehaviour
{
    [SerializeField] public GameObject deskToSpawn;
    [SerializeField] public GameObject tableToSpawn;
    [SerializeField] public GameObject chairToSpawn;
    [SerializeField] public GameObject metalDeskToSpawn;

    public GameObject prefabTransform;

    bool hasPurchased = false;
    bool hasBeenPlaced = false;

    Tilemap map;

    public struct ItemsData
    {
        public string name;
        public int quantity;
    }

    int counter = 0;

    public static List<ShopManager.ItemsData> itemData;

    GameObject panel;
    GameObject closeShopButton;

    GameObject objPointer;

    bool isCreating = false;

    void Start()
    {
        map = GameObject.Find("Grid/Ground").GetComponent<Tilemap>();
        ShopManager.SendItemsListEvent += GetPurchasedItems;
        panel = GameObject.Find("Canvas/Panel");
    }
    public void GetPurchasedItems(object sender, ShopManager.PurchaseItemsEventArgs e)
    {
        itemData = e.itemData;

        if (itemData.Count != 0)
        {
            isCreating = true;

        }
    }

    private void Update()
    {
        if (isCreating == true)
        {
            StartCreation(itemData);
            this.panel.SetActive(false);
        } else {
            this.panel.SetActive(true);
        }
    }

    GameObject obj = null;
    void StartCreation(List<ShopManager.ItemsData> itemData)
    {
        if (itemData.Count != 0)
        {
            if (itemData[0].name == "Desk")
            {
                if (obj == null)
                {
                    obj = Instantiate(deskToSpawn, transform.position, Quaternion.identity);
                    hasBeenPlaced = false;
                }
            }
            else if (itemData[0].name == "Table")
            {
                if (obj == null)
                {
                    obj = Instantiate(tableToSpawn, transform.position, Quaternion.identity);
                    hasBeenPlaced = false;
                }
            } else if (itemData[0].name == "Chair")
            {
                if (obj == null)
                {
                    obj = Instantiate(chairToSpawn, transform.position, Quaternion.identity);
                    hasBeenPlaced = false;
                }
            } else if (itemData[0].name == "Metal Desk")
            {
                if (obj == null)
                {
                    obj = Instantiate(metalDeskToSpawn, transform.position, Quaternion.identity);
                    hasBeenPlaced = false;
                }
            } 
        } else {
            isCreating = false;
        }

        if (hasBeenPlaced == false)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int vec = map.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
            Vector3 vec2 = map.GetCellCenterWorld(vec);

            if (obj != null)
            {
                obj.transform.position = vec2;

            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame == true)
        {
            counter = counter + 1;

            if (itemData.Count > 0)
            {
                PlaceObject(itemData[0].name, itemData[0].quantity, itemData);
            }
        }
    }

    void PlaceObject(string name, int quantity, List<ShopManager.ItemsData> itemData)
    {
        hasBeenPlaced = true;
        obj = null;

        if (counter >= quantity)
        {
            counter = 0;
            itemData.Remove(itemData[0]);
        }
    }

    // void InstantiateObject(GameObject objToSpawn)
    // {
    //     GameObject obj = Instantiate(objToSpawn, transform.position, Quaternion.identity);
    //     Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    //     Vector3Int vec = map.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
    //     Vector3 vec2 = map.GetCellCenterWorld(vec);
    //     obj.transform.position = vec2;

    // }

    // GameObject InstantiateDesk(GameObject objectToSpawn, Transform position, Quaternion quaternion)
    // {
    //     return prefabTransform;
    // }

    // IEnumerator InstantiateGameObjectsSequence(List<ShopManager.ItemsData> itemsData)
    // {
    //     panel.SetActive(false);
    //     foreach (var item in itemsData)
    //     {
    //         yield return StartCoroutine(PlaceObject(item.name, item.quantity, itemsData));
    //         yield return null;
    //     }

    //     itemsData.Clear();
    //     Debug.Log(itemsData.Count);
    //     if (itemsData.Count == 0)
    //     {
    //         panel.SetActive(true);
    //     }
    // }

    // IEnumerator PlaceObject(string name, int quantity, List<ShopManager.ItemsData> itemsData)
    // {
    //     if (name == "Desk")
    //     {
    //         yield return StartCoroutine(SpawnObject(deskToSpawn, name, quantity, itemsData));
    //         yield return null;
    //     }
    //     else if (name == "Table")
    //     {
    //         yield return StartCoroutine(SpawnObject(tableToSpawn, name, quantity, itemsData));
    //         yield return null;
    //     }
    // }

    // IEnumerator SpawnObject(GameObject objectToSpawn, string name, int quantity, List<ShopManager.ItemsData> itemsData)
    // {
    //     GameObject obj = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    //     var counter = 0;
    //     while (counter < quantity)
    //     {
    //         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    //         Vector3Int vec = map.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
    //         Vector3 vec2 = map.GetCellCenterWorld(vec);
    //         obj.transform.position = vec2;

    //         if (Mouse.current.leftButton.wasPressedThisFrame == true)
    //         {
    //             counter++;
    //             Instantiate(objectToSpawn, vec2, Quaternion.identity);

    //         }
    //         yield return null;
    //     }
    //     if (counter == quantity)
    //     {
    //         obj.SetActive(false);
    //     }
    // }
}
