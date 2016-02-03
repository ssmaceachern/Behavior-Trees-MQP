using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitTracker : MonoBehaviour {

    public GameObject goTarget;
    public RectTransform CanvasRect;

    public Vector3 size;

    void Update()
    {
        PositionArrowV1();
        //PositionArrowV2();
    }

    void PositionArrowV1()
    {
        GetComponent<Renderer>().enabled = false;

        Vector3 v3Pos = Camera.main.WorldToViewportPoint(goTarget.transform.position);

        if (v3Pos.z < Camera.main.nearClipPlane)
            return;  // Object is behind the camera

        if (v3Pos.x >= 0.0f && v3Pos.x <= 1.0f && v3Pos.y >= 0.0f && v3Pos.y <= 1.0f)
            return; // Object center is visible

        GetComponent<Renderer>().enabled = true;
        v3Pos.x -= 0.5f;  // Translate to use center of viewport
        v3Pos.y -= 0.5f;
        v3Pos.z = 0;      // I think I can do this rather than do a 
                          //   a full projection onto the plane

        float fAngle = Mathf.Atan2(v3Pos.x, v3Pos.y);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, -fAngle * Mathf.Rad2Deg);

        v3Pos.x = 0.5f * Mathf.Sin(fAngle) + 0.5f;  // Place on ellipse touching 
        v3Pos.y = 0.5f * Mathf.Cos(fAngle) + 0.5f;  //   side of viewport
        v3Pos.z = Camera.main.nearClipPlane + 0.01f;  // Looking from neg to pos Z;
        transform.position = Camera.main.ViewportToWorldPoint(v3Pos);
        transform.LookAt(Camera.main.transform);
    }

    void PositionArrowV2()
    {
        //GetComponent<Renderer>().enabled = false;

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

        screenPos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

        //y = mx + b
        float m = cos / sin;

        Vector3 screenBounds = screenCenter * 0.9f;

        if(cos > 0)
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

        //this.transform.localPosition = screenPos;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = screenPos;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        

        //this.transform.LookAt(Camera.main.transform);

    }

    
}
