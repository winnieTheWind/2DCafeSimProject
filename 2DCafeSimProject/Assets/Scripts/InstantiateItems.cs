using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System;



public class InstantiateItems : MonoBehaviour
{
    [SerializeField] public GameObject deskToSpawn;
    [SerializeField] public GameObject tableToSpawn;
    [SerializeField] public GameObject chairToSpawn;
    [SerializeField] public GameObject metalDeskToSpawn;
    [SerializeField] public GameObject cashRegisterToSpawn;

    bool hasPurchased = false;
    public bool hasBeenPlaced = false;

    bool hasObjectTouchedPath = false;

    Tilemap map;

    public struct ItemsData
    {
        public string name;
        public int quantity;
    }

    int counter = 0;
    int limit = 6;

    public static List<ShopManager.ItemsData> itemData;

    GameObject panel;
    GameObject closeShopButton;

    GameObject objPointer;

    bool isCreating = false;

    int increment = 0;

    public List<PathNode> path;

    public Pathfinding pathFinder;

    GameObject obj = null;

    public Sprite redMarker;

    private bool isColliding;


    void Start()
    {
        map = GameObject.Find("Grid/Ground").GetComponent<Tilemap>();
        ShopManager.SendItemsListEvent += GetPurchasedItems;
        // ShopManager.SendItemsListEvent += GetPurchasedItems;
        CashRegisterBehaviour.PathEvent += GetPath;

        panel = GameObject.Find("Canvas/Panel");

        InitGrid.getPathFinderAction += GetPathFinder;
    }

    void GetPathFinder(Pathfinding _pathFinder)
    {
        pathFinder = _pathFinder;
    }

    void GetColliderEvent(bool _isColliding)
    {
        _isColliding = isColliding;
    }

    private void Update()
    {
        if (isCreating == true)
        {
            BeaconBehaviour.CollidingWithWallEvent += GetColliderEvent;

            

            StartCreation(itemData, path);
            this.panel.SetActive(false);
        }
        else
        {
            this.panel.SetActive(true);
            BeaconBehaviour.CollidingWithWallEvent -= GetColliderEvent;

        }
    }

    void GetPath(List<PathNode> _path)
    {
        path = _path;

    }

    public void GetPurchasedItems(object sender, ShopManager.PurchaseItemsEventArgs e)
    {
        itemData = e.itemData;

        if (itemData.Count != 0)
        {
            isCreating = true;

        }
    }

    void StartCreation(List<ShopManager.ItemsData> itemData, List<PathNode> path)
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
            }
            else if (itemData[0].name == "Chair")
            {
                if (obj == null)
                {
                    obj = Instantiate(chairToSpawn, transform.position, Quaternion.identity);
                    hasBeenPlaced = false;
                }
            }
            else if (itemData[0].name == "Metal Desk")
            {
                if (obj == null)
                {
                    obj = Instantiate(metalDeskToSpawn, transform.position, Quaternion.identity);
                    hasBeenPlaced = false;
                }
            }
            else if (itemData[0].name == "Cash Register")
            {
                if (obj == null)
                {
                    obj = Instantiate(cashRegisterToSpawn, transform.position, Quaternion.identity);
                    obj.GetComponent<CashRegisterBehaviour>().map = map;

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
                obj.transform.position = vec2;

                if (Keyboard.current.eKey.wasPressedThisFrame == true)
                {
                    Debug.Log("exit placement");
                    // obj.gameObject.destrit
                    Destroy(obj);
                    counter = 0;
                    isCreating = false;

                }
            }


        }



        if (Mouse.current.leftButton.wasPressedThisFrame == true)
        {

            // if (path != null)
            // {
            // }
            if (hasObjectTouchedPath == false)
            {
                if (itemData[0].name == "Desk" || itemData[0].name == "Chair" ||
                itemData[0].name == "Table" || itemData[0].name == "Metal Desk")
                {

                    // refreshPathEvent?.Invoke();

                    counter = counter + 1;

                    if (itemData.Count > 0)
                    {
                        Vector3Int vecToWorld = map.WorldToCell(new Vector3(obj.transform.position.x, obj.transform.position.y, 0));
                        pathFinder.GetNode(vecToWorld.x, vecToWorld.y).SetIsWalkable(false);

                        PlaceObject(itemData[0].name, itemData[0].quantity, itemData);
                    }
                }
                else if (itemData[0].name == "Cash Register")
                {
                    if (obj.GetComponent<CashRegisterBehaviour>().IsQueueLineColliding == false)
                    {
                        Debug.Log("placed and queue isnt colliding");
                        if (obj.GetComponent<CashRegisterBehaviour>().isCashRegisterTouchingDesk == true)
                        {
                            counter = counter + 1;

                            if (itemData.Count > 0)
                            {
                                obj.GetComponent<CashRegisterBehaviour>().HasBeenPlaced = true;
                                PlaceObject(itemData[0].name, itemData[0].quantity, itemData);
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("cannot place, queue is colliding");
                    }
                }
            }
        }
    }

    GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
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
}
