using UnityEditor;
using System.IO;
using UnityEngine;
using System;

public class FileActor : MonoBehaviour
{
    private void AppendText(string text)
    {
        string _historyFile = Application.dataPath + "/history.txt";

        if(!File.Exists(_historyFile))
        {
            File.WriteAllText(_historyFile, text);
        }
        else
        {
            using (var writer = new StreamWriter(_historyFile, true))
            {
                writer.WriteLine(text);
            }
        }
    }

    public void DeleteAll()
    {
        string _historyFile = Application.dataPath + "/history.txt";

        if (!File.Exists(_historyFile))
        {
            File.WriteAllText(_historyFile, "");
        }
        else
        {
            using (var writer = new StreamWriter(_historyFile, false))
            {
                writer.WriteLine("");
            }
        }
    }

    public void GenerateText(string actor, string action)
    {
        string text = DateTime.Now.ToString() + ": " + actor + " -> " + action;
        AppendText(text);
    }
}