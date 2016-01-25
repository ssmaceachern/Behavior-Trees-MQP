using UnityEngine;
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

		if (followTarget != null && timeTillFade >= 0) 
		{
			this.transform.position = new Vector3 (followTarget.transform.position.x, followTarget.transform.position.y + 5.0f, followTarget.transform.position.z);
		} 
		else if (followTarget != null && timeTillFade <= 0) 
		{
			followTarget=null;
			
			Rigidbody myBod = this.GetComponent<Rigidbody> ();
			Vector3 nudgeForce = new Vector3 ();
			nudgeForce.x = (Random.value*300-150);
			nudgeForce.y = 100;
			nudgeForce.z = (Random.value*300-150);
			myBod.AddForce(nudgeForce);
			myBod.angularVelocity=new Vector3 (Random.value*10-5, Random.value*10-5, Random.value*10-5);
		}
	}
}
