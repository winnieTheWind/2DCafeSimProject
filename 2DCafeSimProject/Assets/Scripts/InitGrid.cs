using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InitGrid : MonoBehaviour
{
    public Grid<PathNode> grid;
    public Font font;

    public static Action<Grid<PathNode>> getGridAction;
    public static Action<Pathfinding> getPathFinderAction;
    public static Pathfinding pathFinder;

    void Start()
    {
        // SetGrid(37, 30);
        grid = new Grid<PathNode>(37, 30, 1f, Vector3.zero, (Grid<PathNode> global, int x, int y) => new PathNode(global, x, y), font);
        pathFinder = new Pathfinding();
        // GetGrid?.inv

        Debug.Log(pathFinder + " " + "pathfinding in init");
        // InitGrid.getGridAction += GetGrid;

    }

    void Update()
    {
        getGridAction?.Invoke(grid);
        getPathFinderAction?.Invoke(pathFinder);
        
    }

    // void GetGrid(Grid<PathNode> grid) {
    //     Debug.Log("testing" + " " + grid);
    //     // return grid;
    // }
}
