using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CloseShopButtonHandler : MonoBehaviour
{
    public GameObject furnitureButton;
    public GameObject equipmentButton;
    public GameObject furniturePanel;

    public GameObject sellButton;

    private void Start() {
        gameObject.SetActive(false);
    }

    public void CloseShopButtonOnClick() {
        furnitureButton.SetActive(false);
        equipmentButton.SetActive(false);
        furniturePanel.SetActive(false);
        gameObject.SetActive(false);

        sellButton.GetComponent<Button>().enabled = true;
        sellButton.GetComponent<Image>().color = new Vector4(1f,1f,1f,1f);

    }
}
