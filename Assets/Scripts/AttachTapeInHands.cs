using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachTapeInHands : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device
    {
        get { return SteamVR_Controller.Input((int)trackedObject.index); }
    }

    private void Awake()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
