using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitTrackerSpawner : MonoBehaviour {

    private GameObject CharactersReference;
    IDictionary<string, Sprite> spawnable;

    // Use this for initialization
    void Start () {

        spawnable = new Dictionary<string, Sprite>{
        { "Trapper" , Resources.Load("Icons/ActorSprites/Trapper", typeof(Sprite)) as Sprite},
        { "Archer Tower" , Resources.Load("Icons/ActorSprites/Tower", typeof(Sprite)) as Sprite},
		{ "Chester" , Resources.Load("Icons/ActorSprites/Chester", typeof(Sprite)) as Sprite},
        { "Archer" , Resources.Load("Icons/ActorSprites/Archer", typeof(Sprite)) as Sprite},
        { "Assassin" , Resources.Load("Icons/ActorSprites/Assassin", typeof(Sprite)) as Sprite},
        { "Priest" , Resources.Load("Icons/ActorSprites/Priest", typeof(Sprite)) as Sprite},
        { "Bard" , Resources.Load("Icons/ActorSprites/Bard", typeof(Sprite)) as Sprite},
        { "Thug" , Resources.Load("Icons/ActorSprites/Thug", typeof(Sprite)) as Sprite}
        };

        CharactersReference = GameObject.Find("Characters");

        if (CharactersReference.transform.FindChild("Mercs") != null)
        {
            SearchTransformChildrenAndCreateTrackers(CharactersReference.transform.FindChild("Mercs"));
        }
        else
        {
            SearchTransformChildrenAndCreateTrackers(CharactersReference.transform);
        }
        
	}

    void SearchTransformChildrenAndCreateTrackers(Transform GameObjectList)
    {
        foreach (Transform t in GameObjectList)
        {
            foreach (var entry in spawnable)
            {
                // do something with entry.Value or entry.Key

                if (t.name.StartsWith(entry.Key))
                {
                    GameObject UnitTracker = new GameObject();
                    SpriteRenderer sr = UnitTracker.AddComponent<SpriteRenderer>();
                    UnitTracker ut = UnitTracker.AddComponent<UnitTracker>();
                    CapsuleCollider cc = UnitTracker.AddComponent<CapsuleCollider>();
                    cc.radius = 1f;
                    ut.goTarget = t.gameObject;
                    sr.sprite = entry.Value;
                    UnitTracker.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    UnitTracker.transform.parent = this.transform;
                    UnitTracker.name = entry.Key;
                    break;
                }
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
