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

    [SerializeField] private GameObject rayPoint;

    public GameObject marker;

    public bool HasSpawned = false;
    public bool HasBeenPlaced = false;
    public bool timerIsRunning;
    public bool IsQueueLineColliding = false;
    public bool isCashRegisterTouchingDesk;

    public string isFacingDirection;


    public List<PathNode> path;
    public Tilemap map;

    Vector3 lastPos;

    // public int queueLength = 0;
    public List<GameObject> customerQueue;
    // customerQueue
    // public int customerQueue = 0;
    // public int queueOfObjectsY = 1;

    public static Action<GameObject> ActionEvent;

    public static Action<List<PathNode>> PathEvent;

    private Vector3 startPoint;
    private Vector3 endPoint;


    public SpriteRenderer spriteRenderer;

    public Pathfinding pathFinder;

    [SerializeField] public LayerMask layerMask;

    public int rotationSelection = 0;

    public Vector2 directionVector = Vector2.zero;

    public Vector2 offsetTransformPosition = Vector2.zero;

    Vector3Int cashRegisterWToC;

    public GameObject queueNode;

    public static Action<bool> isAvailableEvent;




    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = FacingUpCashRegister;

        marker = GameObject.Find("Marker");

        isCashRegisterTouchingDesk = false;
        timerIsRunning = true;

        // pathFinder = new Pathfinding();

        // Debug.Log(pathFinder);
        // cashRegisterWToC = map.WorldToCell(new Vector2(transform.position.x, transform.position.y));

        // path = pathFinder.FindPath(cashRegisterWToC.x, cashRegisterWToC.y, 6, 27);

        InitGrid.getPathFinder += GetPathFinder;

    }


    private void GetPathFinder(Pathfinding _pathFinder)
    {
        pathFinder = _pathFinder;
    }

    private void SetDisableTiles(string tileMapLayer)
    {
        GameObject wallTilemapObject;
        Tilemap tilemap;
        BoundsInt bounds;
        TileBase[] allTiles;

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

    // private void SetDisableTiles2(string tileMapLayer)
    // {
    //     wallTilemapObject = GameObject.Find(tileMapLayer);
    //     tilemap = wallTilemapObject.GetComponent<Tilemap>();

    //     bounds = tilemap.cellBounds;
    //     allTiles = tilemap.GetTilesBlock(bounds);

    //     for (int x = 0; x < bounds.size.x; x++)
    //     {
    //         for (int y = 0; y < bounds.size.y; y++)
    //         {
    //             TileBase tile = allTiles[x + y * bounds.size.x];
    //             if (tile != null)
    //             {
    //                 pathFinder.GetNode(x, y).SetIsWalkable(false);
    //             }
    //         }
    //     }
    // }

    void Update()
    {



        ActionEvent?.Invoke(gameObject);


        if (HasBeenPlaced == true)
        {
            isAvailableEvent?.Invoke(true);

            // pathFinder.GetNode
            // PathNode pathNode = pathFinder.GetNode(newPos.x, newPos.y);
            // pathNode.SetIsWalkable(false);

            SetDisableTiles("Grid/Walls");
            SetDisableTiles("Grid/Background");


            if (rotationSelection == 0)
            {
                isFacingDirection = "UP";

                SetQueueColliders(-1, +2);
                SetQueueColliders(-1, +1);
                SetQueueColliders(-1, 0);
                SetQueueColliders(-1, -1);
                SetQueueColliders(0, -1);
                SetQueueColliders(+1, -1);
                SetQueueColliders(+1, 0);
                SetQueueColliders(+1, +1);
                SetQueueColliders(+1, +2);
            }

            if (rotationSelection == 1)
            {
                isFacingDirection = "RIGHT";
                SetQueueColliders(2, 1);
                SetQueueColliders(1, 1);
                SetQueueColliders(0, 1);
                SetQueueColliders(-1, 1);
                SetQueueColliders(-1, 0);
                SetQueueColliders(-1, -1);
                SetQueueColliders(0, -1);
                SetQueueColliders(1, -1);
                SetQueueColliders(2, -1);
            }


            if (rotationSelection == 2)
            {
                // isFacingDirection = "DOWN";
                SetQueueColliders(1, -2);
                SetQueueColliders(1, -1);
                SetQueueColliders(1, 0);
                SetQueueColliders(1, 1);
                SetQueueColliders(0, 1);
                SetQueueColliders(-1, 1);
                SetQueueColliders(-1, 0);
                SetQueueColliders(-1, -1);
                SetQueueColliders(-1, -2);
            }
            if (rotationSelection == 3)
            {
                // isFacingDirection = "LEFT";
                SetQueueColliders(-2, 1);
                SetQueueColliders(-1, 1);
                SetQueueColliders(0, 1);
                SetQueueColliders(1, 1);
                SetQueueColliders(1, 0);
                SetQueueColliders(1, -1);
                SetQueueColliders(0, -1);
                SetQueueColliders(-1, -1);
                SetQueueColliders(-2, -1);
            }

            Vector3Int cashRegisterXY = map.WorldToCell(new Vector2(transform.position.x, transform.position.y));
            path = pathFinder.FindPath(cashRegisterXY.x, cashRegisterXY.y, 6, 27);

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    // Instantiate(queueNode, new Vector3(path[i].x + 0.5f, path[i].y + 0.5f), Quaternion.identity);
                    Debug.DrawLine(new Vector3(path[i].x + 0.5f, path[i].y + 0.5f), new Vector3(path[i + 1].x + 0.5f, path[i + 1].y + 0.5f), Color.green);
                }

            }
        }
        else
        {
            if (Keyboard.current.rKey.wasPressedThisFrame == true)
            {
                rotationSelection = rotationSelection + 1;
            }

            if (rotationSelection == 0)
            {
                spriteRenderer.sprite = FacingUpCashRegister;
                directionVector = Vector2.up * 4f;
                offsetTransformPosition = new Vector2(transform.position.x, transform.position.y + 1);
            }
            if (rotationSelection == 1)
            {
                spriteRenderer.sprite = FacingRightCashRegister;
                directionVector = Vector2.right * 4f;

                offsetTransformPosition = new Vector2(transform.position.x + 1, transform.position.y);
            }
            if (rotationSelection == 2)
            {
                spriteRenderer.sprite = FacingDownCashRegister;
                directionVector = Vector2.down * 4f;
                offsetTransformPosition = new Vector2(transform.position.x, transform.position.y - 1);
            }
            if (rotationSelection == 3)
            {
                spriteRenderer.sprite = FacingRightCashRegister;
                directionVector = Vector2.left * 4f;
                offsetTransformPosition = new Vector2(transform.position.x - 1, transform.position.y);
            }

            if (rotationSelection == 4)
            {
                rotationSelection = 0;
            }

            RaycastHit2D hit = Physics2D.Raycast(offsetTransformPosition, directionVector, 4f, layerMask);
            Debug.DrawRay(offsetTransformPosition, directionVector, Color.red);
            if (hit.collider != null)
            {
                IsQueueLineColliding = true;

            }
            else
            {
                IsQueueLineColliding = false;
            }
        }

    }

    private PathNode SetQueueColliders(int offsetX, int offsetY)
    {
        Vector3Int crPosition = map.WorldToCell(gameObject.transform.position);
        Vector3Int newPos = new Vector3Int(crPosition.x + offsetX, crPosition.y + offsetY);
        PathNode pathNode = pathFinder.GetNode(newPos.x, newPos.y);
        pathNode.SetIsWalkable(false);
        return pathNode;
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
        }
    }
}
