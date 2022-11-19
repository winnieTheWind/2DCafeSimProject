using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SellButtonHandler : MonoBehaviour
{
    public GameObject dateTimePanel;
    public GameObject buyButton;
    public GameObject hireButton;
    public GameObject sellButton;

    public GameObject statsMoneyPanel;

    public GameObject furnitureButton;
    public GameObject equipmentButton;
    public GameObject shopPanel;
    public GameObject closeButton;




    private int buttonCount = 0;

    private bool isSelling = false;



    private void Start()
    {
        // panel = GameObject.Find("Canvas/Panel/StatsMoneyPanel");
    }

    private void Update()
    {
        if (buttonCount == 0)
        {
            dateTimePanel.SetActive(true);
            buyButton.SetActive(true);
            hireButton.SetActive(true);
            statsMoneyPanel.SetActive(true);
            // furnitureButton.SetActive(false);
            // equipmentButton.SetActive(false);
            // shopPanel.SetActive(false);
            // closeButton.SetActive(false);

            isSelling = false;
        }
        else if (buttonCount == 1)
        {
            dateTimePanel.SetActive(false);
            buyButton.SetActive(false);
            hireButton.SetActive(false);
            statsMoneyPanel.SetActive(false);
            // furnitureButton.SetActive(false);
            // equipmentButton.SetActive(false);
            // shopPanel.SetActive(false);
            // closeButton.SetActive(false);

            isSelling = true;
        }
        if (buttonCount == 2)
        {
            buttonCount = 0;
        }

        if (isSelling == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);

            if (hit.collider != null)
            {
                // Debug.Log("Target Position: " + hit.collider.gameObject.transform.position + " " + hit.collider.name);

                if (Mouse.current.leftButton.wasPressedThisFrame == true)
                {
                    if (hit.collider.gameObject.name == "Desk(Clone)")
                    {
                        Destroy(hit.collider.gameObject);

                    } else if (hit.collider.gameObject.name == "Chair(Clone)")
                    {
                        Destroy(hit.collider.gameObject);

                    } else if (hit.collider.gameObject.name == "Table(Clone)")
                    {
                        Destroy(hit.collider.gameObject);

                    } else if (hit.collider.gameObject.name == "MetalDesk(Clone)")
                    {
                        Destroy(hit.collider.gameObject);

                    }
                    
                }
            }
        }
    }

    void SellObject() {
        
    }

    public void SellButtonOnClick()
    {
        buttonCount = buttonCount + 1;
        Debug.Log(buttonCount);
    }
}