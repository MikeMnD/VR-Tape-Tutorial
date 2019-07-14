using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour {

    void destroyTrail()
    {
        GameObject trail = GameObject.Find("trail(Clone)");
        Destroy(trail);
    }

    void createTrail()
    {
        Object trailObj = Resources.Load("trail");
        Instantiate(trailObj, this.transform);
    }
}
