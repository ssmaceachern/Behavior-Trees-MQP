using UnityEngine;
using System.Collections;

// Stores references to the different sound effects to allow for random sounds to be chosen
public class AudioManager : MonoBehaviour
{
    public AudioClip[] arrowSounds;
    public AudioClip[] swordSounds;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private AudioClip getRandomSoundFromArray(AudioClip[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public AudioClip getRandomArrowSound()
    {
        return getRandomSoundFromArray(arrowSounds);
    }

    public AudioClip getRandomSwordSound()
    {
        return getRandomSoundFromArray(swordSounds);
    }
}
