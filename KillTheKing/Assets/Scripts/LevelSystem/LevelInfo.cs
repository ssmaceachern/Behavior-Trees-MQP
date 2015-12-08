using System.IO;
using System.Collections;

public class LevelInfo {

    //Filepath
    string path;

    //Info to be loaded
    int index;

    string  LevelTitle,
            LevelDescription,
            ObjectiveTitle,
            ObjectiveDescription;

    //Constructor
    public LevelInfo(string SceneName)
    {
        if (File.Exists(path))
        {
            // Read the names of all levels from the levels file.
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Possibly use ReadToEnd and string.Split(fileContent, Environment.NewLine).
                    while (!reader.EndOfStream)
                    {
                        levelNames.Add(reader.ReadLine());
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Levels file does not exist, no level names available at run-time.");
        }
    }

}
