using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Tape_model : VRTK_InteractableObject {

    private VRTK_ControllerReference controllerReference;
    private LineRenderer duplicate;

    private void SetNewLineRenderer(float length)
    {
        // get both controllers' positions

        // force to adjust left controller's position

        // set up the duplicate of the line renderer
    }


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

    public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);
        Debug.Log("start using");

        // get the line renderer length
        LineRenderer cut_tape = this.gameObject.GetComponentInChildren<LineRenderer>();
        Vector3[] points = new Vector3[2];
        cut_tape.GetPositions(points);

        // get the length of the tape
        float dis = Vector3.Distance(points[0], points[1]);
        Debug.Log("tape length = " + dis);

        // drop the tape in hands
        base.ForceStopInteracting();
        GameObject dropped = GameObject.Find("tape_model");
        Destroy(dropped);
        
    }

}
