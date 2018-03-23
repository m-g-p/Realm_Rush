using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    [SerializeField] bool isRunning = true;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();


    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };


	// Use this for initialization
	void Start ()
    {
        LoadBlocks();
        ColorStartAndEnd();
        Pathfind();

        // ExploreNeighbours();
    }

    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            print("Searching from waypoint: " + searchCenter);
            HaltIfEndFound(searchCenter);
            ExploreNeighbours(searchCenter);
        }

        print("Finnished pathfidning");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if (searchCenter == endWaypoint)
        {
            print("Found end point");
            isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {

        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = from.GetGridPos() + direction;

            try
            {
                QueueNewNeigbours(neighbourCoordinates);
            }
            catch
            {

            }    
                        
        }
    }

    private void QueueNewNeigbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored)
        {

        }
        else
        { 
            neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            print("Queueing" + neighbour);
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOverlapping)
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
                
            }
        }
    }
}
