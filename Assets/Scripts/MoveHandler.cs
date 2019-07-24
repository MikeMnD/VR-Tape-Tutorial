using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class MoveHandler : MonoBehaviour {
    /*
        private int[] indices = { 114, 109, 108, 107, 100, 95, 94, 93, 86, 81, 80,
                                            79, 72, 67, 66, 65, 58, 53, 52, 51, 44, 39,
                                            38, 37, 30, 25, 24, 23, 14, 9, 7, 6, 8};
        private int[] indicesNeg = { 111, 110, 105, 104, 97, 96, 91, 90, 83, 82, 77,
                                                    76, 69, 68, 63, 62, 55, 54, 49, 48, 41, 40,
                                                    35, 34, 27, 26, 21, 20, 11, 10, 4, 1, 0};
                                                    */
    private int[] leftAttachNode = {39, 40 ,81, 82};
    private int[] curIndices = { 8, 0 };        // the bottom particle
    private GameObject[] hint;
    private int cur = 0;                                // record the current hint

    private Quaternion restDarboux;
    private Vector3 offset;
    private Vector3 offsetNeg;

    private GameObject leftController;
    private GameObject rightController;     // right vive controller
    private GameObject tapeSection;
    private GameObject leftHandler;
    private GameObject rightHandler;        // the right cube attached the cloth
    //private VRTK_InteractableObject interactLeftHandler;    // left handler component
    private VRTK_InteractableObject interactRightHandler;    // right handler component
    private ObiCloth obiCloth;
    private ObiCollider leftCollider;
    private ObiCollider rightCollider;

    // Use this for initialization
    void Start () {
        restDarboux = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);       // constant
        offset = new Vector3(0.2f, -1.0f, -0.3f);
        offsetNeg = new Vector3(-0.2f, -1.0f, -0.3f);

        leftController = VRTK_DeviceFinder.GetControllerLeftHand();
        rightController = VRTK_DeviceFinder.GetControllerRightHand();
        leftHandler = GameObject.Find("left_hand");
        rightHandler = GameObject.Find("right_hand");
        tapeSection = GameObject.Find("clothPart");
        obiCloth = tapeSection.GetComponent<ObiCloth>();
        interactRightHandler = rightHandler.GetComponent<VRTK_InteractableObject>();

        leftCollider = leftHandler.GetComponent<ObiCollider>();
        rightCollider = rightHandler.GetComponent<ObiCollider>();

        hint = GameObject.FindGameObjectsWithTag("Hint");
        Debug.Log(hint.Length);
    }

    // Update is called once per frame
    void Update() {
        // printInfo();

        // TODO!! enable handler to certain position!

        /*
        // get the pin constraint information
        ObiPinConstraintBatch ts = obiCloth.PinConstraints.GetFirstBatch();     // only one batch in obi cloth!
        for (int i = 0; i < ts.ConstraintCount; ++i)
        {
            Debug.Log("restDarbouxVectors: " + ts.restDarbouxVectors[i]);        // (0.7,0,0,0.7)
            Debug.Log("offset: " + ts.pinOffsets[i]);
            Debug.Log("index :" + ts.pinIndices[i]);
        }
        */

        if(cur < hint.Length) { 
            VRTK_InteractableObject stickBtn = hint[cur].GetComponent<VRTK_InteractableObject>();

            if(cur == 3)
                Debug.Log(rightController.GetComponent<VRTK_InteractUse>().GetUsingObject());


            if (stickBtn != null && stickBtn.IsUsing())
            {
                Debug.Log("cur = " + cur);

                GameObject handler = GameObject.Find("Handler1");
                if(cur == 1)
                    handler = GameObject.Find("Handler2");
                if(cur == 2)
                    handler = GameObject.Find("Handler3");
                
                if(cur < 3) {
                    handler.transform.SetParent(hint[cur].transform);
                    handler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                }

                if(cur != 0)
                    handler.GetComponent<ObiParticleHandle>().enabled = true;

                leftController = GameObject.Find("left_hand"); 
                leftCollider.enabled = true;

                ObiPinConstraints pinConstraints = obiCloth.GetComponent<ObiPinConstraints>();
                ObiPinConstraintBatch pinConstraintBatch = pinConstraints.GetFirstBatch();

                pinConstraints.RemoveFromSolver(null);

                if(cur == 1) {
                    pinConstraintBatch.RemoveConstraint(3);
                    pinConstraintBatch.RemoveConstraint(2);
                }

                if(cur < 2) {
                    pinConstraintBatch.AddConstraint(leftAttachNode[cur * 2], leftCollider, offset, restDarboux, 1);
                    pinConstraintBatch.AddConstraint(leftAttachNode[cur * 2 + 1], leftCollider, offsetNeg, restDarboux, 1);
                }


                if(cur == 3) {
                    // rightController = GameObject.Find("right_hand");
                    // VRTK_InteractableObject obj = rightController.GetComponent<VRTK_InteractableObject>();
                    Debug.Log("last step");
                    pinConstraintBatch.RemoveConstraint(1);
                    pinConstraintBatch.RemoveConstraint(0);
                }

                
                pinConstraints.AddToSolver(null);

                // stickBtn.StopUsing();
                // hint[cur].SetActive(false);

                ++cur;
                Debug.Log("next = " + cur);

                hint[cur].GetComponent<MeshRenderer>().enabled = true;
                hint[cur].transform.GetChild(0).gameObject.SetActive(true);

                //Debug.Log(pinConstraintBatch.ConstraintCount);
            }
        }
    }
    

    int getClosestParticle(Vector3[] particlePos)
    {
        int bestIndex = -1;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 curPos = rightHandler.transform.position;
        for(int i = 0; i < particlePos.Length; ++i)
        {
            Vector3 directionToTarget = particlePos[i] - curPos;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestIndex = i;
            }
        }

        return bestIndex;
    }

    public void printInfo() {
        // get the pin constraint information
        ObiCloth obiCloth = GameObject.Find("clothPart").GetComponent<ObiCloth>();
        ObiPinConstraintBatch ts = obiCloth.PinConstraints.GetFirstBatch();     // only one batch in obi cloth!
        for (int i = 0; i < ts.ConstraintCount; ++i)
        {
            Debug.Log("restDarbouxVectors: " + ts.restDarbouxVectors[i]);        // (0.7,0,0,0.7)
            Debug.Log("offset: " + ts.pinOffsets[i]);
            Debug.Log("index :" + ts.pinIndices[i]);
        }
    }

}