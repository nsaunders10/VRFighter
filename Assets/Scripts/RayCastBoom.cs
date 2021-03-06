﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCastBoom : MonoBehaviour {

	public float radius = 5.0F;
	public float power = 10.0F;
	public Collider mainColl;
	public Collider[] colliders;
	RaycastHit hit;
	public GameObject smokeRing;
	public GameObject punchHit;
	public GameObject trail;
	Klak.Motion.BrownianMotion cameraShake;
	bool isHit;
	public Transform target;

	void Start () {
		cameraShake = GetComponent<Klak.Motion.BrownianMotion> ();
	}

	void Update () {

		//transform.LookAt (target);

		if(Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene ("PunchTest");
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Input.GetMouseButtonDown(0) && !isHit) {
			if (Physics.Raycast (ray, out hit, 1000)) {
				Debug.DrawLine (ray.origin, hit.point); 
				mainColl = hit.collider;
				colliders = Physics.OverlapSphere(hit.point, radius);

				Rigidbody mainRB = mainColl.attachedRigidbody;
				if (mainRB != null) {
					//mainRB.AddExplosionForce (power / 100, hit.point, radius);
					mainRB.useGravity = false;
					mainRB.drag = 10;
					isHit = true;
					Instantiate (punchHit, hit.point, Quaternion.identity);
					cameraShake.enabled = true;
					cameraShake.positionAmplitude = 0;
					Invoke ("Delay", 1.5f);
					Klak.Motion.BrownianMotion objMotion = mainColl.gameObject.AddComponent<Klak.Motion.BrownianMotion> ();

				}

				foreach (Collider col in colliders)
				{
					Rigidbody rb = col.gameObject.GetComponent<Rigidbody> ();

					if (rb != null) {
						//rb.AddExplosionForce (power / 100, hit.point, radius);
						Instantiate (punchHit, hit.point, Quaternion.identity);
					}
				}
			}
		}
		if (isHit) {
			
			Klak.Motion.BrownianMotion objMotion = mainColl.gameObject.GetComponent<Klak.Motion.BrownianMotion> ();
			objMotion.enablePositionNoise = true;
			objMotion.enableRotationNoise = false;
			objMotion.positionAmplitude = cameraShake.positionAmplitude* 4;
			objMotion.positionScale = new Vector3(0.1f, 0.1f, 0.1f);
			objMotion.positionFrequency = 60;
			cameraShake.positionAmplitude += 0.01f;
		}

	}

	void Delay(){

		Rigidbody mainRB = mainColl.attachedRigidbody;
		if (mainRB != null) {
			Klak.Motion.BrownianMotion objMotion = mainColl.gameObject.GetComponent<Klak.Motion.BrownianMotion> ();
			Destroy (objMotion);
			mainRB.useGravity= true;
			mainRB.drag = 0;
			isHit = false;
			cameraShake.enabled = false;
			Instantiate (smokeRing, mainRB.gameObject.transform.position, Quaternion.identity);
			Instantiate (trail, mainRB.gameObject.transform);
			mainRB.AddExplosionForce (power, hit.point, radius);
		}

		foreach (Collider col in colliders)
		{
			
			Rigidbody rb = col.gameObject.GetComponent<Rigidbody> ();

			if (rb != null) {
				rb.AddExplosionForce (power/10, hit.point, radius);
			}
		}

	}
}
