﻿using UnityEngine;
using System.Collections;

public class ParticleFade : MonoBehaviour {

	public int timeTillFade=1000;
	public GameObject followTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.y <= -5) {
			this.gameObject.SetActive(false);
		}

		timeTillFade--;
		if (timeTillFade < 0) {
			this.GetComponent<Rigidbody>().useGravity=true;
		}

		if (followTarget != null) {
			;
		}
	}
}
