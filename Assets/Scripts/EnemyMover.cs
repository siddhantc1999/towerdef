using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public List<Node> Path=new List<Node>();
    public List<Vector2Int> pathVector;
    GameObject pathLane;
    Pathfinder pathfinder;
    GridManager gridManager;
    public Vector3 endPosition;
    public Vector3 whileendPosition;
    public float maintimer;
    // Start is called before the first frame update
    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        FindObjectOfType<Pathfinder>().regeneratepath += RegeneratePath;
    }
    void Start()
    {
        RegeneratePath();
    }
  //create aniotgher methodf for findpath and startcoroutine move
   public void RegeneratePath()
    {
        StopCoroutine(Move());
        FindPath();
        StartCoroutine(Move());
    }
    private void FindPath()
    {
        
        Path.Clear();
        
        pathVector.Clear();
        //thgis has to be called again
        //Debug.Log("in find new path");
        Vector2Int coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        Path = pathfinder.GetNewPath(coordinates);
        foreach(Node path in Path)
        {
            pathVector.Add(path.coordinates);
        }
        //Debug.Log("the coordinates "+Path[1].coordinates);
    }

    IEnumerator Move()
    {
        //path with node
       foreach(Node waypointnode in Path)
        {
            Vector3 startPosition = transform.position;
             endPosition = gridManager.GetPositionFromCoordinates(waypointnode.coordinates);
            //Debug.Log("before while loop");
            //Debug.Log(endPosition);
            //Debug.Log("the endposition" + endPosition);
            //Vector3 endPosition = new Vector3(waypoint.transform.position.x,1f, waypoint.transform.position.z);
            
            float timer = 0;

            while(timer<=1f)
            {
                
      
                transform.position = Vector3.Lerp(startPosition,endPosition,timer);
                //whileendPosition = endPosition;
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("here in log");
    //}
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("here in log");
    }
}
