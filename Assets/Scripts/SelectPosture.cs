using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPosture : MonoBehaviour {
    private Animator anim;
    private int postureNum;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        postureNum = StaticData.getTargetNum();
	}
	
	// Update is called once per frame
	void Update () {
        //anim.SetInteger("bodyPartNum", postureNum);
        //if(postureNum == 0)
        //{

        //}
	}
}
