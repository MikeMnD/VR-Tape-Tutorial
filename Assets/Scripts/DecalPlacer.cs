using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DecalPlacer : MonoBehaviour {
    [SerializeField]
    private GameObject decal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hitInfo))
            {
                SpawnDecal(hitInfo);
            }

        }
	}

    private void SpawnDecal(RaycastHit hitInfo)
    {
        var d = Instantiate(decal);
        GameObject t = GameObject.Find("BBP_t-pose");
        d.transform.position = hitInfo.point;
        //d.transform.position = t.transform.position;
        d.transform.forward = hitInfo.normal * -1.0f; 
    }
}
