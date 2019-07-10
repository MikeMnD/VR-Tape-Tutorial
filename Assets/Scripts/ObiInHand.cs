using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class ObiInHand : MonoBehaviour
{
    private bool setUp = false;

    //ObiCloth obiCloth;

    // Use this for initialization
    void Start()
    {
        //obiCloth = this.GetComponent<ObiCloth>();
        //obiCloth.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        // check if show scissor and not setup and cut
        if (StaticData.getIsCut() && StaticData.getShowScissor() && !setUp)
        {

            // set up length
            //double length = StaticData.getTargetTapeLength();
            //transform.localScale = new Vector3((float)length * 0.1f, 1, 0.006f);

            // set up material
            //Material newMat = Resources.Load("tape", typeof(Material)) as Material;
            //Renderer renderer = GetComponent<Renderer>();
            //renderer.material = newMat;

            // set up position
            GameObject leftController = VRTK_DeviceFinder.GetControllerLeftHand();
            GameObject leftHandler = GameObject.Find("/ClothBothHand/left_hand");
            leftHandler.transform.SetParent(leftController.transform);
            leftHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            GameObject rightController = VRTK_DeviceFinder.GetControllerRightHand();
            GameObject rightHandler = GameObject.Find("/ClothBothHand/right_hand");
            rightHandler.transform.SetParent(rightController.transform);
            rightHandler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);



            //transform.SetParent(leftHand.transform);
            //transform.localPosition = new Vector3(0.3f, 0.1f, 0.0f);

            /*
            GameObject rightHand = VRTK_DeviceFinder.GetControllerRightHand();
            transform.SetParent(rightHand.transform);
            transform.localPosition = new Vector3(0.3f, 0.1f, 0.0f);
            */

            // set up  cloth
            //Cloth _cloth = this.GetComponent<Cloth>();
            //_cloth.enabled = false;
            //_cloth.gameObject.SetActive(false);
            //_cloth.gameObject.SetActive(true);
            //_cloth.enabled = true;

            // set up cloth collider
            /*
            List<string> collList = StaticData.getTargetBodyCollider();
            CapsuleCollider[] ccList = new CapsuleCollider[collList.Count];
            for (int i = 0; i < collList.Count; ++i)
            {
                GameObject capsule = GameObject.Find(collList[i]);
                CapsuleCollider cc = capsule.GetComponent<CapsuleCollider>();
                if (cc != null)
                {
                    ccList[i] = cc;
                }
                else
                {
                    Debug.Log(collList[i]);
                }
            }
            _cloth.capsuleColliders = ccList;

            _cloth.enabled = false;
            _cloth.gameObject.SetActive(false);
            _cloth.gameObject.SetActive(true);
            _cloth.enabled = true;
            */

            // enable skin mesh renderer
            //obiCloth.enabled = true;

            setUp = true;
        }

    }
}
