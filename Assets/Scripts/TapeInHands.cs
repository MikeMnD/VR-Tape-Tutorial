using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TapeInHands : MonoBehaviour {
    private bool setUp = false;

    SkinnedMeshRenderer mesh;

    // Use this for initialization
    void Start () {
        mesh = this.GetComponent<SkinnedMeshRenderer>();
        mesh.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        // check if show scissor and not setup and cut
        if (StaticData.getIsCut() && StaticData.getShowScissor() && !setUp)
        {

            // set up length
            double length = StaticData.getTargetTapeLength();
            transform.localScale = new Vector3((float)length*0.1f, 1, 0.003f);

            // set up material
            Material newMat = Resources.Load("tape", typeof(Material)) as Material;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material = newMat;

            // set up position
            GameObject leftHand = VRTK_DeviceFinder.GetControllerLeftHand();
            transform.SetParent(leftHand.transform);
            transform.localPosition = new Vector3( 0.0f, 0.0f, 0.0f);

            // set up  cloth
            Cloth _cloth = this.GetComponent<Cloth>();
            _cloth.enabled = false;
            _cloth.gameObject.SetActive(false);
            _cloth.gameObject.SetActive(true);
            _cloth.enabled = true;

            // enable skin mesh renderer
            mesh.enabled = true;

            setUp = true;
        }
      
    }
}
