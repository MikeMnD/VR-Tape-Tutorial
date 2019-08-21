using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class ShowMainUI : MonoBehaviour {

	private const float signAppearTime = 3.0f;
	
	private GameObject hintSign;
	private float timeRemaining;

	private VRTK_ControllerEvents leftEvent;
	private VRTK_ControllerEvents rightEvent;

	// Use this for initialization
	void Start () {
		timeRemaining = signAppearTime;
		hintSign = GameObject.Find("InstructionSign");	// trigger
		leftEvent = GameObject.Find("LeftController").GetComponent<VRTK_ControllerEvents>();
		rightEvent = GameObject.Find("RightController").GetComponent<VRTK_ControllerEvents>();
	}
	
	// Update is called once per frame
	void Update () {

		// count 5 secs to close the hint
		if(timeRemaining > 0.0f) {

			timeRemaining -= Time.deltaTime;

			if (timeRemaining <= 0)
			{
				Debug.Log("close hint");

				// turn off hint
				hintSign.SetActive(false);
			}
		}
		else {
			// wrong operation -> show hint again and play audio
			if(leftEvent.gripPressed || leftEvent.touchpadTouched || rightEvent.gripPressed || rightEvent.touchpadTouched) {
				Debug.Log("turn on hint");
				
				// show hint
				timeRemaining = signAppearTime;
				hintSign.SetActive(true);

				// TODO: play audio
			}
		}
		
	}
}
