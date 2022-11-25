using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class HandleCashRegister : MonoBehaviour
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

    GameObject wallTilemapObject;
    Tilemap tilemap;
    BoundsInt bounds;
    TileBase[] allTiles;

    SpriteRenderer spriteRenderer;

    Pathfinding pathFinder;
    Pathfinding pathFinderStart;


    [SerializeField] private LayerMask layerMask;

    private int rotationSelection = 0;

    private Vector2 directionVector = Vector2.zero;

    Vector2 offsetTransformPosition = Vector2.zero;

    Vector3Int cashRegisterWToC;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = FacingUpCashRegister;

        marker = GameObject.Find("Marker");

        isCashRegisterTouchingDesk = false;
        timerIsRunning = true;




        Debug.Log(pathFinder);
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

    private void RefreshPath()
    {

    }

    void Update()
    {

        SetDisableTiles("Grid/Walls");
        SetDisableTiles("Grid/Background");

        ActionEvent?.Invoke(gameObject);


        if (HasBeenPlaced == true)
        {

            cashRegisterWToC = map.WorldToCell(new Vector2(transform.position.x, transform.position.y));
            path = pathFinder.FindPath(cashRegisterWToC.x, cashRegisterWToC.y, 6, 27);


            if (path != null)
            {

                PathEvent?.Invoke(path);


                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x + 0.5f, path[i].y + 0.5f), new Vector3(path[i + 1].x + 0.5f, path[i + 1].y + 0.5f), Color.green);
                }
            }

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
            else if (rotationSelection == 1)
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
            else if (rotationSelection == 2)
            {
                isFacingDirection = "DOWN";

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
            else if (rotationSelection == 3)
            {
                isFacingDirection = "LEFT";

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
