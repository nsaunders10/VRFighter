using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityReader : MonoBehaviour {

	SteamVR_Controller.Device controller;
	public Rigidbody rb;
	public GameObject sword;
	public Vector3 velocity;
	public float maxVelocityX;
	public float maxVelocityY;
	public float maxVelocityZ;
	public float totalMax;
	public float strength;
	public float percent;

	public TextMesh text;

	void Start () {
		
	}
	

	void Update () {
		
		controller = GetComponent<Valve.VR.InteractionSystem.Hand> ().controller;
		if(controller != null)
		velocity = controller.velocity;

		if (controller != null) {
			if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				controller.TriggerHapticPulse (1800);
				percent = 0;
			}
		}
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

	void OnCollisionEnter(Collision other){

		totalMax = Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y) + Mathf.Abs(velocity.z);
		text.text = ""+percent;

		if (totalMax < 4) {
			other.collider.gameObject.GetComponent<Renderer> ().material.color = Color.green;
			sword.GetComponent<Renderer> ().material.color = Color.clear;
			sword.GetComponent<Collider> ().enabled = false;
			Invoke ("CanHit", 0.5f);
			other.collider.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(0,3,1);
			percent += 2;
		}
		if (totalMax > 4 && totalMax < 9f) {
			other.collider.gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
			sword.GetComponent<Renderer> ().material.color = Color.clear;
			sword.GetComponent<Collider> ().enabled = false;
			Invoke ("CanHit", 0.65f);
			other.collider.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(0,2,6);
			percent += 6;
		}
		if (totalMax > 9f) {
			other.collider.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			sword.GetComponent<Renderer> ().material.color = Color.clear;
			sword.GetComponent<Collider> ().enabled = false;
			Invoke ("CanHit", 0.85f);
			other.collider.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(0,totalMax*(percent/strength),totalMax*(percent/strength));
			percent += 11;
		}
	}

	void CanHit(){
		
		sword.GetComponent<Renderer> ().material.color = Color.white;
		sword.GetComponent<Collider> ().enabled = true;
	}
}
