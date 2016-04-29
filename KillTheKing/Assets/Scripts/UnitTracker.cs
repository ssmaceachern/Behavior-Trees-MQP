using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitTracker : MonoBehaviour {

    public GameObject goTarget;
    public RectTransform CanvasRect;

    bool activated;

    Vector3 CameraSmoothRefV;
    Vector3 TargetPos;

    public Vector3 size;

    float speed = 50f;
    Vector3 movement;

    void Start()
    {
        //this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal * speed, 0.0f, vertical * speed);
        
		if (goTarget.transform == null || goTarget.activeSelf == false) {
			Destroy (this.gameObject);
		} else {
			PositionArrow();
		}

        if(Input.anyKeyDown)
        {
            StopCoroutine("MoveCamera");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        CenterCamera();
    }

    void PositionArrow()
    {

        Vector3 v3Screen = Camera.main.WorldToViewportPoint(goTarget.transform.position);
        if (v3Screen.x > -0.1f && v3Screen.x < 1.1f && v3Screen.y > -0.1f && v3Screen.y < 0.95f)
        {
            GetComponent<Renderer>().enabled = false;
        }

        else
        {
            GetComponent<Renderer>().enabled = true;
            v3Screen.x = Mathf.Clamp(v3Screen.x, 0.05f, 0.95f);
            v3Screen.y = Mathf.Clamp(v3Screen.y, 0.05f, 0.95f);
            v3Screen.z = Mathf.Clamp(v3Screen.z, 0.05f, 0.95f);
            transform.position = Camera.main.ViewportToWorldPoint(v3Screen);
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }

    }

    public void CenterCamera()
    {
        activated = true;
        TargetPos = new Vector3(goTarget.transform.position.x, Camera.main.transform.position.y, goTarget.transform.position.z);
        //Debug.Log("Button clicked");
        StartCoroutine(MoveCamera(Camera.main.transform.position, TargetPos, 1.5f));
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
