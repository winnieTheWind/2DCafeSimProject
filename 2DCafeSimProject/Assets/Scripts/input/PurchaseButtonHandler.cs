using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class PurchaseButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public static event Action PurchaseEvent;
    public static event Action HireEvent;
    private string typeButton;

    public GameObject itemSpawner;

    private ShopItemSO[] furnitureShopItemsSO;
    private ShopItemSO[] equipmentShopItemsSO;


    private void Awake()
    {

    }
    private void OnEnable()
    {
        // itemPlacement.SetActive(false);

        BuyButtonHandler.BuyPressEvent += SetButtonToPurchase;
        HireButtonHandler.HirePressEvent += SetButtonToHire;

        HandleItems.passFurnitureShopItemsEvent += GetFurnitureItems;
        HandleItems.passEquipmentShopItemsEvent += GetEquipmentItems;
    }

    private void Start()
    {

    }

    private void OnDisable()
    {
        BuyButtonHandler.BuyPressEvent -= SetButtonToPurchase;
        HireButtonHandler.HirePressEvent -= SetButtonToHire;



    }

    void GetFurnitureItems(ShopItemSO[] furnitureItems)
    {
        furnitureShopItemsSO = furnitureItems;

    }

    void GetEquipmentItems(ShopItemSO[] equipmentItems)
    {
        equipmentShopItemsSO = equipmentItems;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (typeButton == "BUY_BUTTON")
        {
            // for (int i = 0; i < furnitureShopItemsSO.Length; i++)
            // {
            //     if (furnitureShopItemsSO[i].quantityToBuy == 0)
            //     {
            //         return;
            //     }
            //     else
            //     {
                    itemSpawner.SetActive(true);

                    PurchaseEvent?.Invoke();
            //     }
            // }
            // for (int i = 0; i < equipmentShopItemsSO.Length; i++)
            // {
            //     if (equipmentShopItemsSO[i].quantityToBuy == 0)
            //     {
            //         return;
            //     }
            //     else
            //     {
            //         // itemSpawner.SetActive(true);
            //         PurchaseEvent?.Invoke();
            //     }
            // }


        }
        else if (typeButton == "HIRE_BUTTON")
        {
            HireEvent?.Invoke();

            // itemPlacement.GetComponent<InstantiateEmployees>().enabled = true;
        }
    }

    // Debug.Log();
    private void SetButtonToPurchase(string _typeButton)
    {
        // Debug.Log("purchase button");
        typeButton = _typeButton;
    }

    private void SetButtonToHire(string _typeButton)
    {
        typeButton = _typeButton;
        // Debug.Log("hire button");

    }


}
