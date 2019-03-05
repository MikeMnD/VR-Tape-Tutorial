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

        // let the grabbed point to face users' eyes
        start_point.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        Debug.Log("start point = " + start_point.position);

        VRTK_InteractableObject grabbed_script = GetComponent<VRTK_InteractableObject>();
        if(grabbed_script.IsGrabbed())
        {
            Vector3 vector_x = new Vector3(-1.0f, 0.0f, 0.0f);
            Vector3 current_vector = start_point.position - end_point.position;
            float angle = Vector3.Angle(vector_x, current_vector);

            if(angle > 30.0f)
            {
                // drop the grabbed point
                Debug.Log(angle);
                grabbed_script.ForceStopInteracting();
                return;
            }

        }

        lineRederer.SetPosition(0, start_point.position);
        lineRederer.SetPosition(1, end_point.position);
    }
}
