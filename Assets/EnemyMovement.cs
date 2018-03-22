using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] List<WayPoint> path;

	// Use this for initialization
	void Start () {
        StartCoroutine(FollowPath());
	}

    IEnumerator FollowPath()
    {
        print("Starting patrol...");

        foreach (WayPoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            print("Visting block: " + waypoint.name);
            yield return new WaitForSeconds(1f);
        }

        print("Ending patrol!");
    }

    // Update is called once per frame
    void Update () {

    
	}
}
