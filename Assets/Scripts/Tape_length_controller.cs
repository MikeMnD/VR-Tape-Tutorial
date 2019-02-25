using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Tape_length_controller : MonoBehaviour {

    public Transform start_point;
    public Transform end_point;
    public LineRenderer lineRederer;

    // Use this for initialization
    void Start () {
        lineRederer.positionCount = 2;      // start and end
    }
    

    // Update is called once per frame
    void Update () {
        lineRederer.SetPosition(0, start_point.position);
        lineRederer.SetPosition(1, end_point.position);
    }
}
