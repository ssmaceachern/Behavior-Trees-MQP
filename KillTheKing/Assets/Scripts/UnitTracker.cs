using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitTracker : MonoBehaviour {

    public GameObject goTarget;
    public RectTransform CanvasRect;

    public Vector3 size;

    float speed = 50f;
    Vector3 movement;

    GameObject targetPointerObj;
    public Sprite targetPointer;

    void Start()
    {
        targetPointerObj = new GameObject();
        targetPointerObj.transform.parent = this.transform;
        targetPointerObj.AddComponent<SpriteRenderer>().sprite = targetPointer;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal * speed, 0.0f, vertical * speed);

        PositionArrowV3();
        //PositionArrowV2();
    }

    void PositionArrowV1()
    {
        //GetComponent<Renderer>().enabled = false;
        GetComponent<Renderer>().enabled = true;

        Vector3 v3Pos = Camera.main.WorldToViewportPoint(goTarget.transform.position - movement);

        if (v3Pos.z < Camera.main.nearClipPlane)
            return;  // Object is behind the camera

        if (v3Pos.x >= 0.0f && v3Pos.x <= 1.0f && v3Pos.y >= 0.0f && v3Pos.y <= 1.0f)
            return; // Object center is visible

        Debug.Log(v3Pos);
        

        v3Pos.x -= 0.5f;  // Translate to use center of viewport
        v3Pos.y -= 0.5f;
        v3Pos.z = 0;      // I think I can do this rather than do a 
                          //   a full projection onto the plane

        float fAngle = Mathf.Atan2(v3Pos.x, v3Pos.y);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, -fAngle * Mathf.Rad2Deg);

        v3Pos.x = 0.4f * Mathf.Sin(fAngle) + 0.5f;  // Place on ellipse touching 
        v3Pos.y = 0.4f * Mathf.Cos(fAngle) + 0.5f;  //   side of viewport
        v3Pos.z = Camera.main.nearClipPlane + 0.01f;  // Looking from neg to pos Z;
        transform.position =  Camera.main.ViewportToWorldPoint(v3Pos) + movement;

        Debug.Log(transform.position);

        transform.LookAt(Camera.main.transform);
    }

    void PositionArrowV2()
    {
        GetComponent<Renderer>().enabled = false;

        Vector3 screenCenter = new Vector3(CanvasRect.sizeDelta.x * 0.5f, 
            CanvasRect.sizeDelta.y * 0.5f);

        //Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0)/2;
        Vector3 screenPos = Camera.main.WorldToViewportPoint(goTarget.transform.position);

        //Makes the origin the center of the screen rather than the bottom left
        screenPos -= screenCenter;

        //Find the angle from the center of the screen
        float angle = Mathf.Atan2(screenPos.y, screenPos.x);

        Debug.Log(angle);

        angle -= 90 * Mathf.Deg2Rad; //Convert to radians

        float cos = Mathf.Cos(angle);
        float sin = -Mathf.Sin(angle);

        screenPos = screenCenter + new Vector3(150 * sin, 150 * cos, 0);

        //y = mx + b
        float m = cos / sin;

        Vector3 screenBounds = screenCenter * 0.9f;


        if (screenPos.z < Camera.main.nearClipPlane)
            return;  // Object is behind the camera

        if (screenPos.x >= 0.0f && screenPos.x <= 1.0f && screenPos.y >= 0.0f && screenPos.y <= 1.0f)
            return; // Object center is visible

        GetComponent<Renderer>().enabled = true;

        if (cos > 0)
        {
            screenPos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
        }
        else
        {
            screenPos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
        }

        if(screenPos.x > screenBounds.x) //Target is out of bounds! It must be on the right
        {
            screenPos = new Vector3(screenBounds.x, screenBounds.x * m, 0);
        }else if(screenPos.x < -screenBounds.x) //Out of bounds on the left side
        {
            screenPos = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);
        }

        screenPos += screenCenter;

        this.transform.position = Camera.main.ViewportToWorldPoint(screenPos);
        //RectTransform rectTransform = GetComponent<RectTransform>();
        //rectTransform.localPosition = screenPos;
        this.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        transform.LookAt(Camera.main.transform);

        //this.transform.LookAt(Camera.main.transform);

    }

    void PositionArrowV3()
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

            //float fAngle = Mathf.Atan2(v3Screen.x, v3Screen.y);
            //fAngle -= 180 * Mathf.Deg2Rad;

            // targetPointerObj.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -fAngle * Mathf.Rad2Deg);

            var dir = goTarget.transform.position - targetPointerObj.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            targetPointerObj.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.up);

            //float fAngle = Mathf.Atan2(v3Screen.x, v3Screen.y);
            //fAngle -= 90 * Mathf.Deg2Rad; //Convert to radians
            //targetPointerObj.transform.rotation = Quaternion.Euler(90.0f, 0.0f, fAngle * Mathf.Rad2Deg);
        }

    }


}
