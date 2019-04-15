using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Tape_length_controller : MonoBehaviour {

    public Transform start_point;
    public Transform end_point;
    public LineRenderer lineRederer;
    private GameObject scissor;
    private GameObject tape_roll;

    private bool show_scissor = false;
    private double target_tape_length = StaticData.getTargetTapeLength();
  
    private void BuildTapeSection(float length)
    {
        // get both controllers' positions

        // force to adjust left controller's position

        // set up the new tape section
    }

    
    public float GetCurrentLength()
    {

        /*
        // get the line renderer length
        LineRenderer cut_tape = this.gameObject.GetComponentInChildren<LineRenderer>();
        Vector3[] points = new Vector3[2];
        cut_tape.GetPositions(points);

        // get the length of the tape
        return Vector3.Distance(points[0], points[1]);
        */

        return Vector3.Distance(start_point.position, end_point.position); 

    }

    // Use this for initialization
    void Start () {
        lineRederer.positionCount = 2;      // start and end

        Debug.Log(target_tape_length);
    }
    
    // Update is called once per frame
    void Update () {

        // let the grabbed point be vertical to tape
        start_point.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

        VRTK_InteractableObject tape = GetComponent<VRTK_InteractableObject>();
        if(tape.IsGrabbed())
        {
            Vector3 vector_x = new Vector3(-0.01f, 0.0f, 0.0f);
            Vector3 current_vector = start_point.position - end_point.position;
            float angle = Vector3.Angle(vector_x, current_vector);

            /*
            if(angle > 30.0f)
            {
                // drop the grabbed point
                grabbed_script.ForceStopInteracting();
                start_point.localPosition = end_point.localPosition + vector_x;
                return;
            }
            */
        }

        float dis = GetCurrentLength();
        //Debug.Log("dis: " + dis);

        if (dis >= target_tape_length)
        {

            if (!show_scissor)
            {
                // create the scissors
                show_scissor = true;
                scissor = (GameObject)Resources.Load("scissors");

                tape_roll= GameObject.Find("connect");
                scissor = Instantiate(scissor) as GameObject;

                scissor.transform.SetParent(tape_roll.transform);
                scissor.transform.localPosition = new Vector3( 0.0f, 0.0f, 1.0f);
                scissor.transform.localRotation = Quaternion.Euler(0.0f, -90.0f, -90.0f);

            }

        }
        lineRederer.SetPosition(0, start_point.position);
        lineRederer.SetPosition(1, end_point.position);

    }
}
