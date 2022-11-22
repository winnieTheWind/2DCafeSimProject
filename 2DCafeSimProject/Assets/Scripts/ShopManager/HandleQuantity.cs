using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleQuantity : MonoBehaviour
{
    private ShopItemSO[] furnitureShopItemsSO;
    private ShopItemSO[] equipmentShopItemsSO;

    private void OnEnable() {
        HandleItems.passFurnitureShopItemsEvent += GetFurnitureItems;
        HandleItems.passEquipmentShopItemsEvent += GetEquipmentItems;

        ItemButtonHandler.HandleItemQuantity += SetItemQuantity;
    }
    private void OnDisable() {
        HandleItems.passFurnitureShopItemsEvent -= GetFurnitureItems;
        HandleItems.passEquipmentShopItemsEvent -= GetEquipmentItems;

        ItemButtonHandler.HandleItemQuantity -= SetItemQuantity;
    }
    private void GetFurnitureItems(ShopItemSO[] _furnitureShopItemsSO) 
    {
        furnitureShopItemsSO = _furnitureShopItemsSO;
    }
    private void GetEquipmentItems(ShopItemSO[] _equipmentShopItemsSO) 
    {
        equipmentShopItemsSO = _equipmentShopItemsSO;
    }
    private void SetItemQuantity(GameObject obj)
    {
        string columnSelected = obj.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text;
        SetMinusPlus(obj, columnSelected, furnitureShopItemsSO);
        SetMinusPlus(obj, columnSelected, equipmentShopItemsSO);
    }

    private void SetMinusPlus(GameObject obj, string columnSelected, ShopItemSO[] typeShopItemsSO)
    {
        if (obj.GetComponentInChildren<TextMeshProUGUI>().text == "+")
        {
            // Debug.Log(columnSelected);
            for (int i = 0; i < typeShopItemsSO.Length; i++)
            {
                if (typeShopItemsSO[i].name == columnSelected)
                {
                    typeShopItemsSO[i].quantityToBuy = typeShopItemsSO[i].quantityToBuy + 1;
                }
            }
        }
        else if (obj.GetComponentInChildren<TextMeshProUGUI>().text == "-")
        {
            for (int i = 0; i < typeShopItemsSO.Length; i++)
            {

                if (typeShopItemsSO[i].name == columnSelected)
                {
                    if (typeShopItemsSO[i].quantityToBuy == 0)
                    {
                        typeShopItemsSO[i].quantityToBuy = 0;
                    }
                    else
                    {
                        typeShopItemsSO[i].quantityToBuy = typeShopItemsSO[i].quantityToBuy - 1;

                    }
                }
            }
        }
    }
}
