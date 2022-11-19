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

}
