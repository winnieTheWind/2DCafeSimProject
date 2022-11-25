using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System;
public class InstantiateObjects : MonoBehaviour
{
    [SerializeField] public GameObject deskToSpawn;
    [SerializeField] public GameObject tableToSpawn;
    [SerializeField] public GameObject chairToSpawn;
    [SerializeField] public GameObject metalDeskToSpawn;
    [SerializeField] public GameObject cashRegisterToSpawn;

    public struct ItemsData
    {
        public string name;
        public int quantity;
    }

    public static List<HandleExecute.ItemsData> purchaseItemData;
    public static List<HandleExecute.HireItemsData> hireItemData;

    Tilemap map;
    GameObject obj = null;
    public GameObject panel;
    GameObject closeShopButton;
    public Pathfinding pathFinder;
    public List<PathNode> path;

    bool isCreating = false;
    public bool hasBeenPlaced = false;

    public static Action<List<HandleExecute.ItemsData>> getDataEvent;
    public static Action handleMovementEvent;
    public static Action<Tilemap> getMapEvent;
    public static Action<Pathfinding> getPathFinderEvent;
    public static Action<GameObject> getGameObjectEvent;

    void Start()
    {
        map = GameObject.Find("Grid/Ground").GetComponent<Tilemap>();
        getMapEvent?.Invoke(map);
    }

    private void OnEnable()
    {
        PlaceObjectHandler.setHasBeenPlacedEvent += SetHasBeenPlaced;
        PlaceObjectHandler.setGameObjectEvent += SetGameObject;
        PlaceObjectHandler.setItemRemoveEvent += SetItemData;

        HandleExecute.sendPurchaseItemsListEvent += GetPurchasedItems;
        CashRegisterBehaviour.PathEvent += GetPath;
        InitGrid.getPathFinder += GetPathFinder;
    }

    private void OnDisable()
    {
        PlaceObjectHandler.setHasBeenPlacedEvent -= SetHasBeenPlaced;
        PlaceObjectHandler.setGameObjectEvent -= SetGameObject;
        HandleExecute.sendPurchaseItemsListEvent -= GetPurchasedItems;
        
        CashRegisterBehaviour.PathEvent -= GetPath;
        InitGrid.getPathFinder -= GetPathFinder;
    }
    private void Update()
    {
        if (isCreating == true)
        {
            StartCreation(purchaseItemData, path);
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }

    private void StartCreation(List<HandleExecute.ItemsData> itemData, List<PathNode> path)
    {
        if (itemData.Count != 0)
        {
            InstantiateItem("Desk", deskToSpawn);
            InstantiateItem("Table", tableToSpawn);
            InstantiateItem("Chair", chairToSpawn);
            InstantiateItem("Metal Desk", metalDeskToSpawn);

            if (itemData[0].name == "Cash Register")
            {
                if (obj == null)
                {
                    obj = Instantiate(cashRegisterToSpawn, transform.position, Quaternion.identity);
                    obj.GetComponent<CashRegisterBehaviour>().map = map;


                    // obj.GetComponent<CashRegisterBehaviour>().map = map;



                    hasBeenPlaced = false;
                }
            }
        }
        else
        {
            isCreating = false;
        }

        if (hasBeenPlaced == false)
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int vec = map.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
            Vector3 vec2 = map.GetCellCenterWorld(vec);

            if (obj != null)
            {
                getGameObjectEvent?.Invoke(obj);

                obj.transform.position = vec2;

                if (Keyboard.current.eKey.wasPressedThisFrame == true)
                {
                    Destroy(obj);
                    isCreating = false;
                }
            }
        }
        handleMovementEvent?.Invoke();
    }

    private void InstantiateItem(string type, GameObject prefabToSpawn)
    {
        if (purchaseItemData[0].name == type)
        {
            if (obj == null)
            {
                obj = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

                hasBeenPlaced = false;

            }
        }
    }
    
    void GetPathFinder(Pathfinding _pathFinder)
    {
        pathFinder = _pathFinder;
        getPathFinderEvent?.Invoke(pathFinder);
    }

    public void GetPurchasedItems(List<HandleExecute.ItemsData> purchasedList)
    {
        purchaseItemData = purchasedList;
        if (purchaseItemData.Count != 0)
        {
            isCreating = true;
        }
        getDataEvent?.Invoke(purchaseItemData);
    }
    
    void SetHasBeenPlaced(bool _bool) { hasBeenPlaced = _bool; }
    void SetGameObject(GameObject _obj) { obj = _obj; }
    void SetItemData(List<HandleExecute.ItemsData> _itemsData) { purchaseItemData = _itemsData; }
    void GetPath(List<PathNode> _path) { path = _path; }

}
