using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class MoveHandler : MonoBehaviour {

    private int[] indices = { 114, 109, 108, 107, 100, 95, 94, 93, 86, 81, 80,
                                        79, 72, 67, 66, 65, 58, 53, 52, 51, 44, 39,
                                        38, 37, 30, 25, 24, 23, 14, 9, 7, 6, 8};
    private int[] indicesNeg = { 111, 110, 105, 104, 97, 96, 91, 90, 83, 82, 77,
                                                76, 69, 68, 63, 62, 55, 54, 49, 48, 41, 40,
                                                35, 34, 27, 26, 21, 20, 11, 10, 4, 1, 0};

    private Quaternion restDarboux;
    private Vector3 offset;
    private Vector3 offsetNeg;

    private GameObject leftController;
    private GameObject rightController;     // right vive controller
    private GameObject tapeSection;
    private GameObject leftHandler;
    private GameObject rightHandler;        // the right cube attached the cloth
    private VRTK_InteractableObject interactRightHandler;    // right handler component
    private ObiCloth obiCloth;

	// Use this for initialization
	void Start () {
        restDarboux = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
        offset = new Vector3(0.2f, 1.0f, 0.0f);
        offsetNeg = new Vector3(-0.2f, 1.0f, 0.0f);

        leftController = VRTK_DeviceFinder.GetControllerLeftHand();
        rightController = VRTK_DeviceFinder.GetControllerRightHand();
        leftHandler = GameObject.Find("clothPart/left_hand");
        rightHandler = GameObject.Find("clothPart/right_hand");
        tapeSection = GameObject.Find("clothPart");
        obiCloth = tapeSection.GetComponent<ObiCloth>();
//        interactLeftHandler = leftHandler.GetComponent<VRTK_InteractableObject>();
        interactRightHandler = rightHandler.GetComponent<VRTK_InteractableObject>();
	}

    // Update is called once per frame
    void Update() {
        
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

        // if handler.isUsing and grab controller and **touch the hint**
        if (interactRightHandler.IsUsing() && interactRightHandler.GetUsingObject().name == rightController.name)
        {
            Debug.Log("using cloth");
            ObiPinConstraints pinConstraints = obiCloth.GetComponent<ObiPinConstraints>();
            ObiPinConstraintBatch pinConstraintBatch = pinConstraints.GetFirstBatch();
            Debug.Log(pinConstraintBatch.ConstraintCount);      // test!!

            pinConstraints.RemoveFromSolver(null);
            // MODIFY CONSTRAINTS HERE!

            // remove the pinConstraintBatch
            //pinConstraintBatch.RemoveConstraint(0);
            //pinConstraintBatch.RemoveConstraint(1);
            pinConstraintBatch.RemoveConstraint(2);
            pinConstraintBatch.RemoveConstraint(3);

            /////////////pinConstraintBatch.pinIndices.Clear();
            Debug.Log(pinConstraintBatch.ConstraintCount);      // test!!

            // TODO what is restDarboux
            //            pinConstraintBatch.AddConstraint(9, rightHandler, offset, restDarboux, 1);
            //            pinConstraintBatch.AddConstraint(10, rightHandler, offset, restDarboux, 1);
            pinConstraintBatch.AddConstraint(53, rightHandler.GetComponent<ObiCollider>(), offset, restDarboux, 1);
            pinConstraintBatch.AddConstraint(54, rightHandler.GetComponent<ObiCollider>(), offsetNeg, restDarboux, 1);

            pinConstraints.AddToSolver(null);
        }
    }
}