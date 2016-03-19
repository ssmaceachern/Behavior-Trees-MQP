using UnityEngine;
using System.Collections;

public class MoveCameraToKing : MonoBehaviour {

    bool activated;

    Transform King;

    Vector3 CameraSmoothRefV;
    Vector3 TargetPos;

	// Use this for initialization
	void Start () {
        activated = false;
        King = GameObject.Find("King").transform;
    }

    public void MoveToKing()
    {
        activated = true;
        TargetPos = new Vector3(King.position.x, Camera.main.transform.position.y, King.position.z);
        //Debug.Log("Button clicked");
        StartCoroutine(MoveCamera(Camera.main.transform.position, TargetPos, 2f));
    }

    IEnumerator MoveCamera(Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            Camera.main.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        Camera.main.transform.position = target;
    }

}
