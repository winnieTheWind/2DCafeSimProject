using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class PurchaseButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public static event EventHandler PurchaseEvent;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
            PurchaseEvent?.Invoke(this, EventArgs.Empty);
    }
}
