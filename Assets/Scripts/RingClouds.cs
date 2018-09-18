using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingClouds : MonoBehaviour {

	Camera mainCamera;
	GameObject target;
	bool look;

	void Start () {
		mainCamera = Camera.main;
		target = mainCamera.GetComponent<RayCastBoom> ().mainColl.gameObject;
		Invoke ("StopLooking", 0.1f);
		Invoke ("Delete", 4);
		look = true;

	}
	

	void Update () {
		if (look) {
			transform.LookAt (target.transform);
		}
		transform.Translate (Vector3.back * Time.deltaTime * 50);
	}
	void StopLooking(){
		look = false;

	}
	void Delete(){

		Destroy (gameObject);
	}
}
