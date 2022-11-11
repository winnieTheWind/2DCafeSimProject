using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShopButtonHandler : MonoBehaviour
{
    public GameObject furnitureButton;
    public GameObject equipmentButton;
    public GameObject furniturePanel;

    private void Start() {
        gameObject.SetActive(false);
    }

    public void CloseShopButtonOnClick() {
        furnitureButton.SetActive(false);
        equipmentButton.SetActive(false);
        furniturePanel.SetActive(false);
        gameObject.SetActive(false);
    }
}
