using UnityEngine;
using System.Collections;

public class SpawnAssassin : MonoBehaviour {

    public GameObject AssassinPrefab;

    private static bool AssassinPlaced;

	// Use this for initialization
	void Start () {
        AssassinPlaced = false;
	}
	
    public void Spawn()
    {
        Debug.Log("Button Pressed");
        if (AssassinPlaced == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000.0f))
                {
                    Instantiate(AssassinPrefab, hit.point, Quaternion.identity);
                    AssassinPlaced = true;
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if (AssassinPlaced == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000.0f))
                {
                    Instantiate(AssassinPrefab, hit.point, Quaternion.identity);
                    AssassinPlaced = true;
                }
            }
        }
    }
}
