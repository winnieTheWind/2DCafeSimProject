using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButtonHandler : MonoBehaviour
{
    public GameObject furnitureButton;
    public GameObject equipmentButton;
    public GameObject furniturePanel;
    public GameObject closeShopButton;

    void Start() {
        furnitureButton.SetActive(false);
        equipmentButton.SetActive(false);
        furniturePanel.SetActive(false);



    }
    public void BuyButtonOnClick() {
        furnitureButton.SetActive(true);
        equipmentButton.SetActive(true);
        furniturePanel.SetActive(true);
        closeShopButton.SetActive(true);


        Debug.Log("clicked on buy button");
    }
}
