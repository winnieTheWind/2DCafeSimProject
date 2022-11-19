using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyButtonHandler : MonoBehaviour
{
    public GameObject furnitureButton;
    public GameObject equipmentButton;
    public GameObject furniturePanel;
    public GameObject closeShopButton;

    public GameObject sellButton;


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

        sellButton.GetComponent<Button>().enabled = false;
        sellButton.GetComponent<Image>().color = new Vector4(1f,1f,1f,0.5f);

    }
}
