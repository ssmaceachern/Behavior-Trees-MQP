using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PieMenu : MonoBehaviour
{

    public List<string> commands;
    public List<Texture> icons;

    public float iconSize = 64f;
    public float spacing = 12f;
    public float speed = 8f;
    public GUISkin skin;

    [HideInInspector]
    public float scale;
    [HideInInspector]
    public float angle;

    PieMenuManager manager;

    void Awake()
    {
        manager = PieMenuManager.Instance;
    }

    void OnMouseUp()
    {
        manager.Show(this);
    }

    public void AddCommand(string Command, Texture CommandIMG)
    {
        commands.Add(Command);
        icons.Add(CommandIMG);
    }

    public void RemoveCommand(string Command)
    {
        if (commands.Contains(Command))
        {
            int removeIndex = commands.IndexOf(Command);
            commands.RemoveAt(removeIndex);
            icons.RemoveAt(removeIndex);
        }
        
    }

}