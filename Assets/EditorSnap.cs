using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour {


    [Range(1f,20f)] [SerializeField] float gridSide = 10f;

	// Update is called once per frame
	void Update ()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSide) * gridSide;
        snapPos.y = 0f;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSide) * gridSide; ;

        transform.position = new Vector3(snapPos.x, snapPos.y, snapPos.z);
	}
}
