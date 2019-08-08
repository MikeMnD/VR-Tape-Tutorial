using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class ObiInHand : MonoBehaviour
{
    private GameObject obiTape;
    private GameObject rightHandler;

    // Use this for initialization
    void Start()
    {
        obiTape = GameObject.Find("TapeController");
        obiTape.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        // check if cut tape is not attached to the left hand
        if (StaticData.getIsCut() && StaticData.getShowScissor() && !StaticData.isTapeAttachLeftHand())
        {
            obiTape.SetActive(true);
            Transform cloth = GameObject.Find("/TapeController/clothPart").transform;
            obiTape.transform.position = this.transform.position - new Vector3(0.0f, cloth.lossyScale.y/2, 0.0f);
            // Debug.Log("scale: " + obiTape.transform.lossyScale.y);
            
            // set up position
            GameObject leftHandler = GameObject.Find("/TapeController/clothPart/left_hand");

            GameObject leftController = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/LeftController");
            leftHandler.transform.SetParent(leftController.transform);
            leftHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.1f);
            leftHandler.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

            // set up left hand animation
            setHandHolded("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/LeftController/VRTK_BasicHand/LeftHand");
        
            StaticData.setTapeAttachLeftHand(true);
        }


        if(StaticData.isTapeAttachLeftHand() && !StaticData.isTapeAttachBothHands())
        {

            // let tape moves followed left hand
            GameObject leftController = VRTK_DeviceFinder.GetControllerLeftHand();
            Transform cloth = GameObject.Find("/TapeController/clothPart").transform;
            obiTape.transform.position = this.transform.position - new Vector3(0.0f, cloth.lossyScale.y* 0.3f, 0.0f);

            GameObject rightController = VRTK_DeviceFinder.GetControllerRightHand();
            GameObject rightHandler = GameObject.Find("/TapeController/clothPart/right_hand");
            
            // touch and grab the right side of the tape
            if (rightHandler != null && rightHandler.GetComponent<VRTK_InteractableObject>().IsUsing())
            {
                rightHandler.transform.SetParent(GameObject.Find("RightController").transform);
                rightHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.1f);
                rightHandler.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

                // hide the rectangular prism
                rightHandler.GetComponent<MeshRenderer>().enabled = false;

                // set up right hand animation
                setHandHolded("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (right)/RightController/VRTK_BasicHand/RightHand");
               

                StaticData.setTapeAttachBothHands(true);
            }
        }
    }

    public void setHandHolded(string path) {
        Animator animator = GameObject.Find(path).GetComponent<Animator>();
        if(!animator.GetBool("grabObiCloth")){
            Debug.Log("set right hand");
            animator.SetBool("grabObiCloth", true);
        }
    }
}