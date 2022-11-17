using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class CustomerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 target;
    public NavMeshAgent agent;

    public Vector3Int location;
    public Vector3Int worldLocation;

    public Tilemap map;
    public Tile tile;

    public Vector3 vec;
    public GameObject customer;
    public GameObject cashRegisterObj;


    public int yIndex;
    public int xIndex;

    



    private void OnEnable()
    {
        CashRegisterBehaviour.ActionEvent += ActionCall;
    }

    private void OnDisable()
    {
        CashRegisterBehaviour.ActionEvent -= ActionCall;

    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.Find("CustomerSpawner").transform.position;
    }
    void Update()
    {
    }
    public void setTarget(Vector3 vec)
    {
        target = vec;
    }
    public void SetAgentPosition()
    {
        float offsetY = 0.7f;
        agent.SetDestination(new Vector3(target.x, target.y + offsetY, transform.position.z));

    }

    void ActionCall(GameObject obj)
    {
        // Debug.Log("obj: " + obj);
        cashRegisterObj = obj;
    }


}
