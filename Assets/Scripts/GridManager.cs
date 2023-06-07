using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int();
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> getGrid
    {
        get { return grid; }
    }
    //below to be deleted
    private List<Vector2Int> gridList = new List<Vector2Int>();
    public List<Vector2Int> getgridList
    {
        get { return gridList; }
    }
    float gridUnitSize;
    private void Awake()
    {
        Creategrid();
        gridUnitSize = UnityEditor.EditorSnapSettings.move.x;
    }

    private void Creategrid()
    {
       for(int i=0;i<gridSize.x;i++)
        {
            for(int j=0;j<gridSize.y; j++)
            {
            
                Vector2Int coordinates = new Vector2Int(i, j);
                grid.Add(coordinates,new Node(coordinates,true));
                //below line to bve deleted
                gridList.Add(coordinates);
            }
        }
    }

    public Node GetNode(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate))
        {
            return grid[coordinate];
        }
        return null;
    }
    public void blockNode(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate))
        {
            grid[coordinate].isWalkable = false;
        }
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinate)
    {
        Vector3 position = new Vector3();
        position.x = coordinate.x * gridUnitSize;
        position.z = coordinate.y * gridUnitSize;
        return position;
    }
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / gridUnitSize);
        coordinates.y= Mathf.RoundToInt(position.z / gridUnitSize);
        return coordinates;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
