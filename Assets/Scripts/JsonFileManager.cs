using System.IO;
using UnityEngine;

public static class JsonFileHandler
{
    public static void SaveToJson<T>(T obj, string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        string json = JsonUtility.ToJson(obj, true);
        File.WriteAllText(path, json);
        Debug.Log("Data saved to: " + path);
    }

    public static T LoadFromJson<T>(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            T obj = JsonUtility.FromJson<T>(json);
            Debug.Log("Data loaded from: " + path);
            return obj;
        }
        else
        {
            Debug.LogWarning("File not found: " + path);
            return default;
        }
    }
}