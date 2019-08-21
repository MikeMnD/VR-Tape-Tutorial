using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class ShowUserUI : MonoBehaviour {

	private const float signAppearTime = 5.0f;
	private const float blinkInterval = 0.75f;
	
	private GameObject triggerSign;
	private GameObject gripSign;

	private GameObject curSign;
	private float timeRemaining;
	private float blinkRemaing;
	private bool blink;

	private VRTK_ControllerEvents leftEvent;
	private VRTK_ControllerEvents rightEvent;

	// Use this for initialization
	void Start () {
		timeRemaining = signAppearTime;
		blinkRemaing = blinkInterval;

		triggerSign = GameObject.Find("TriggerSign");
		gripSign = GameObject.Find("GripSign");

		leftEvent = GameObject.Find("LeftController").GetComponent<VRTK_ControllerEvents>();
		rightEvent = GameObject.Find("RightController").GetComponent<VRTK_ControllerEvents>();
	}
	
	// Update is called once per frame
	void Update () {

		if(timeRemaining > 0.0f) {
			getCurSign();
		}

		// update remaining time off showing sign and blinking countdown
		updateSignRemaining();
		
	}


	public void getCurSign() {
		
		// check current sign
		if(!StaticData.getShowScissor()) {
			curSign = gripSign;
			turnOnSign(false, true);
		}
		if(!StaticData.getIsCut() && StaticData.getShowScissor()) {
			// next: right hand trigger
			curSign = triggerSign;
			turnOnSign(true, false);
		}
		else if(StaticData.getIsCut() && !StaticData.isTapeAttachBothHands()) {
			// next: right hand grip			
			curSign = gripSign;
			turnOnSign(false, true);
		}
		else if(StaticData.isTapeAttachBothHands()) {
			// next: left hand trigger			
			curSign = triggerSign;
			turnOnSign(true, false);
		}

	}

	public void turnOnSign(bool triggerEnable, bool gripEnable) {
		triggerSign.SetActive(triggerEnable);
		gripSign.SetActive(gripEnable);
	}
	
	public void updateSignRemaining() {
		
		// count 5 secs to close the hint
		if(timeRemaining > 0.0f) {

			timeRemaining -= Time.deltaTime;
			blinkRemaing -= Time.deltaTime;

			if (timeRemaining <= 0)
			{
				curSign.SetActive(false);
				Debug.Log("close hint");
				blink = false;
			}
			else {		// hint is turning on
				if(blinkRemaing <= 0.0f) {
					curSign.GetComponent<RawImage>().enabled = !curSign.GetComponent<RawImage>().enabled;
					blinkRemaing = blinkInterval;
				}
			}
		}
		else {
			// wrong operation -> show hint again
			showSign();
		}
	}

	public void resetTimeSetting() {
		timeRemaining = signAppearTime;
		blinkRemaing = blinkInterval;
		curSign.SetActive(true);
	}

	public void showSign() {
		
		if(curSign.Equals(gripSign)) {
			if(leftEvent.triggerPressed || leftEvent.touchpadTouched || rightEvent.triggerPressed || rightEvent.touchpadTouched) {
				resetTimeSetting();
				Debug.Log("turn on grip hint");
			}
		}
		else if(curSign.Equals(triggerSign)) {
			if(leftEvent.gripPressed || leftEvent.touchpadTouched || rightEvent.gripPressed || rightEvent.touchpadTouched) {
				resetTimeSetting();
				Debug.Log("turn on trigger hint");
			}
		}

	}
}
