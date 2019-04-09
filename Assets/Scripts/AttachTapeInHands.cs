using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachTapeInHands : MonoBehaviour {

    public GameObject leftPosition, rightPosition;
    private float init_dist;
    private float init_len;
    private Vector3 init_left_pos, init_right_pos;

    // Use this for initialization
    void Start () {
        init_dist = Vector3.Distance(leftPosition.transform.position, rightPosition.transform.position);
        //Debug.Log(original_dist);  //original distance = 0.4
        init_len = this.transform.localScale.x;
        //Debug.Log(original_len);  //original length = 0.04
        //init_left_pos = leftPosition.transform.position;
        //init_right_pos = rightPosition.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(leftPosition.transform.position, rightPosition.transform.position);
        
        
        //Debug.Log(original_len * (dist / original_dist));
        //Debug.Log(leftPosition.transform.position + "," +rightPosition.transform.position);
        this.transform.localScale = new Vector3(init_len * (dist/ init_dist), 1.0f, 0.01f);
        //Debug.Log(this.transform.position);
    }
}
