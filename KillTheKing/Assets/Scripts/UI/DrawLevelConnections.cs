using UnityEngine;
using System.Collections;

public class DrawLevelConnections : MonoBehaviour {

	LevelMarker[] LevelMarkers;

	// Use this for initialization
	void Start () {
		LevelMarkers = FindObjectsOfType<LevelMarker>();

		foreach(LevelMarker lm in LevelMarkers){


			for(int i = 0; i < lm.Parents.Length; i++){
				GameObject connection = new GameObject();
				LineRenderer line = connection.AddComponent<LineRenderer>();

				line.material = Resources.Load("Unlit") as Material;

				line.SetPosition(0, lm.transform.position);
				line.SetPosition(1, lm.Parents[i].transform.position);
				
				if(lm.Parents[i].isComplete){
					line.material.color = Color.yellow;
				}else{
					line.material.color = Color.gray;
				}
			}
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
