using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using VRTK;
using Obi;

public class MoveHandler : MonoBehaviour
{
    /*
        private int[] indices = { 114, 109, 108, 107, 100, 95, 94, 93, 86, 81, 80,
                                            79, 72, 67, 66, 65, 58, 53, 52, 51, 44, 39,
                                            38, 37, 30, 25, 24, 23, 14, 9, 7, 6, 8};
        private int[] indicesNeg = { 111, 110, 105, 104, 97, 96, 91, 90, 83, 82, 77,
                                                    76, 69, 68, 63, 62, 55, 54, 49, 48, 41, 40,
                                                    35, 34, 27, 26, 21, 20, 11, 10, 4, 1, 0};
                                                    */
    private int[] leftAttachNode = { 39, 40, 81, 82 };
    private int[] curIndices = { 8, 0 };        // the bottom particle
    private GameObject[] hint;
    private GameObject[] stickPos = new GameObject[4];
    private int cur = 0;                                // record the current hint

    private float timeRemaining = 5.0f;

    private Quaternion restDarboux;
    private Vector3 offset;
    private Vector3 offsetNeg;

    private GameObject leftController;
    private GameObject rightController;     // right vive controller
    private GameObject tapeSection;
    private GameObject leftHandler;
    private GameObject rightHandler;        // the right cube attached the cloth
    private ObiCloth obiCloth;
    private ObiCollider leftCollider;
    private ObiCollider rightCollider;

    // Use this for initialization
    void Start()
    {
        restDarboux = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);       // constant
        offset = new Vector3(0.2f, -1.0f, -0.3f);
        offsetNeg = new Vector3(-0.2f, -1.0f, -0.3f);

        leftController = VRTK_DeviceFinder.GetControllerLeftHand();
        rightController = VRTK_DeviceFinder.GetControllerRightHand();
        leftHandler = GameObject.Find("left_hand");
        rightHandler = GameObject.Find("right_hand");
        tapeSection = GameObject.Find("clothPart");
        obiCloth = tapeSection.GetComponent<ObiCloth>();

        leftCollider = leftHandler.GetComponent<ObiCollider>();
        rightCollider = rightHandler.GetComponent<ObiCollider>();

        hint = GameObject.FindGameObjectsWithTag("Hint");
        Debug.Log(hint.Length);

        stickPos[0] = GameObject.Find("StickPos_1");
        stickPos[1] = GameObject.Find("StickPos_2");
        stickPos[2] = GameObject.Find("StickPos_3");
        stickPos[3] = GameObject.Find("StickPos_4");

        Debug.Log(hint.Length);
    }

    // Update is called once per frame
    void Update()
    {
        // printInfo();

        // after attach to both hands, show the first hint
        if(StaticData.isTapeAttachBothHands() && cur == 0) {

            // TODO: modify to handle different model
            
            // turn on the hint of trail renderer
            GameObject.Find("ecum").transform.GetChild(0).gameObject.SetActive(true);
            
            // turn on step buttons
            hint[cur].GetComponent<MeshRenderer>().enabled = true;
            hint[cur].transform.GetChild(0).gameObject.SetActive(true);
        }

        if (StaticData.isTapeAttachBothHands() && cur < hint.Length)
        {
            VRTK_InteractableObject stickBtn = hint[cur].GetComponent<VRTK_InteractableObject>();

            if (stickBtn != null && stickBtn.IsUsing())
            {

                // fix current position
                GameObject handler = getCurHandler();


                handler.transform.SetParent(stickPos[cur].transform);
                handler.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                if (cur != hint.Length -1 ) {
                    handler.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
                }
                handler.GetComponent<ObiParticleHandle>().enabled = true;


                // move on to next position
                leftController = GameObject.Find("left_hand");
                leftCollider.enabled = true;


                ObiPinConstraints pinConstraints = obiCloth.GetComponent<ObiPinConstraints>();
                ObiPinConstraintBatch pinConstraintBatch = pinConstraints.GetFirstBatch();

                pinConstraints.RemoveFromSolver(null);


                // remove previous position constraints first
                // if (cur == 0)
                // {
                    // do nothing
                    // no constraint at the left hand side
                // }
                if (cur == 1)
                {
                    pinConstraintBatch.RemoveConstraint(3);
                    pinConstraintBatch.RemoveConstraint(2);
                }
                if (cur == 2)
                {
                    pinConstraintBatch.RemoveConstraint(3);
                    pinConstraintBatch.RemoveConstraint(2);
                }
                if (cur == 3)
                {
                    pinConstraintBatch.RemoveConstraint(1);
                    pinConstraintBatch.RemoveConstraint(0);
                }

                // add next position constraint
                if (cur == 0 || cur == 1)
                {
                    pinConstraintBatch.AddConstraint(leftAttachNode[cur * 2], leftCollider, offset, restDarboux, 1);
                    pinConstraintBatch.AddConstraint(leftAttachNode[cur * 2 + 1], leftCollider, offsetNeg, restDarboux, 1);
                }

                pinConstraints.AddToSolver(null);

                // turn off the current hint
                hint[cur].GetComponent<MeshRenderer>().enabled = false;
                hint[cur].transform.GetChild(0).gameObject.SetActive(false);

                ++cur;

                // turn on the next hint
                if (cur < hint.Length)
                {
                    hint[cur].GetComponent<MeshRenderer>().enabled = true;
                    hint[cur].transform.GetChild(0).gameObject.SetActive(true);
                }

                // Last step: use right hand to attach
                if (cur == hint.Length - 1)
                {
                    // set lefthand gesture unhold
                    setHandUnhold("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (left)/LeftController/VRTK_BasicHand/LeftHand");

                    rightHandler = GameObject.Find("right_hand");
                    rightController = VRTK_DeviceFinder.GetControllerRightHand();
                }

            }
        }
        else if(StaticData.isTapeAttachBothHands() && cur >= hint.Length)
        {  // finish all steps
            
            // set rightHand gesture unhold
            setHandUnhold("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]/Controller (right)/RightController/VRTK_BasicHand/RightHand");

            // count 5 secs to reload main scene
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                Debug.Log("finish taping");
                SceneManager.LoadScene("testMain");
            }
        }
    }

    public void setHandUnhold(string path) {
        Animator animator = GameObject.Find(path).GetComponent<Animator>();
        if(animator.GetBool("grabObiCloth")){
            Debug.Log("reset right hand");
            animator.SetBool("grabObiCloth", false);
        }
    }

    public GameObject getCurHandler()
    {
        return GameObject.Find("Handler" + (cur + 1));
    }

    public void printInfo()
    {
        // get the pin constraint information
        ObiCloth obiCloth = GameObject.Find("clothPart").GetComponent<ObiCloth>();
        ObiPinConstraintBatch ts = obiCloth.PinConstraints.GetFirstBatch();     // only one batch in obi cloth!
        for (int i = 0; i < ts.ConstraintCount; ++i)
        {
            Debug.Log("restDarbouxVectors: " + ts.restDarbouxVectors[i]);        // (0.7,0,0,0.7)
            Debug.Log("offset: " + ts.pinOffsets[i]);
            Debug.Log("index :" + ts.pinIndices[i]);
        }
    }

}