using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    GridManager gridManager;
    public Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    public Vector2Int startcoordinates;
    public Vector2Int destinationcoordinates;
    Node Startnode;
    Node Destinationnode;
    Node Currentnode;
    public List<Node> reached= new List<Node>();
    //queue
    public List<Vector2Int> directions = new List<Vector2Int>() { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
    public Queue<Node> frontier= new Queue<Node>();
    bool isbinarysearch = true;
    public List<Vector2Int> vectorlist;
    public List<Node> newpath;
    public List<Vector2Int> pathList;
    public event Action regeneratepath;
    EnemyMover enemyMover;
    // Start is called before the first frame update
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        enemyMover = FindObjectOfType<EnemyMover>();
        grid = gridManager.getGrid;

        //below line to be deleted
        vectorlist = gridManager.getgridList;

        Startnode = grid[startcoordinates];
        Destinationnode = grid[destinationcoordinates];
    }
    void Start()
    {
        //GetNewPath();

        //Debug.Log(newpath.Count);
    }
    public List<Node> GetNewPath()
    {
        return GetNewPath(startcoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        //very importatn when recalculating path we have to put starting coordinateds as transform .positon of the enemymover
        ResetNodes();
        BreadFirstSearch(coordinates);
        newpath = Buildpath();
        return newpath;
    }

    private void ResetNodes()
    {
       foreach(KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    private void Exploreneighbours()
    {
        List<Node> neighbours = new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourcords = Currentnode.coordinates + direction;
            if (gridManager.getGrid.ContainsKey(neighbourcords))
            {
                Node neighbournode = gridManager.GetNode(neighbourcords);
                neighbours.Add(neighbournode);
            }
        }
        foreach (Node neighbour in neighbours)
        {

            if(!reached.Contains(neighbour) && neighbour.isWalkable)
            {
                reached.Add(neighbour);
                frontier.Enqueue(neighbour);
                neighbour.connectedTo = Currentnode;
            }

        }
    }

    private void BreadFirstSearch(Vector2Int coordinates)
    {
        reached.Clear();
        frontier.Clear();
        startcoordinates = coordinates;
        //Debug.Log("the start coordinates"+startcoordinates);
        Startnode = grid[coordinates];
        Destinationnode = grid[destinationcoordinates];
        //cant keep neigbours here else it itself would get added
        //start node and destination node initaialse here
        reached.Add(Startnode);
        frontier.Enqueue(Startnode);
        while (frontier.Count>0 && isbinarysearch)
        {
            Currentnode = frontier.Dequeue();
            Exploreneighbours();
            if (Startnode==Destinationnode)
            {
                isbinarysearch = false;
            }
        }
    }
    private List<Node> Buildpath()
    {

        
        List<Node> path = new List<Node>();
        Node Currentnode = Destinationnode;
        path.Add(Currentnode);
        pathList.Clear();
        Currentnode.isPath = true;
        pathList.Add(Currentnode.coordinates);

        while (Currentnode.connectedTo!=null)
        {
          
            Currentnode = Currentnode.connectedTo;
            Currentnode.isPath = true;
            path.Add(Currentnode);
            pathList.Add(Currentnode.coordinates);
        }
        path.Reverse();
        pathList.Reverse();
        return path;
    }
    public bool WillBlockPath(Vector2Int coordinates)
    {    
        if(grid.ContainsKey(coordinates))
        {
            bool previoustate = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            newpath = GetNewPath();
            if(newpath.Count<=1)
            {
                //rese the original path it will be same as before because the shortest path remains the same
                GetNewPath();
                return true;
            }

            else
            {
                regeneratepath?.Invoke();
            }


        }
        //trigger the event here
        return false;
    }



}
