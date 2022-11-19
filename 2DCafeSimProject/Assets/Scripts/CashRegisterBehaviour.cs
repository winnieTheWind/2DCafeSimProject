using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class CashRegisterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite FacingUpCashRegister;
    public Sprite FacingRightCashRegister;
    public Sprite FacingDownCashRegister;
    public Sprite FacingLeftCashRegister;
    public GameObject prefabTransform;

    public GameObject marker;

    public bool HasSpawned = false;
    public bool HasBeenPlaced = false;
    public bool timerIsRunning;
    public bool IsQueueLineColliding = false;
    public bool isCashRegisterTouchingDesk;

    public List<PathNode> path;
    public Tilemap map;

    Vector3 lastPos;


    public static Action<GameObject> ActionEvent;

    public static Action<List<PathNode>> PathEvent;

    private Vector3 startPoint;
    private Vector3 endPoint;

    GameObject wallTilemapObject;
    Tilemap tilemap;
    BoundsInt bounds;
    TileBase[] allTiles;

    SpriteRenderer spriteRenderer;

    Pathfinding pathFinder;

    [SerializeField] private LayerMask layerMask;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = FacingUpCashRegister;

        marker = GameObject.Find("Marker");

        isCashRegisterTouchingDesk = false;
        timerIsRunning = true;

        InitGrid.getPathFinderAction += GetPathFinder;

    }

    void GetPathFinder(Pathfinding _pathFinder)
    {
        pathFinder = _pathFinder;

    }

    private void SetDisableTiles(string tileMapLayer)
    {
        wallTilemapObject = GameObject.Find(tileMapLayer);
        tilemap = wallTilemapObject.GetComponent<Tilemap>();

        bounds = tilemap.cellBounds;
        allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    pathFinder.GetNode(x, y).SetIsWalkable(false);
                }
            }
        }
    }

    private void FixedUpdate()
    {

    }

    void Update()
    {




        SetDisableTiles("Grid/Walls");
        SetDisableTiles("Grid/Background");

        Vector3Int cashRegisterWToC = map.WorldToCell(new Vector2(transform.position.x, transform.position.y));
        Vector3Int crOffset = map.WorldToCell(new Vector3(cashRegisterWToC.x, cashRegisterWToC.y - 1, 0));
        Vector3 crOffsetVec = map.GetCellCenterWorld(crOffset);

        Vector3Int crOffset1 = map.WorldToCell(new Vector3(cashRegisterWToC.x, cashRegisterWToC.y + 1, 0));
        Vector3 crOffsetVec1 = map.GetCellCenterWorld(crOffset1);

        Vector3Int crOffset2 = map.WorldToCell(new Vector3(cashRegisterWToC.x, cashRegisterWToC.y + 2, 0));
        Vector3 crOffsetVec2 = map.GetCellCenterWorld(crOffset2);

        Vector3Int crOffset3 = map.WorldToCell(new Vector3(cashRegisterWToC.x, cashRegisterWToC.y + 3, 0));
        Vector3 crOffsetVec3 = map.GetCellCenterWorld(crOffset3);

        Vector3Int crOffset4 = map.WorldToCell(new Vector3(cashRegisterWToC.x, cashRegisterWToC.y + 4, 0));
        Vector3 crOffsetVec4 = map.GetCellCenterWorld(crOffset4);

        path = pathFinder.FindPath(cashRegisterWToC.x, cashRegisterWToC.y, 6, 27);

        if (path != null)
        {


            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].x + 0.5f, path[i].y + 0.5f), new Vector3(path[i + 1].x + 0.5f, path[i + 1].y + 0.5f), Color.green);
            }
        }

        PathEvent?.Invoke(path);

        ActionEvent?.Invoke(gameObject);

        if (HasBeenPlaced == true)
        {
            Vector3Int crPosition = map.WorldToCell(gameObject.transform.position);
            Vector3Int newPos = new Vector3Int(crPosition.x - 1, crPosition.y + 2);
            Vector3Int newPos1 = new Vector3Int(crPosition.x - 1, crPosition.y + 1);
            Vector3Int newPos2 = new Vector3Int(crPosition.x - 1, crPosition.y);
            Vector3Int newPos3 = new Vector3Int(crPosition.x - 1, crPosition.y - 1);
            Vector3Int newPos4 = new Vector3Int(crPosition.x, crPosition.y - 1);
            Vector3Int newPos5 = new Vector3Int(crPosition.x + 1, crPosition.y - 1);
            Vector3Int newPos6 = new Vector3Int(crPosition.x + 1, crPosition.y);
            Vector3Int newPos7 = new Vector3Int(crPosition.x + 1, crPosition.y + 1);
            Vector3Int newPos8 = new Vector3Int(crPosition.x + 1, crPosition.y + 2);

            Vector3Int newPos9 = new Vector3Int(crPosition.x - 1, crPosition.y + 3);
            Vector3Int newPos10 = new Vector3Int(crPosition.x + 1, crPosition.y + 3);

            pathFinder.GetNode(newPos.x, newPos.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos1.x, newPos1.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos2.x, newPos2.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos3.x, newPos3.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos4.x, newPos4.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos5.x, newPos5.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos6.x, newPos6.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos7.x, newPos7.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos8.x, newPos8.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos9.x, newPos9.y).SetIsWalkable(false);
            pathFinder.GetNode(newPos10.x, newPos10.y).SetIsWalkable(false);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up * 4f, 4f, layerMask);
            Debug.DrawRay(transform.position, Vector2.up * 4f, Color.red);
            if (hit.collider != null)
            {
                IsQueueLineColliding = true;

            } else {
                IsQueueLineColliding = false;
            }
        }

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Desk(Clone)")
        {
            isCashRegisterTouchingDesk = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Desk(Clone)")
        {
            isCashRegisterTouchingDesk = false;
            // isCashRegisterTouchingDesk = true;

        }
    }
}
