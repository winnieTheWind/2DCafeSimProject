using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomerCashRegisterState : CustomerBaseState
{

    public int queueNumber;
    List<PathNode> path;

    public override void EnterState(CustomerStateManager customer)
    {
        CustomerBehaviour customerBehaviour = customer.GetComponent<CustomerBehaviour>();

        GameObject[] firstList = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> finalList = new List<GameObject>();
        string nameToLookFor = "CashRegister(Clone)";

        for (var i = 0; i < firstList.Length; i++)
        {
            if (firstList[i].gameObject.name == nameToLookFor)
            {
                finalList.Add(firstList[i]);
            }
        }

        int selected = Random.Range(0, finalList.Count);

        customerBehaviour.cashRegisterObj = finalList[selected];

        CashRegisterBehaviour cashRegBehaviour = customerBehaviour.cashRegisterObj.GetComponent<CashRegisterBehaviour>();

        if (cashRegBehaviour.path != null)
        {
            cashRegBehaviour.customerQueue.Add(customer.gameObject);

            for (int i = 0; i < cashRegBehaviour.customerQueue.Count; i++)
            {
                customerBehaviour.setTarget(new Vector3(cashRegBehaviour.path[i + 2].x + 0.5f, cashRegBehaviour.path[i + 2].y + 0.5f));
                customerBehaviour.SetAgentPosition();
            }
        }
    }
    public override void UpdateState(CustomerStateManager customer)
    {
    }
}
