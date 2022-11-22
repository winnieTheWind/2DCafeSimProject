using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HandleItems : MonoBehaviour
{
    public ShopItemSO[] furnitureShopItemsSO;
    public ShopItemSO[] equipmentShopItemsSO;
    public ShopItemSO[] hireShopItemsSO;


    public ShopTemplate[] furnitureShopPanels;
    public ShopTemplate[] equipmentShopPanels;
    public ShopTemplate[] hireShopPanels;

    private bool isSettingItems;

    public static Action<ShopItemSO[]> passFurnitureShopItemsEvent;
    public static Action<ShopItemSO[]> passEquipmentShopItemsEvent;
    public static Action<ShopItemSO[]> passHireShopItemsEvent;



    private void OnEnable()
    {
        HandleShop.HandleItemsEvent += _SetItems;

        SetQuantity(furnitureShopItemsSO, 0);
        SetQuantity(equipmentShopItemsSO, 0);
        SetQuantity(hireShopItemsSO, 0);

    }
    private void OnDisable()
    {
        HandleShop.HandleItemsEvent -= _SetItems;
    }

    private void Update()
    {
        if (isSettingItems)
        {
            SetPanels();
        }
        else
        {
            return;
        }
    }
    private void _SetItems()
    {
        isSettingItems = true;

        passFurnitureShopItemsEvent?.Invoke(furnitureShopItemsSO);
        passEquipmentShopItemsEvent?.Invoke(equipmentShopItemsSO);
        passHireShopItemsEvent?.Invoke(hireShopItemsSO);
    }
    private void SetPanels()
    {
        for (int i = 0; i < furnitureShopItemsSO.Length; i++)
        {
            furnitureShopPanels[i].tileTxt.text = furnitureShopItemsSO[i].name;
            furnitureShopPanels[i].quantityTxt.text = furnitureShopItemsSO[i].quantityToBuy.ToString();
        }
        for (int i = 0; i < equipmentShopItemsSO.Length; i++)
        {
            equipmentShopPanels[i].tileTxt.text = equipmentShopItemsSO[i].name;
            equipmentShopPanels[i].quantityTxt.text = equipmentShopItemsSO[i].quantityToBuy.ToString();

        }
        for (int i = 0; i < hireShopItemsSO.Length; i++)
        {
            hireShopPanels[i].tileTxt.text = hireShopItemsSO[i].name;
        }
    }

    private void SetQuantity(ShopItemSO[] shopItemsSO, int quantityNumber)
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopItemsSO[i].quantityToBuy = quantityNumber;
        }
    }
}
