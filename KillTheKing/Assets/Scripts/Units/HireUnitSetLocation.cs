using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Minds;
using RAIN.BehaviorTrees;
using System.IO;
using System;

public class HireUnitSetLocation : MonoBehaviour {

	private bool givePosition = false;

    private static Texture2D mouseCursorTexture;
    private GameObject moveToLine;
    private GameObject moveToLineIMG;

    LineRenderer line;
    SpriteRenderer flagSprite;

    float lifeTimer;
    float indicatorLifeTime = 2.0f;

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            //Debug.Log("File Found");
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
        }
        return tex;
    } 

    // Use this for initialization
    void Start () {
        lifeTimer = 0f;

        moveToLine = new GameObject();
        moveToLineIMG = new GameObject();

        line = moveToLine.AddComponent<LineRenderer>();
        line.material.color = Color.black;
        line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        line.receiveShadows = false;
        line.SetWidth(.2f, .2f);

        flagSprite = moveToLineIMG.AddComponent<SpriteRenderer>();
        flagSprite.sprite = Resources.Load("Flag", typeof(Sprite)) as Sprite;
        flagSprite.enabled = false;

        line.material = flagSprite.material;

        line.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z));
        line.SetPosition(1, transform.position);

        moveToLine.name = transform.name + " Destination Line Path";
		moveToLineIMG.name = transform.name + "Designation Marker";

        moveToLine.transform.parent = transform;
        moveToLineIMG.transform.parent = transform;
        moveToLineIMG.transform.rotation = Quaternion.Euler(new Vector3(75f, 0, 0));

        mouseCursorTexture = Resources.Load("target") as Texture2D;
    }
	
	// Update is called once per frame
	void Update () 
	{
        //Cursor.SetCursor(mouseCursorTexture, Vector2.zero, CursorMode.Auto);
        fadeIndicator();

        if (givePosition)
        {
            Cursor.SetCursor(mouseCursorTexture, Vector2.zero, CursorMode.Auto);
        }

        if (Input.GetMouseButtonDown(0) && givePosition)
		{
			//.Log ("Clicking");
			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit, 200.0f) != false)
			{
                moveToLineIMG.SetActive(true);
                moveToLine.SetActive(true);

                GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<Vector3>("Location", hit.point);
                GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<bool>("Moving", true);
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

                givePosition = false;
            }
        }

        if (GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<bool>("Moving"))
        {
            Vector3 designatedLocation = GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<Vector3>("Location");

            line.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z));
            line.SetPosition(1, designatedLocation);

            moveToLineIMG.transform.position = Vector3.Lerp(transform.position, designatedLocation, 1f);
            moveToLineIMG.transform.rotation = Quaternion.Euler(new Vector3(75f, 0, 0));

            flagSprite.enabled = true;
            line.enabled = true;

            if (Vector3.Distance(moveToLineIMG.transform.position, transform.position) < 1f ||
            GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<GameObject>("Enemy"))
            {
                line.enabled = false;
                flagSprite.enabled = false;
            }
        }

        //line.SetPosition(0, transform.position);

        if (Input.GetKeyDown("x") && givePosition)
        {
            givePosition = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void fadeIndicator()
    {
        float lerp = Mathf.PingPong(Time.time, indicatorLifeTime) / indicatorLifeTime;

        float alpha = Mathf.Lerp(1.0f, 0.0f, lerp);

        line.material.color = flagSprite.color = new Color(1f, 1f, 1f, alpha);
        if(alpha <= 0.1)
        {
            moveToLineIMG.SetActive(false);
            moveToLine.SetActive(false);
        }
    }

    void OnSelect(string command)
	{
		if (!givePosition) {
			GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("Command", command);
			givePosition = true;

            line.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z));
            line.SetPosition(1, transform.position);
        }
	}

	public bool IsSelected()
	{
		return givePosition;
	}
}
