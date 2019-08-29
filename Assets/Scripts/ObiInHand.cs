using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class ObiInHand : MonoBehaviour
{
    private GameObject obiTape;
    private GameObject rightHandler;

    private VRTK_ControllerHighlighter leftHighlighter;
	private VRTK_ControllerHighlighter rightHighlighter;


    // Use this for initialization
    void Start()
    {
        obiTape = GameObject.Find("TapeController");
        obiTape.SetActive(false);

        string leftPath = "[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/Model";
        leftHighlighter = GameObject.Find(leftPath).GetComponent<VRTK_ControllerHighlighter>();

    }

    // Update is called once per frame
    void Update()
    {
        
        // set up left controller highlight
        if (!StaticData.getShowScissor()) {
            string leftPath = "[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/Model";

            // if(leftHighlighter == null) {
			    // leftHighlighter = GameObject.Find(leftPath).GetComponent<VRTK_ControllerHighlighter>();
            // }

            // set up left highlighter
            // leftHighlighter.HighlightElement(SDK_BaseController.ControllerElements.GripLeft, Color.yellow, 0.0f);
            // leftHighlighter.HighlightElement(SDK_BaseController.ControllerElements.GripRight, Color.yellow, 0.0f);            
            // leftHighlighter.highlightGrip = Color.yellow;
            
            // draw grip button to yellow
            StaticData.setActiveBtn(leftPath + "/lgrip", "on");
            StaticData.setActiveBtn(leftPath + "/rgrip", "on");
            // GameObject drawLeftGrip = GameObject.Find(leftPath + "/lgrip");
            // GameObject drawRightGrip = GameObject.Find(leftPath + "/rgrip");

            // if(drawLeftGrip.GetComponent<MeshRenderer>() != null) {
            //     MeshRenderer leftGrip = drawLeftGrip.GetComponent<MeshRenderer>();
            //     Material[] m = leftGrip.materials;
            //     m[0] = Resources.Load("activeBtn") as Material;
            //     leftGrip.materials = m;
            // }

            // if(drawRightGrip.GetComponent<MeshRenderer>() != null) {
            //     MeshRenderer rightGrip = drawRightGrip.GetComponent<MeshRenderer>();
            //     Material[] m = rightGrip.materials;
            //     m[0] = Resources.Load("activeBtn") as Material;
            //     rightGrip.materials = m;
            // }

            VRTK_ObjectAppearance.SetOpacity(GameObject.Find(leftPath), 0.8f);
        }
        if(StaticData.getShowScissor() && !StaticData.isTapeAttachLeftHand()) {
            string leftPath = "[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/Model";
            string rightPath = "[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (right)/Model";

            if(rightHighlighter == null) {
			    rightHighlighter = GameObject.Find(rightPath).GetComponent<VRTK_ControllerHighlighter>();
            }

            // clear left controller hint
            // leftHighlighter.UnhighlightController();

            // leftHighlighter.highlightGrip = Color.clear;

            // clear grip button color
            StaticData.setActiveBtn(leftPath + "/lgrip", "off");
            StaticData.setActiveBtn(leftPath + "/rgrip", "off");

            // GameObject drawLeftGrip = GameObject.Find(leftPath + "/lgrip");
            // GameObject drawRightGrip = GameObject.Find(leftPath + "/rgrip");

            // if(drawLeftGrip.GetComponent<MeshRenderer>() != null) {
            //     MeshRenderer leftGrip = drawLeftGrip.GetComponent<MeshRenderer>();
            //     Material[] m = leftGrip.materials;
            //     m[0] = Resources.Load("Standard") as Material;
            //     leftGrip.materials = m;
            // }

            // if(drawRightGrip.GetComponent<MeshRenderer>() != null) {
            //     MeshRenderer rightGrip = drawRightGrip.GetComponent<MeshRenderer>();
            //     Material[] m = rightGrip.materials;
            //     m[0] = Resources.Load("Standard") as Material;
            //     rightGrip.materials = m;
            // }

            VRTK_ObjectAppearance.SetOpacity(GameObject.Find(leftPath), 1.0f);

            // set up right controller hint
            // rightHighlighter.HighlightElement(SDK_BaseController.ControllerElements.Trigger, Color.yellow, 0.0f);
            
            // rightHighlighter.highlightTrigger = Color.yellow;
            
            // draw right trigger yellow
            StaticData.setActiveBtn(rightPath + "/trigger", "on");

            VRTK_ObjectAppearance.SetOpacity(GameObject.Find(rightPath), 0.8f);
        }


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