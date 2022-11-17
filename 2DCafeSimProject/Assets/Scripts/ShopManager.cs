using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
// using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public ShopItemSO[] furnitureShopItemsSO;
    public ShopItemSO[] equipmentShopItemsSO;

    public ShopTemplate[] furnitureShopPanels;
    public ShopTemplate[] equipmentShopPanels;

    GameObject furnitureSelectionButton;
    GameObject equipmentSelectionButton;
    GameObject furnitureScrollView;
    GameObject equipmentScrollView;

    public GameObject selectionTypeText;
    private TextMeshProUGUI selectionTypeTextMesh;


    // events
    public static event EventHandler<PurchaseItemsEventArgs> SendItemsListEvent;
    private List<ItemsData> purchaseItemlist = new List<ItemsData>();
    public struct ItemsData
    {
        public string name;
        public int quantity;
    }

    public class PurchaseItemsEventArgs : EventArgs
    {
        public List<ItemsData> itemData;
    }



    private void OnEnable()
    {
        ItemButtonHandler.HandleItemQuantity += HandleItemQuantity;
        PurchaseButtonHandler.PurchaseEvent += PurchaseItems;
    }

    private void OnDisable()
    {
        ItemButtonHandler.HandleItemQuantity -= HandleItemQuantity;
        PurchaseButtonHandler.PurchaseEvent -= PurchaseItems;
    }

    private void HandleItemQuantity(GameObject obj)
    {
        string columnSelected = obj.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log(columnSelected);
        SetMinusPlus(obj, columnSelected, furnitureShopItemsSO);
        SetMinusPlus(obj, columnSelected, equipmentShopItemsSO);

    }

    private void SetMinusPlus(GameObject obj, string columnSelected, ShopItemSO[] typeShopItemsSO)
    {
        if (obj.GetComponentInChildren<TextMeshProUGUI>().text == "+")
        {
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

    void Start()
    {
        for (int i = 0; i < furnitureShopItemsSO.Length; i++)
        {
            furnitureShopItemsSO[i].quantityToBuy = 0;
        }

        for (int i = 0; i < equipmentShopItemsSO.Length; i++)
        {
            equipmentShopItemsSO[i].quantityToBuy = 0;
        }

        selectionTypeTextMesh = selectionTypeText.GetComponent<TextMeshProUGUI>();

        furnitureScrollView = GameObject.Find("Canvas/Panel/ShopPanel/FurnitureScrollView");
        equipmentScrollView = GameObject.Find("Canvas/Panel/ShopPanel/EquipmentScrollView");

        furnitureSelectionButton = GameObject.Find("Canvas/Panel/FurnitureButton");
        equipmentSelectionButton = GameObject.Find("Canvas/Panel/EquipmentButton");

        furnitureSelectionButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            equipmentScrollView.SetActive(false);
            furnitureScrollView.SetActive(true);
            selectionTypeTextMesh.text = "Furniture";

            for (int i = 0; i < furnitureShopItemsSO.Length; i++) {
                furnitureShopItemsSO[i].quantityToBuy = 0;
            }

            for (int i = 0; i < equipmentShopItemsSO.Length; i++) {
                equipmentShopItemsSO[i].quantityToBuy = 0;
            }

        });
        equipmentSelectionButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            equipmentScrollView.SetActive(true);
            furnitureScrollView.SetActive(false);
            selectionTypeTextMesh.text = "Equipment";

            for (int i = 0; i < furnitureShopItemsSO.Length; i++) {
                furnitureShopItemsSO[i].quantityToBuy = 0;
            }

            for (int i = 0; i < equipmentShopItemsSO.Length; i++) {
                equipmentShopItemsSO[i].quantityToBuy = 0;
            }
        });
    }

    private void Update()
    {
        LoadPanels();
    }
    public void LoadPanels()
    {
        SetEquipmentPanels();
        SetFurniturePanels();
    }

    private void SetEquipmentPanels()
    {
        for (int i = 0; i < equipmentShopItemsSO.Length; i++)
        {
            equipmentShopPanels[i].tileTxt.text = equipmentShopItemsSO[i].name;
            equipmentShopPanels[i].quantityTxt.text = equipmentShopItemsSO[i].quantityToBuy.ToString();

        }
    }

    private void SetFurniturePanels()
    {
        for (int i = 0; i < furnitureShopItemsSO.Length; i++)
        {
            furnitureShopPanels[i].tileTxt.text = furnitureShopItemsSO[i].name;
            furnitureShopPanels[i].quantityTxt.text = furnitureShopItemsSO[i].quantityToBuy.ToString();
        }
    }

    public void PurchaseItems(object sender, EventArgs args)
    {
        purchaseItemlist.Clear();
        SetItems(furnitureShopItemsSO);
        SetItems(equipmentShopItemsSO);

        SendItemsListEvent?.Invoke(this, new PurchaseItemsEventArgs { itemData = purchaseItemlist });
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
                purchaseItemlist.Sort(SortFunc);
                purchaseItemlist.Reverse();
            }
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
