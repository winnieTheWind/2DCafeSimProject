using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDepthHandler : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] customers;
    void Start()
    {
        customers = GameObject.FindGameObjectsWithTag("Customer");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < customers.Length; i++)
        {
            // customers[i].transform.position.y = customers[i].GetComponent<SpriteRenderer>().sortingOrder ;
            Vector2 index = customers[i].transform.position;
            int k = (int)(index.y);
            customers[i].GetComponent<SpriteRenderer>().sortingOrder = k;
        }

    }
}
