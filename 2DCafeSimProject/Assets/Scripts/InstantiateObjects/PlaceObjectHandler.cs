using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System;

public class PlaceObjectHandler : MonoBehaviour
{
    private List<HandleExecute.ItemsData> itemData;

    private Tilemap map;
    private GameObject obj;
    private Pathfinding pathFinder;

    private bool hasBeenPlaced = false;
    private int counter = 0;

    public static Action<bool> setHasBeenPlacedEvent;
    public static Action<GameObject> setGameObjectEvent;
    public static Action<List<HandleExecute.ItemsData>> setItemRemoveEvent;

    private void OnEnable()
    {
        InstantiateObjects.getDataEvent += GetData;
        InstantiateObjects.getMapEvent += GetMap;
        InstantiateObjects.getGameObjectEvent += GetGameObject;
        InstantiateObjects.handleMovementEvent += MoveInstanceUpdate;
        InstantiateObjects.getPathFinderEvent += GetPathFinder;
    }

    private void OnDisable()
    {
        InstantiateObjects.getDataEvent -= GetData;
        InstantiateObjects.getMapEvent -= GetMap;
        InstantiateObjects.handleMovementEvent -= MoveInstanceUpdate;
        InstantiateObjects.getPathFinderEvent -= GetPathFinder;
        InstantiateObjects.getGameObjectEvent -= GetGameObject;
    }

    private void MoveInstanceUpdate() {
        if (Mouse.current.leftButton.wasPressedThisFrame == true)
        {
                if (itemData[0].name == "Desk" || itemData[0].name == "Chair" ||
                itemData[0].name == "Table" || itemData[0].name == "Metal Desk")
                {
                    counter = counter + 1;

                    if (itemData.Count > 0)
                    {
                        Vector3Int vecToWorld = map.WorldToCell(new Vector3(obj.transform.position.x, obj.transform.position.y, 0));
                        // pathFinder.GetNode(vecToWorld.x, vecToWorld.y).SetIsWalkable(false);

                        PlaceObject(itemData[0].name, itemData[0].quantity, itemData);
                    }
                }
                else if (itemData[0].name == "Cash Register")
                {
                    if (obj.GetComponent<CashRegisterBehaviour>().IsQueueLineColliding == false)
                    {
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
                }
        }
    }

    void PlaceObject(string name, int quantity, List<HandleExecute.ItemsData> itemData)
    {
        SetHasBeenPlaced(true);
        SetGameObject(null);

        if (counter >= quantity)
        {
            counter = 0;
            itemData.Remove(itemData[0]);
            SetItemData(itemData);
        }
    }
    public void SetHasBeenPlaced(bool _bool) { setHasBeenPlacedEvent?.Invoke(_bool); }
    public void SetGameObject(GameObject _obj) { setGameObjectEvent?.Invoke(_obj); }
    public void SetItemData(List<HandleExecute.ItemsData> _itemData) { setItemRemoveEvent?.Invoke(_itemData);} 

    private void GetGameObject(GameObject _object) { obj = _object; }
    private void GetPathFinder(Pathfinding _pathFinder) { pathFinder = _pathFinder; }
    private void GetData(List<HandleExecute.ItemsData> purchasedList) { itemData = purchasedList; }
    private void GetMap(Tilemap _map) { map = _map; }

    
}
