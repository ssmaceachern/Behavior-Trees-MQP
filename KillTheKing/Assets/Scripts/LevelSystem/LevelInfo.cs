using System.IO;
using System.Collections;
using UnityEngine;

public class LevelInfo {

    //Info to be loaded
    int index;

    public bool isComplete { get; private set; }
    public bool subquestComplete { get; private set; }

    public string  FileLoc,
            LevelTitle,
            LevelDescription,
            ObjectiveTitle,
            ObjectiveDescription;

    //Constructor
    public LevelInfo(string SceneName)
    {
        isComplete = false;

        FileLoc = "Descriptions/" + SceneName;

        if (Resources.Load(FileLoc) != null)
        {
            ParseLevelDescriptionFile(FileLoc);
        }
        else
        {
            Debug.LogWarning("Levels file does not exist, no level names available at run-time.");
		}

    }

    /// <summary>
    /// Parses a level's description file for relevant details
    /// </summary>
    /// <param name="FileLoc">Location of the text file in the Resources Folder</param>
    void ParseLevelDescriptionFile(string FileLoc)
    {

		TextAsset ta = Resources.Load(FileLoc) as TextAsset;
		string[] lines = ta.text.Split('\n');
		foreach(string line in lines)
		{
                    string[] tokens = line.Split('=');

                    switch (tokens[0])
                    {
                        case "LevelTitle":
                        LevelTitle = tokens[1].Substring(tokens[1].IndexOf('[') + 1, tokens[1].IndexOf(']') - 1);
                        break;
                        case "LevelDescription":
                        LevelDescription = tokens[1].Substring(tokens[1].IndexOf('[') + 1, tokens[1].IndexOf(']') - 1);
                        break;
                        case "ObjectiveTitle":
                        ObjectiveTitle = tokens[1].Substring(tokens[1].IndexOf('[') + 1, tokens[1].IndexOf(']') - 1);
                        break;
                        case "ObjectiveDescription":
                        ObjectiveDescription = tokens[1].Substring(tokens[1].IndexOf('[') + 1, tokens[1].IndexOf(']') - 1);
                        break;
                        default:
                        Debug.LogWarning("Unknown Information is in the file.");
                        break;
                    }
		}
    }


    /// <summary>
    /// Example for how to use/call these functions within another class
    /// </summary>
    /// if (LevelCoordinator.instance.GetLevelRegistry().ContainsKey(LevelCoordinator.instance.currentLevel))
	///		{
	///			LevelCoordinator.instance.GetLevelRegistry()[LevelCoordinator.instance.currentLevel].setComplete(true);
    ///        }
	///		else{
	///			Debug.LogError("Could not update level information");
	///		}
    /// <param name="val"></param>

    public void setComplete(bool val) { isComplete = val; }
    public void setSubquestComplete(bool val) { isComplete = val; }
}
