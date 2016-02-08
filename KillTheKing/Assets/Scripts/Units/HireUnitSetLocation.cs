using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Minds;
using RAIN.BehaviorTrees;
using System.IO;

public class HireUnitSetLocation : MonoBehaviour {

	private bool givePosition = false;

    private static Texture2D mouseCursorTexture;

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
        mouseCursorTexture = Resources.Load("target") as Texture2D;
    }
	
	// Update is called once per frame
	void Update () 
	{
        //Cursor.SetCursor(mouseCursorTexture, Vector2.zero, CursorMode.Auto);

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
				GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<Vector3>("Location", hit.point);
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                givePosition = false;
			}

			EntityRig pEnt = GetComponentInChildren<AIRig>().AI.Body.GetComponentInChildren<EntityRig> ();

			if(pEnt!=null)
			{
                if (pEnt.Entity.GetAspect("Good") != null)
                {
                    pEnt.Entity.GetAspect("Good").IsActive = true;
                }
                if (pEnt.Entity.GetAspect("Stalker") != null)
                {
                    pEnt.Entity.GetAspect("Stalker").IsActive = true;
                }
				//pEnt.Entity.ActivateEntity();
				if(GetComponentInChildren<AIRig>().AI.WorkingMemory.GetItem<string>("UnitType")=="Assassin")
				{
					pEnt.Entity.GetAspect("Assassin").IsActive=true;
				}
			}
		}
	}
	
	void OnSelect(string command)
	{
		if (!givePosition) {
			GetComponentInChildren<AIRig> ().AI.WorkingMemory.SetItem<string> ("Command", command);
			givePosition = true;
		}
	}
}
