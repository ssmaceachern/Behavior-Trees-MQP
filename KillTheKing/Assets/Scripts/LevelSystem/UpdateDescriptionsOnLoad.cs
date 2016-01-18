using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This gets stuck on the Canvas parent class
public class UpdateDescriptionsOnLoad : MonoBehaviour
{
    private LevelCoordinator LevelCoordinator;
    private LevelInfo LevelInfo;

    public Text LevelTitle;
    public Text LevelDescription;
    public Text ObjectiveTitle;
    public Text ObjectiveDescription;

    public Button PlayButton;

    void Start()
    {
        if (GameObject.Find("LevelCoordinator") != null)
        {

            //Get a reference to the LevelCoordinator script.
            LevelCoordinator = GameObject.Find("LevelCoordinator").GetComponent<LevelCoordinator>();
            Debug.Log(LevelCoordinator.GetLevelRegistry().Count);
            if(LevelCoordinator.GetLevelRegistry().TryGetValue(LevelCoordinator.GetLevelToBeLoaded(), out LevelInfo))
            {
                UpdateDescriptions();
            }
            else
            {
                Debug.LogError("LevelInfo not loaded");
            }
        }
    }

    void UpdateDescriptions()
    {
        LevelTitle.text = LevelInfo.LevelTitle;
        LevelDescription.text = LevelInfo.LevelDescription;
        ObjectiveTitle.text = LevelInfo.ObjectiveTitle;
        ObjectiveDescription.text = LevelInfo.ObjectiveDescription;

        PlayButton.onClick.AddListener(() => { LevelCoordinator.LoadLevelToBeLoaded(); });
    }
}
