using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomerCashRegisterState : CustomerBaseState
{

    private CustomerBehaviour customerBehaviour;
    private GameObject cashRegisterObject;

    private CashRegisterBehaviour crBehaviour;

    // int index = 0;
    public override void EnterState(CustomerStateManager customer)
    {
    }
    public override void UpdateState(CustomerStateManager customer)
    {
            customerBehaviour = customer.GetComponent<CustomerBehaviour>();
            cashRegisterObject = customer.GetComponent<CustomerBehaviour>().cashRegisterObj;
            crBehaviour = cashRegisterObject.GetComponent<CashRegisterBehaviour>();

            Vector3Int pathTiles = new Vector3Int(crBehaviour.path[customerBehaviour.yIndex + 2].x, crBehaviour.path[customerBehaviour.yIndex + 2].y, 0);
            // {
            Vector3 tileToWorld = customerBehaviour.map.CellToWorld(pathTiles);

            Vector3 crPosition = tileToWorld;
            Vector3Int crWToC = customerBehaviour.map.WorldToCell(crPosition);

            Vector3Int offsetted = new Vector3Int(crWToC.x, crWToC.y);

            Vector3 centerCashRegisterObj = customerBehaviour.map.GetCellCenterWorld(offsetted);

            customerBehaviour.setTarget(centerCashRegisterObj);
            customerBehaviour.SetAgentPosition();

    }
}
