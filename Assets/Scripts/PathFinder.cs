﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        LoadBlocks();
        ColorStartAndEnd();
        BreadthFirstSearch();
        CreatePath();
        return path;
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();
    } 

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            print("Found end point");
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {

        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;

            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeigbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeigbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //do notting
        }
        else
        { 
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
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