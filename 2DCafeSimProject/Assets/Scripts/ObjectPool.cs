using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObjectPool instance;
    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 20;
    Tilemap map;
    [SerializeField] private GameObject prefab;
    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }
    void Start()
    {
        map = GameObject.Find("Grid/Ground").GetComponent<Tilemap>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.GetComponent<DeskBehaviour>().map = map;
            obj.GetComponent<DeskBehaviour>().prefabTransform = obj;
            obj.GetComponent<DeskBehaviour>().HasSpawned = true;
            obj.GetComponent<DeskBehaviour>().HasBeenPlaced = false;

            pooledObjects.Add(obj);
        }
        
    }

    public GameObject GetPooledObjects() {


        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
