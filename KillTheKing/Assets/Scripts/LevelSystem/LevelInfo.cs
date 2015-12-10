using System.IO;
using System.Collections;
using UnityEngine;

public class LevelInfo {

    //Filepath
    string path = Application.dataPath + "/Scenes/MainLevels/Descriptions/";

    //Info to be loaded
    int index;

    public string  FileLoc,
            LevelTitle,
            LevelDescription,
            ObjectiveTitle,
            ObjectiveDescription;

    //Constructor
    public LevelInfo(string SceneName)
    {
        GenerateFileLoc(SceneName);

        //Debug.Log(path + SceneName);
        if (File.Exists(FileLoc))
        {
            ParseLevelDescriptionFile(path);
        }
        else
        {
            Debug.LogWarning("Levels file does not exist, no level names available at run-time.");
        }

    }

    void GenerateFileLoc(string SceneName)
    {
        string DescriptionFileName = SceneName.Substring(0, SceneName.IndexOf(".")) + ".txt";
        //Debug.Log(DescriptionFileName);

        FileLoc = path + DescriptionFileName;
    }

    void ParseLevelDescriptionFile(string path)
    {
        // Read the names of all levels from the levels file.
        using (FileStream stream = File.Open(FileLoc, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                //File.SetAttributes(FileLoc, FileAttributes.Normal);
                // Possibly use ReadToEnd and string.Split(fileContent, Environment.NewLine).
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    //tokens[0] = Variable
                    //tokens[1] = [Description]
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
        }
    }
}
