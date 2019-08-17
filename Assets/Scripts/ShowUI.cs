using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class ShowUI : MonoBehaviour {

	private const float signAppearTime = 5.0f;
	private const float blinkInterval = 0.75f;
	
	private GameObject hintSign;
	private float timeRemaining;
	private float blinkRemaing;
	private bool blink;

	private VRTK_ControllerEvents leftEvent;
	private VRTK_ControllerEvents rightEvent;

	// Use this for initialization
	void Start () {
		timeRemaining = signAppearTime;
		blinkRemaing = blinkInterval;
		hintSign = GameObject.Find("InstructionSign");	// trigger
		leftEvent = GameObject.Find("LeftController").GetComponent<VRTK_ControllerEvents>();
		rightEvent = GameObject.Find("RightController").GetComponent<VRTK_ControllerEvents>();
	}
	
	// Update is called once per frame
	void Update () {

		// count 5 secs to close the hint
		if(timeRemaining > 0.0f) {

			timeRemaining -= Time.deltaTime;
			blinkRemaing -= Time.deltaTime;

			if (timeRemaining <= 0)
			{
				hintSign.SetActive(false);
				Debug.Log("close hint");
				blink = false;
			}
			else {		// hint is turning on

				if(blinkRemaing <= 0.0f) {
					hintSign.GetComponent<RawImage>().enabled = !hintSign.GetComponent<RawImage>().enabled;
					blinkRemaing = blinkInterval;
				}
			}
		}
		else {
			// wrong operation -> show hint again
			if(leftEvent.gripPressed || leftEvent.touchpadTouched || rightEvent.gripPressed || rightEvent.touchpadTouched) {
				timeRemaining = signAppearTime;
				blinkRemaing = blinkInterval;
				hintSign.SetActive(true);
				Debug.Log("turn on hint");
			}
		}
		
	}
}
