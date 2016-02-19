using UnityEngine;
using System.Collections;

// Handles a smoother transition when activating a tool tip
public class ActivateToolTips : MonoBehaviour
{
    public float scaleSpeed = 0.1f;    // The speed at which the tool tip will appear

    private bool activated = false;     // Whether the tool tip is currently activated
    private float xScale = 0.0f;
    private float zScale = 0.0f;

	// Use this for initialization
	void Start ()
    {
        // Start off with a scale of 0 and deactivated
        gameObject.SetActive(false);
        gameObject.transform.localScale = new Vector3(0.0f, 1.0f, 0.0f);

        xScale = gameObject.transform.localScale.x;
        zScale = gameObject.transform.localScale.z;
    }

    // Update is called once per frame
    void Update ()
    {
        Debug.Log(activated);
	    if (activated)
        {
            
            xScale = Mathf.Lerp(xScale, 1.0f, scaleSpeed);
            zScale = Mathf.Lerp(zScale, 1.0f, scaleSpeed);

            // If within a given range, just set to 1.0
            if (xScale >= 0.98f) xScale = 1.0f;
            if (zScale >= 0.98f) zScale = 1.0f;

            gameObject.transform.localScale = new Vector3(xScale, 1.0f, zScale);
        }
        else
        {
            xScale = Mathf.Lerp(xScale, 0.0f, scaleSpeed);
            zScale = Mathf.Lerp(zScale, 0.0f, scaleSpeed);

            gameObject.transform.localScale = new Vector3(xScale, 1.0f, zScale);

            if (xScale <= 0.02f) xScale = 0.0f;
            if (zScale <= 0.02f) zScale = 0.0f;

            if (xScale == 0.0f)
            {
                gameObject.SetActive(false);
            }   
        }
	}

    public void Activate()
    {
        activated = true;
        gameObject.SetActive(true);
    }
    
    public void Deactivate()
    {
        Debug.Log("Deactivating");
        activated = false;
    }
}
