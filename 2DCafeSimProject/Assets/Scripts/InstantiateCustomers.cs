using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InstantiateCustomers : MonoBehaviour
{
    [SerializeField] public GameObject entityToSpawn;
    public Tilemap map;
    public float timeRemaining = 0;
    private bool incomingCustomers = true;
    private int amountOfCustomers = 0;
    public int indexQueue = -1;
    void Update()
    {
        timeRemaining = timeRemaining + 1;
        if (timeRemaining == 20)
        {
            // if (incomingCustomers == true)
            // {
                amountOfCustomers = amountOfCustomers + 1;
                SpawnCustomers();
            // }
            // if (amountOfCustomers >= 13)
            // {
                // incomingCustomers = false;
            // }

        } else if (timeRemaining == 2000)
        {
            timeRemaining = 0;
        }

        // if (timeRemaining == 5500)
        // {
            // timeRemaining = 0;
        // }
    }
    void SpawnCustomers()
    {
        indexQueue = indexQueue + 1;
        GameObject prefabTransform = Instantiate(entityToSpawn, transform.position, Quaternion.identity);
        prefabTransform.GetComponent<CustomerBehaviour>().map = map;
        prefabTransform.GetComponent<CustomerBehaviour>().vec = map.GetCellCenterWorld(new Vector3Int(7, 17 + indexQueue));
        prefabTransform.GetComponent<CustomerBehaviour>().yIndex = indexQueue;
    }
}

