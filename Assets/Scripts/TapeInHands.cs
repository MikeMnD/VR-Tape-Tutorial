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
            transform.localScale = new Vector3((float)length*0.1f, 1, 0.006f);

            // set up material
            Material newMat = Resources.Load("tape", typeof(Material)) as Material;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material = newMat;

            // set up position
            GameObject leftHand = VRTK_DeviceFinder.GetControllerLeftHand();
            transform.SetParent(leftHand.transform);
            transform.localPosition = new Vector3( 0.1f, 0.0f, 0.1f);

            // set up  cloth
            Cloth _cloth = this.GetComponent<Cloth>();
            _cloth.enabled = false;
            _cloth.gameObject.SetActive(false);
            _cloth.gameObject.SetActive(true);
            _cloth.enabled = true;

            // set up cloth collider !! NOT TEST !!
            List<string> collList = StaticData.getTargetBodyCollider();
            CapsuleCollider[] ccList = new CapsuleCollider[collList.Count];
            for(int i = 0;i < collList.Count; ++i)
            {
                GameObject capsule = GameObject.Find(collList[i]);
                CapsuleCollider cc = capsule.GetComponent<CapsuleCollider>();
                if(cc != null)
                {
                    ccList[i] = cc;
                }
            }
            _cloth.capsuleColliders = ccList;


            // enable skin mesh renderer
            mesh.enabled = true;

            setUp = true;
        }
      
    }
}
