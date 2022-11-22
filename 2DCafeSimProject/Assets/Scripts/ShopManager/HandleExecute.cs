using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class HandleExecute : MonoBehaviour
{
    public ShopItemSO[] furnitureShopItemsSO;
    public ShopItemSO[] equipmentShopItemsSO;
    public ShopItemSO[] hireShopItemsSO;
    
    private List<ItemsData> purchaseItemlist = new List<ItemsData>();
    private List<HireItemsData> hireItemList = new List<HireItemsData>();

    public static Action<List<ItemsData>> sendPurchaseItemsListEvent;
    public static Action<List<HireItemsData>> sendHireItemsListEvent;

    public struct ItemsData
    {
        public string name;
        public int quantity;
    }

    public struct HireItemsData
    {
        public string name;
    }

    private void OnEnable()
    {
        HandleItems.passFurnitureShopItemsEvent += GetFurnitureItems;
        HandleItems.passEquipmentShopItemsEvent += GetEquipmentItems;
        HandleItems.passHireShopItemsEvent += GetHireItems;

        PurchaseButtonHandler.PurchaseEvent += PurchaseItems;
        PurchaseButtonHandler.HireEvent += HireItems;
    }

    private void OnDisable()
    {
        PurchaseButtonHandler.PurchaseEvent -= PurchaseItems;
        PurchaseButtonHandler.HireEvent -= HireItems;

        HandleItems.passFurnitureShopItemsEvent -= GetFurnitureItems;
        HandleItems.passEquipmentShopItemsEvent -= GetEquipmentItems;
        HandleItems.passHireShopItemsEvent -= GetHireItems;
    }

    private void GetFurnitureItems(ShopItemSO[] _furnitureShopItemsSO)
    {
        furnitureShopItemsSO = _furnitureShopItemsSO;
    }
    private void GetEquipmentItems(ShopItemSO[] _equipmentShopItemsSO)
    {
        equipmentShopItemsSO = _equipmentShopItemsSO;
    }
    private void GetHireItems(ShopItemSO[] _hireShopItemsSO)
    {
        hireShopItemsSO = _hireShopItemsSO;
    }

    private void PurchaseItems()
    {
        purchaseItemlist.Clear();
        SetItems(furnitureShopItemsSO);
        SetItems(equipmentShopItemsSO);

        sendPurchaseItemsListEvent?.Invoke(purchaseItemlist);
    }

    private void HireItems()
    {
        hireItemList.Clear();
        SetHireItems(hireShopItemsSO);

        sendHireItemsListEvent?.Invoke(hireItemList);
    }

    private void SetItems(ShopItemSO[] typeShopItemsSO)
    {
        for (int i = 0; i < typeShopItemsSO.Length; i++)
        {
            if (typeShopItemsSO[i].quantityToBuy > 0)
            {
                ItemsData item = new ItemsData();
                item.name = typeShopItemsSO[i].name;
                item.quantity = typeShopItemsSO[i].quantityToBuy;
                purchaseItemlist.Add(item);
            }
        }
    }
    private void SetHireItems(ShopItemSO[] typeShopItemsSO)
    {
        for (int i = 0; i < typeShopItemsSO.Length; i++)
        {
            HireItemsData item = new HireItemsData();
            item.name = typeShopItemsSO[i].name;
            hireItemList.Add(item);
        }
    }

    private int SortFunc(ItemsData a, ItemsData b)
    {
        if (a.quantity < b.quantity)
        {
            return -1;
        }
        else if (a.quantity > b.quantity)
        {
            return 1;
        }
        return 0;
    }
}
