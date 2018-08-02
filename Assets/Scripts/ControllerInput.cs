using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {

	public SteamVR_Controller.Device controller;
	void Start () {
		
	}

	void Update () {

		controller = GetComponent<Valve.VR.InteractionSystem.Hand> ().controller;

		if (controller != null) {
			if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
				controller.TriggerHapticPulse (1800);
			}
			if (controller.GetPressDown (SteamVR_Controller.ButtonMask.ApplicationMenu)) {
				
			}

		}
			
	}
}
