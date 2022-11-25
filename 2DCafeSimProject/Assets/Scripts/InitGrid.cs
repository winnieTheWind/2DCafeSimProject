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
    public static Action<Pathfinding> getPathFinder;
    public static Action<Pathfinding> getPathFinderStartAction;

    public Pathfinding pathFinder;

    public static InitGrid Instance { get; private set; }

    public InitGrid() {
        Instance = this;
    }

    void Start()
    {
        grid = new Grid<PathNode>(37, 30, 1f, Vector3.zero, (Grid<PathNode> global, int x, int y) => new PathNode(global, x, y), font);
        pathFinder = new Pathfinding();
        // getPathFinderAction?.Invoke(pathFinder);
        // getPathFinderStartAction?.Invoke(pathFinder);

    }

    void Update()
    {
        getPathFinder?.Invoke(pathFinder);
        getGridAction?.Invoke(grid);

        
    }
}
