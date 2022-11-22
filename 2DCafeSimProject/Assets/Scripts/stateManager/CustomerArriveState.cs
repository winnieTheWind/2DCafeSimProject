using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerArriveState : CustomerBaseState
{
    public override void EnterState(CustomerStateManager customer)
    {
        
    }
    public override void UpdateState(CustomerStateManager customer)
    {
        if (customer.GetComponent<CustomerBehaviour>().cashRegisterObj == null)
        {
            customer.GetComponent<CustomerBehaviour>().setTarget(customer.GetComponent<CustomerBehaviour>().vec);
            customer.GetComponent<CustomerBehaviour>().SetAgentPosition();


        } else {
            customer.SwitchState(customer.CashRegisterState);
        }
    }


}
