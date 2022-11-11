using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGrid : MonoBehaviour
{
    private Grid<PathNode> grid;
    public Font font;

    void Start()
    {
        grid = new Grid<PathNode>(37, 30, 1f, Vector3.zero, (Grid<PathNode> global, int x, int y) => new PathNode(global, x, y), font);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
