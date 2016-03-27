using UnityEngine;
using System.Collections;

public class BlockArrows : MonoBehaviour {
	// Update is called once per frame
	void Update () 
	{

		this.transform.Rotate(new Vector3(0.0f, 2.0f, 0.0f));

		GameObject[] arrows = GameObject.FindGameObjectsWithTag("Arrow");

		if(GetComponent<ParticleFade>().followTarget.activeSelf)
		{
			for (int i=0; i<arrows.Length; i++) 
			{
				Vector3 realPos=arrows[i].transform.position;
				realPos.y=0;
				if(Vector3.Distance(realPos,this.transform.position)<3)
				{

					GameObject particle = (GameObject)GameObject.Instantiate (Resources.Load ("Bile"));
					particle.transform.position = new Vector3 (arrows[i].transform.position.x, arrows[i].transform.position.y, arrows[i].transform.position.z);
					Rigidbody hisBod = particle.GetComponent<Rigidbody> ();
					Vector3 nudgeForce = new Vector3 ();
					nudgeForce=(arrows[i].transform.position-this.transform.position)*50;
					nudgeForce.x = (Random.value*10-5);
					nudgeForce.y = 300;
					nudgeForce.z = (Random.value*10-5);
					hisBod.AddForce(nudgeForce);

					arrows[i].SetActive(false);
					return;
				}
			}
		}

	}
}
