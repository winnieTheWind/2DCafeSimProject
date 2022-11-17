using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStateManager : MonoBehaviour
{
    // Start is called before the first frame update

    CustomerBaseState currentState;
    public CustomerArriveState ArriveState = new CustomerArriveState();
    public CustomerCashRegisterState CashRegisterState = new CustomerCashRegisterState();

    void Start()
    {
        currentState = ArriveState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(CustomerBaseState state) 
    {
        currentState = state;
        state.EnterState(this);
    }
}
