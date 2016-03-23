using UnityEngine;
using System.Collections;

using RAIN.Navigation;
using RAIN.Navigation.Waypoints;

[RequireComponent(typeof(WaypointRig))]
public class CreateRoadConnections : MonoBehaviour {

	WaypointSet levelWaypoints;
	public GameObject PathBlock = Resources.Load("PathBlock", typeof(GameObject)) as GameObject;

	private Vector3 A, B;

	public float lineWidth = 3.0f;
	public float lineHeight = 1.0f;

	// Use this for initialization
	void Start () {
		levelWaypoints = transform.GetComponent<WaypointRig>().WaypointSet;
		Debug.Log("Level Waypoints Connections Count: " + levelWaypoints.Connections.Count);

		foreach(WaypointSet.WaypointConnection wc in levelWaypoints.Connections){
			DrawConnection(levelWaypoints.Waypoints[wc.wpOne], levelWaypoints.Waypoints[wc.wpTwo]);
		}
	}

	void DrawConnection(Waypoint wA, Waypoint wB){
		A = wA.Position; B = wB.Position;

		Vector3 C = ((B - A) * 0.5F ) + A; 
		float lengthC = (B - A).magnitude; //C#

		float sineC = ( B.y - A.y ) / lengthC;
		float cosC = (B.x - A.x) / lengthC;

		float angleC = Mathf.Asin( sineC ) * Mathf.Rad2Deg; 
		float angleC2 = Mathf.Acos( cosC ) * Mathf.Rad2Deg;

		if (B.x < A.x) {angleC = 0 - angleC;}
		if (B.z > A.z) {angleC2 = 0 - angleC2;}
		
		Debug.Log( "inputPosA" + A + " : inputPosB" + B + " : posC" + C + " : lengthC " + lengthC + " : sineC " + sineC + " : angleC " + angleC );

        GameObject endPiece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        endPiece.transform.position = new Vector3(wB.Position.x, wB.Position.y + 0.2f, wB.Position.z);
        endPiece.transform.localScale = new Vector3(lineWidth + 0.1f, lineHeight, lineWidth + 0.1f);
        Destroy(endPiece.GetComponent<CapsuleCollider>());

        float c_rad = lineWidth / 2;

        GameObject connection = Instantiate( PathBlock, C, Quaternion.identity ) as GameObject; 
		connection.name = "Road";
		connection.transform.localScale = new Vector3(lengthC + lineWidth/2f - c_rad, lineHeight, lineWidth); 
		connection.transform.rotation = Quaternion.Euler(0, angleC2, angleC);

        endPiece.GetComponent<Renderer>().material = connection.GetComponent<Renderer>().material;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
