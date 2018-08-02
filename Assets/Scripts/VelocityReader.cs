using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityReader : MonoBehaviour {

	SteamVR_Controller.Device controller;
	public Rigidbody rb;
	public Vector3 velocity;
	public float maxVelocityX;
	public float maxVelocityY;
	public float maxVelocityZ;
	public float totalMax;

	void Start () {
		
	}
	

	void Update () {
		
		controller = GetComponent<Valve.VR.InteractionSystem.Hand> ().controller;
		if(controller != null)
		velocity = controller.velocity;
	/*	if (controller != null) {
			if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				controller.TriggerHapticPulse (1800);
				maxVelocityX = 0;
				maxVelocityY = 0;
				maxVelocityZ = 0;
				totalMax = 0;
			}
		}

		

		if (velocity.x > maxVelocityX) {
			maxVelocityX = velocity.x;
		}
		if (velocity.y > maxVelocityY) {
			maxVelocityY = velocity.y;
		}
		if (velocity.z > maxVelocityZ) {
			maxVelocityZ = velocity.z;
		}
		if (maxVelocityX + maxVelocityY + maxVelocityZ > totalMax) {
			totalMax = maxVelocityX + maxVelocityY + maxVelocityZ;
		}*/
	}

	void OnTriggerEnter(Collider other){

		totalMax = Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y) + Mathf.Abs(velocity.z);

		if (totalMax < 3) {
			other.gameObject.GetComponent<Renderer> ().material.color = Color.green;
		}
		if (totalMax > 3 && totalMax < 5.5f) {
			other.gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
		}
		if (totalMax > 5.5f) {
			other.gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
