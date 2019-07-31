using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class ObiInHand : MonoBehaviour
{
    private GameObject obiTape;
    private GameObject rightHandler;
    private bool tapeAttachLeftHand = false;
    private bool tapeAttachBothHands = false;

    // Use this for initialization
    void Start()
    {
        obiTape = GameObject.Find("TapeController");
        obiTape.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        // check if show scissor and not setup and cut
        if (StaticData.getIsCut() && StaticData.getShowScissor() && !tapeAttachLeftHand)
        {
            obiTape.SetActive(true);
            Transform cloth = GameObject.Find("/TapeController/clothPart").transform;
            obiTape.transform.position = this.transform.position - new Vector3(0.0f, cloth.lossyScale.y/2, 0.0f);
            Debug.Log("scale: " + obiTape.transform.lossyScale.y);
            
            // set up position
            GameObject leftHandler = GameObject.Find("/TapeController/clothPart/left_hand");
            leftHandler.transform.SetParent(this.transform);

            Debug.Log(this.name);
            leftHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            leftHandler.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

            tapeAttachLeftHand = true;
        }


        if (tapeAttachLeftHand && !tapeAttachBothHands)
        {

            // let tape moves followed left hand
            GameObject leftController = VRTK_DeviceFinder.GetControllerLeftHand();
            Transform cloth = GameObject.Find("/TapeController/clothPart").transform;
            obiTape.transform.position = this.transform.position - new Vector3(0.0f, cloth.localScale.y/2, 0.0f);

            GameObject rightController = VRTK_DeviceFinder.GetControllerRightHand();
            GameObject rightHandler = GameObject.Find("/TapeController/clothPart/right_hand");
            
            // touch and grab the right side of the tape
            if (rightHandler != null && rightHandler.GetComponent<VRTK_InteractableObject>().IsUsing())
            {
                rightHandler.transform.SetParent(GameObject.Find("RightController").transform);
                rightHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                rightHandler.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
                tapeAttachBothHands = true;
            }
        }
    }
}
