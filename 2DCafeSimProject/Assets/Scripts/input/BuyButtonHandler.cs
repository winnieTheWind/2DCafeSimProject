using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuyButtonHandler : MonoBehaviour
{
    public GameObject furnitureButton;
    public GameObject equipmentButton;

    public GameObject shopPanel;

    public GameObject furnitureScrollView;
    public GameObject equipmentScrollView;
    public GameObject hireScrollView;

    public GameObject closeShopButton;

    public GameObject sellButton;

    public static event Action<string> BuyPressEvent;

    void Start() {
        // furnitureButton.SetActive(false);
        // equipmentButton.SetActive(false);

        // shopPanel.SetActive(false);

    }

    private void Update() {
                
    }

    private void OnEnable() {
        
    }
    public void BuyButtonOnClick() {
        

        furnitureButton.SetActive(true);
        equipmentButton.SetActive(true);

        shopPanel.SetActive(true);

        furnitureScrollView.SetActive(true);
        equipmentScrollView.SetActive(false);
        hireScrollView.SetActive(false);

        closeShopButton.SetActive(true);

        sellButton.GetComponent<Button>().enabled = false;
        sellButton.GetComponent<Image>().color = new Vector4(1f,1f,1f,0.5f);

        string typeButton = "BUY_BUTTON";
        BuyPressEvent?.Invoke(typeButton);

    }
}
