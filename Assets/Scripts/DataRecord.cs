using System.IO;
using UnityEngine;
using System;

public class DataRecord : MonoBehaviour
{
    public static void GenerateCSVFile(string fileName, string text)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".csv");
        if (!File.Exists(path))
        {
            File.WriteAllText(path, string.Empty);
        }

        using (TextWriter writer = File.AppendText(path))
        {
            writer.Write(text);
            writer.Close();
        }

    }
    public static void GenerateNewFile(string fileName, string type, string text)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + "." + type);
        if (!File.Exists(path))
        {
            File.WriteAllText(path, string.Empty);
        }

        using (TextWriter writer = File.CreateText(path))
        {
            writer.Write(text);
            writer.Close();
        }

    }

    public static void AppendFile(string fileName, string type, string text)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + "." + type);
        if (!File.Exists(path))
        {
            File.WriteAllText(path, string.Empty);
        }

        using (TextWriter writer = File.AppendText(path))
        {
            writer.Write(text);
            writer.Close();
        }

    }
}