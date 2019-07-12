using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class ObiInHand : MonoBehaviour
{
    public GameObject obiTape;
    private bool setUp = false;
    
    // Use this for initialization
    void Start()
    {
        obiTape.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        // check if show scissor and not setup and cut
        if (StaticData.getIsCut() && StaticData.getShowScissor() && !setUp)
        {
            obiTape.transform.position = this.transform.position;
            obiTape.SetActive(true);
            
            // set up position
            //GameObject leftController = VRTK_DeviceFinder.GetControllerLeftHand();
            GameObject leftHandler = GameObject.Find("/TapeController/clothPart/left_hand");
            //leftHandler.transform.SetParent(leftController.transform);
            leftHandler.transform.SetParent(this.transform);
            leftHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            GameObject rightController = VRTK_DeviceFinder.GetControllerRightHand();
            GameObject rightHandler = GameObject.Find("/TapeController/clothPart/right_hand");
            rightHandler.transform.SetParent(rightController.transform);
            rightHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            setUp = true;
        }

        /*
        else if(setUp  )          // left hand grab one of sides of the tape
        {
            GameObject rightController = VRTK_DeviceFinder.GetControllerRightHand();
            GameObject rightHandler = GameObject.Find("/TapeController/right_hand");
            rightHandler.transform.SetParent(rightController.transform);
            rightHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
        */
    }
}
