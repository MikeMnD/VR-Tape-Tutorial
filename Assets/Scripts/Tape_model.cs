using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Tape_model : VRTK_InteractableObject {

    private VRTK_ControllerReference controllerReference;
    public LineRenderer lineRederer;
    public Transform start_point;
    public Transform end_point;

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
    }

    public override void OnInteractableObjectUnused(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectUnused(e);
        Debug.Log("used");
        
        
        
    }
}
