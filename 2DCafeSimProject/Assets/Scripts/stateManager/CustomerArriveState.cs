using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerArriveState : CustomerBaseState
{

    private List<GameObject> finalList;
    private GameObject cashRegisterObject;

    bool isAvailable = false;


    public override void EnterState(CustomerStateManager customer)
    {
        customer.GetComponent<CustomerBehaviour>().setTarget(customer.GetComponent<CustomerBehaviour>().vec);
        customer.GetComponent<CustomerBehaviour>().SetAgentPosition();

        CashRegisterBehaviour.isAvailableEvent += GetIsAvailable;
    }

    public override void UpdateState(CustomerStateManager customer)
    {
        if (isAvailable)
        {
            CashRegisterBehaviour.isAvailableEvent -= GetIsAvailable;
            customer.SwitchState(customer.CashRegisterState);
        }
    }

    void GetIsAvailable(bool _isAvailable)
    {
        isAvailable = _isAvailable;

    }
}
