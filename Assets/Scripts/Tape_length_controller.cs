using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape_length_controller : MonoBehaviour {

    public Transform[] points;
    public LineRenderer lineRederer;

	// Use this for initialization
	void Start () {
        lineRederer.positionCount = points.Length;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0;i < points.Length; ++i)
        {
            lineRederer.SetPosition(i, points[i].position);
        }
	}
}
