using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Tape_model : VRTK_InteractableObject {

    private VRTK_ControllerReference controllerReference;

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


        if (StaticData.getShowScissor())
        {
            Debug.Log("start using");

            // drop the tape in hands
            base.ForceStopInteracting();
            GameObject dropped = GameObject.Find("tape_model");
            Debug.Log(dropped.transform.lossyScale);

            // change right hand gesture
            GameObject rightHand = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (right)/RightController/VRTK_BasicHand");
            
            VRTK_SDKTransformModify controllerTrans = rightHand.GetComponent<VRTK_SDKTransformModify>();
            if(controllerTrans.sdkOverrides != null) {
                // Debug.Log("Modify right hand rotation");
                // rotate right hand
                controllerTrans.enabled = false;
                controllerTrans.sdkOverrides[0].rotation = new Vector3(0.0f, 0.0f, -72.0f);
                controllerTrans.enabled = true;
            }

            // tell the system that the scissor is cut
            StaticData.setIsCut(true);

            Destroy(dropped);
        }

    }

}
