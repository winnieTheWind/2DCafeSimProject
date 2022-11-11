using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System;

public class DeskBehaviour : MonoBehaviour
{
    public GameObject prefabTransform;
    public GameObject marker;

    public Tilemap map;

    Vector3 lastPos;

    public bool HasSpawned = false;
    public bool HasBeenPlaced = false;

    int time = 0;

    void Update()
    {
        // if (HasSpawned == true)
        // {
        //     if (HasBeenPlaced == false)
        //     {
        //         // has not been placed
        //         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //         Vector3Int vec = map.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
        //         Vector3 vec2 = map.GetCellCenterWorld(vec);

        //         // desk is following pointer
        //         prefabTransform.transform.position = vec2;
        //         lastPos = vec2;

        //         // press mouse button to place desk
        //         if (Mouse.current.leftButton.wasPressedThisFrame)
        //         {
        //             this.HasBeenPlaced = true;
        //         }
        //     }
        //     else
        //     {

        //         // has been placed
        //         prefabTransform.transform.position = lastPos;
        //     }
        // }
    }
}
