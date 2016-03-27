using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Responsible for flashing an image red
public class FlashColor : MonoBehaviour
{
    // The color we will flash
    public Color badFlashColor = Color.red;
    public float flashLength = 0.5f;
	public Color goodFlashColor = Color.green;

	private Color flashColor;
    private Image ourImage;
    private bool toFlash;
    private Color startColor;
    private float currentFlashLength;

	// Use this for initialization
	void Start ()
    {
        ourImage = GetComponent<Image>();
        toFlash = false;
        startColor = ourImage.color;
        currentFlashLength = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (toFlash)
        {
            if (currentFlashLength >= flashLength)
            {
                ourImage.color = flashColor;
                toFlash = false;
                currentFlashLength = 0.0f;
            }
            ourImage.color = Color.Lerp(startColor, flashColor, currentFlashLength / flashLength);
            currentFlashLength += Time.deltaTime;
        }
        else
        {
            if (currentFlashLength >= flashLength)
            {
                ourImage.color = startColor;
            }
            ourImage.color = Color.Lerp(flashColor, startColor, currentFlashLength / flashLength);
            currentFlashLength += Time.deltaTime;
        }
	}

    public void Flash()
    {
		flashColor = badFlashColor;
        toFlash = true;
        currentFlashLength = 0.0f;
    }

	public void GoodFlash()
	{
		flashColor = goodFlashColor;
		toFlash = true;
		currentFlashLength = 0.0f;
	}
}
