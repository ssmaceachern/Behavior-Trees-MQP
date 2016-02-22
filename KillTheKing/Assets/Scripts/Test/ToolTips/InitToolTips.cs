using UnityEngine;
using System.Collections;

// Initialize the tool tips.
public class InitToolTips : MonoBehaviour
{
    private PieMenu pm;                 // The pie menu of the object we're on. Used to grab icons for actions.
    private SpriteRenderer[] sprites;   // The sprites of the icons representing actions in the pie menu.

	// Set up sprites before we deactivate them in ActivateToolTips
	void Awake ()
    {
        pm = transform.parent.gameObject.GetComponent<PieMenu>();

        sprites = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < sprites.Length; i++)
        {
            if (pm.icons[i] != null)
            {
                Texture2D tex2d = (Texture2D)pm.icons[i];
                Debug.Log(pm.icons[i].name);
                sprites[i].sprite = Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height), new Vector2());
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
