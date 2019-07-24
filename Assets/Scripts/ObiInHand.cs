using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class ObiInHand : MonoBehaviour
{
    public GameObject obiTape;
    private GameObject rightHandler;
    private bool tapeAttachLeftHand = false;
    private bool tapeAttachBothHands = false;

    // Use this for initialization
    void Start()
    {
        obiTape.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        // check if show scissor and not setup and cut
        if (StaticData.getIsCut() && StaticData.getShowScissor() && !tapeAttachLeftHand)
        {
            obiTape.transform.position = this.transform.position;
            obiTape.SetActive(true);
            
            // set up position
            GameObject leftHandler = GameObject.Find("/TapeController/clothPart/left_hand");
            leftHandler.transform.SetParent(this.transform);
            leftHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            leftHandler.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

            tapeAttachLeftHand = true;
        }


        if (tapeAttachLeftHand && !tapeAttachBothHands)
        {
            GameObject rightController = VRTK_DeviceFinder.GetControllerRightHand();
            GameObject rightHandler = GameObject.Find("/TapeController/clothPart/right_hand");

            // touch and grab the right side of the tape
            // if (rightHandler != null && rightHandler.GetComponent<VRTK_InteractableObject>().IsGrabbed())
            if (rightHandler != null && rightController.GetComponent<VRTK_InteractGrab>().IsGrabButtonPressed())
            {
                rightHandler.transform.SetParent(rightController.transform);
                rightHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                tapeAttachBothHands = true;
            }
        }
    }
}
