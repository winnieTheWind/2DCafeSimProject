using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HandleShop : MonoBehaviour
{
    private GameObject furnitureSelectionButton;
    private GameObject equipmentSelectionButton;
    private GameObject furnitureScrollView;
    private GameObject equipmentScrollView;
    private GameObject hireScrollView;
    public GameObject selectionTypeText;
    private TextMeshProUGUI selectionTypeTextMesh;

    public static Action HandleItemsEvent;

    void Start()
    {
        selectionTypeTextMesh = selectionTypeText.GetComponent<TextMeshProUGUI>();
            // selectionTypeTextMesh.text = "Furniture";

        
        furnitureSelectionButton = GameObject.Find("Canvas/Panel/FurnitureButton");
        equipmentSelectionButton = GameObject.Find("Canvas/Panel/EquipmentButton");

        furnitureScrollView = GameObject.Find("Canvas/Panel/ShopPanel/FurnitureScrollView");
        equipmentScrollView = GameObject.Find("Canvas/Panel/ShopPanel/EquipmentScrollView");
        hireScrollView = GameObject.Find("Canvas/Panel/ShopPanel/HireScrollView");
        
        furnitureSelectionButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            equipmentScrollView.SetActive(false);
            furnitureScrollView.SetActive(true);
            // selectionTypeTextMesh.text = "Furniture";
        });
        equipmentSelectionButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            equipmentScrollView.SetActive(true);
            furnitureScrollView.SetActive(false);
            // selectionTypeTextMesh.text = "Equipment";
        });

        HandleItemsEvent?.Invoke();
    }
 }
