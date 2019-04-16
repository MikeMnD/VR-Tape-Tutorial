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
    private Vector3 init_pos;

    private bool show_scissor = false;
    private double target_tape_length = StaticData.getTargetTapeLength();
  
    private void BuildTapeSection(float length)
    {
        // get both controllers' positions

        // force to adjust left controller's position

        // set up the new tape section
    }

    public float getCurrentLength()
    {
        return Vector3.Distance(start_point.position, end_point.position); 
    }

    // Use this for initialization
    void Start () {
        lineRederer.positionCount = 2;      // start and end

        Debug.Log(target_tape_length);
            
        GameObject grabbed = GameObject.Find("grabbedBtn");
        init_pos = grabbed.transform.localPosition;


        // set up scissor
        scissor = (GameObject)Resources.Load("scissors");

        tape_roll = GameObject.Find("tapeRoll");

        scissor = Instantiate(scissor) as GameObject;

        // set up the local position
        scissor.transform.SetParent(tape_roll.transform);
        scissor.transform.localPosition = new Vector3(-2.0f, 4.0f, 0.0f);
        scissor.transform.localRotation = Quaternion.Euler(0.0f, -90.0f, -90.0f);

        scissor.SetActive(false);
    }
    
    // Update is called once per frame
    void Update () {

        Debug.Log(tape_roll.transform.localPosition);

        // let the grabbed point be vertical to tape
        start_point.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

        VRTK_InteractableObject tape = GetComponent<VRTK_InteractableObject>();
        if(tape.IsGrabbed())
        {
            Vector3 vector_x = new Vector3(-0.01f, 0.0f, 0.0f);
            Vector3 current_vector = start_point.position - end_point.position;
            float angle = Vector3.Angle(vector_x, current_vector);

            if(angle > 30.0f)
            {
                /*
                Debug.Log("angle is out of range");
                tape.ForceStopInteracting();

                GameObject grabbed = GameObject.Find("grabbedBtn");
                grabbed.transform.position = transform.TransformPoint(init_pos);

                */
            }
        }

        float curLength = getCurrentLength();
        if (curLength >= target_tape_length)
            scissor.SetActive(true);
        else
            scissor.SetActive(false);

        lineRederer.SetPosition(0, start_point.position);
        lineRederer.SetPosition(1, end_point.position);
    }
}
