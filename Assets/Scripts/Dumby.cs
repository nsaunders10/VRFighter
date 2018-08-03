using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumby : MonoBehaviour {

	public float speed;
	bool walk = true;

	void Start () {
		
	}
	

	void Update () {
		if(walk)
		transform.Translate (0, 0, -speed * Time.deltaTime);

	}
	void OnCollisionEnter(Collision other){
		if (other.collider.gameObject.tag == "Wall")
		walk = false;

	}
	void OnCollisionExit(Collision other){
		if (other.collider.gameObject.tag == "Wall")
			walk = true;

	}
}
