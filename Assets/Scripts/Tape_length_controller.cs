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
    private double target_tape_length = StaticData.getTargetTapeLength();

    public float getCurrentLength()
    {
        return Vector3.Distance(start_point.position, end_point.position); 
    }

    public string GetGameObjectPath(GameObject obj)
    {
        string path = "/" + obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }

    // Use this for initialization
    void Start () {
        lineRederer.positionCount = 2;      // start and end
        // Debug.Log(target_tape_length);
            
        GameObject grabbed = GameObject.Find("grabbedBtn");
        init_pos = grabbed.transform.localPosition;

        // set up scissor
        scissor = (GameObject)Resources.Load("scissors");
        // scissor = (GameObject)Resources.Load("scissors(Clone)");
        scissor = Instantiate(scissor) as GameObject;

        // set up the local position
        tape_roll = GameObject.Find("tapeRoll");
        scissor.transform.SetParent(tape_roll.transform);
        scissor.transform.localPosition = new Vector3(-2.0f, 4.0f, 0.0f);
        scissor.transform.localRotation = Quaternion.Euler(0.0f, -90.0f, -90.0f);

        scissor.SetActive(false);
    }
    
    // Update is called once per frame
    void Update () {
        GameObject tape_model = GameObject.Find("tape_model");
        if(tape_model.GetComponent<VRTK_InteractableObject>().IsGrabbed()) {
            tape_model.transform.localPosition = new Vector3(0.0f, -0.03535533f, 0.03535534f);
            tape_model.transform.localRotation = Quaternion.Euler(-45.0f, 0.0f, 0.0f);
        }

        // let the grabbed point be vertical to tape
        start_point.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

        VRTK_InteractableObject tape = GetComponent<VRTK_InteractableObject>();
        // if(tape.IsGrabbed())
        if(tape.IsUsing())
        {
            string path = "[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/LeftController/VRTK_BasicHand/GrabAttachPoint";
            // string path = "[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/LeftController/GrabAttachPoint";
            start_point.position = GameObject.Find(path).transform.position;

            Vector3 vector_x = transform.right * -1;
            Vector3 current_vector = start_point.position - end_point.position;
            float angle = Vector3.Angle(vector_x, current_vector);
            if(angle > 30.0f)
            {  
                Debug.Log("angle is out of range");
                tape.ForceStopInteracting();

                GameObject grabbed = GameObject.Find("grabbedBtn");
                grabbed.transform.localPosition = init_pos;
            }
        }

        float curLength = getCurrentLength();
        if (curLength >= target_tape_length)
        {
            scissor.SetActive(true);
            StaticData.setShowScissor(true);
        }
        else
        {
            scissor.SetActive(false);
            StaticData.setShowScissor(false);
        }

        lineRederer.SetPosition(0, start_point.position);
        lineRederer.SetPosition(1, end_point.position);
    }
}