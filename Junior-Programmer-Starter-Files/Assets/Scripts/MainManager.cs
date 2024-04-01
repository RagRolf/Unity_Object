using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    public Color teamColour;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }
[System.Serializable]
class SaveData
{
    public Color teamColor;
}
public void SaveColor()
{
    SaveData data = new SaveData();
    data.teamColor = teamColour;
    string json = JsonUtility.ToJson(data);
    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
}
public void LoadColor()
{
    string path = Application.persistentDataPath + "/savefile.json";
    if (File.Exists(path))
    {
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        teamColour = data.teamColor;
    }
}
}