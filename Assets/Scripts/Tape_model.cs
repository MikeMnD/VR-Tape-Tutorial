using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Tape_model : VRTK_InteractableObject {

    private VRTK_ControllerReference controllerReference;
    //public LineRenderer lineRederer;
    //public Transform start_point;
    //public Transform end_point;

    //GameObject rightHand = VRTK_DeviceFinder.GetControllerRightHand(true);
    //GameObject leftHand = VRTK_DeviceFinder.GetControllerLeftHand(true);

    public override void Grabbed(VRTK_InteractGrab grabbingObject)
    {
        base.Grabbed(grabbingObject);
        controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);
        controllerReference = null;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        controllerReference = null;
        interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
       

        //lineRederer.positionCount = 2;      // start and end
    }


    /*
    public override void OnInteractableObjectUnused(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectUnused(e);
        Debug.Log("used");
        //GameObject leftHand = VRTK_DeviceFinder.GetControllerLeftHand(true);
        //GameObject rightHand = VRTK_DeviceFinder.GetControllerRightHand(true);



        lineRederer.SetPosition(0, start_point.position);
        lineRederer.SetPosition(1, end_point.position);
    }


    public override void StopUsing(VRTK_InteractUse previousUsingObject = null, bool resetUsingObjectState = true)
    {
        base.StopUsing(previousUsingObject, resetUsingObjectState);
        
        //lineRederer.SetPosition(0, start_point.position);
        //lineRederer.SetPosition(1, end_point.position);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(IsUsing())
        {
            lineRederer.SetPosition(0, start_point.position);
            lineRederer.SetPosition(1, end_point.position);
        }
    }
    */

}
