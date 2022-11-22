using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HireButtonHandler : MonoBehaviour
{
    ///////////////////////////////////////////////////
    public GameObject furnitureButton;
    public GameObject equipmentButton;
    public GameObject shopPanel;
    public GameObject furniturePanel;
    //////////////////////////////////////////////////
    public GameObject hirePanel;
    public GameObject equipmentPanel;
    ///////////////////////////////////////////////////
    public GameObject closeShopButton;
    ///////////////////////////////////////////////////
    public GameObject sellButton;
    ///////////////////////////////////////////////////
    public static event Action<string> HirePressEvent;
    ///////////////////////////////////////////////////
    void Start()
    {
        // furnitureButton.SetActive(false);
        // equipmentButton.SetActive(false);
        // shopPanel.SetActive(false);
    }

    private void Update() {
        
        
    }
    public void HireButtonOnClick()
    {
        
        ///////////////////////////////////////////////////
        furnitureButton.SetActive(false);
        equipmentButton.SetActive(false);
        ///////////////////////////////////////////////////
        shopPanel.SetActive(true);
        ///////////////////////////////////////////////////
        furniturePanel.SetActive(false);
        hirePanel.SetActive(true);
        equipmentPanel.SetActive(false);
        ///////////////////////////////////////////////////
        closeShopButton.SetActive(true);
        ///////////////////////////////////////////////////
        sellButton.GetComponent<Button>().enabled = false;
        sellButton.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, 0.5f);
        ///////////////////////////////////////////////////

        string typeButton = "HIRE_BUTTON";
        HirePressEvent?.Invoke(typeButton);
    }
}
