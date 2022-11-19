using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;
public class BeaconBehaviour : MonoBehaviour
{

    // public Tilemap map;
    public Tilemap map;
    // Start is called before the first frame update

    public static Action<bool> CollidingWithWallEvent;
    public bool isCollidingWithGround = false;
    public bool isCollidingWithDesk = false;

    public bool isTriggerOn = false;

    public Sprite greenMarker;
    public Sprite redMarker;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // Vector3Int vec = map.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
        // Vector3 vec2 = map.GetCellCenterWorld(vec);

        // transform.position = vec2;

                // CollidingWithWallEvent?.Invoke(isCollidingWithGround);


    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggerOn == true)
        {
            // Debug.Log(other.gameObject.name);
            if (other.gameObject.name == "Ground")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = greenMarker;
                isCollidingWithGround = true;

            }
            else if (other.gameObject.name == "Desk(Clone)")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = redMarker;

                isCollidingWithDesk = true;
            }


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isTriggerOn == true)
        {
            // Debug.Log(other.gameObject.name + " not touching" );
            if (other.gameObject.name == "Ground")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = redMarker;

                isCollidingWithGround = false;
            }
            else if (other.gameObject.name == "Desk(Clone)")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = greenMarker;

                isCollidingWithDesk = false;
            }


        }
    }
}


// Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

// if (obj != null)
// {
// }

