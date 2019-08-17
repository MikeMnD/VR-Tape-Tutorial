using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDirection : MonoBehaviour {

	private Transform prevTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		prevTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 targetDir = new Vector3(-0.15f, -0.5155f, 1.41285f) - this.transform.position;


		// The step size is equal to speed times frame time.
        // float step = 1 * Time.deltaTime;
        // Vector3 newDir = Vector3.down;

        // Vector3 newDir = Vector3.RotateTowards(prevTransform.position, targetDir, step, 0.0f);
        // Debug.DrawRay(prevTransform.position, transform.position, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(targetDir);
		transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

		// Debug.Log(Vector3.up);
		// Debug.Log(this.transform.position);

		prevTransform = this.transform;		
	}
}
