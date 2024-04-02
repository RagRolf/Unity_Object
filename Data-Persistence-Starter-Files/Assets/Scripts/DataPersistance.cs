using System.IO;
using TMPro;
using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    public static DataPersistance own;
    private static string ending = "/savefile.json";
    private TextMeshProUGUI playerNameRef;
    [HideInInspector] public string playerNameSelf;
    [HideInInspector] public string playerNameWinner = "Nameless";
    public int highScore;
    private void Awake()
    {
        playerNameRef = GameObject.FindGameObjectsWithTag("UI")[0].GetComponent<TextMeshProUGUI>();
        own = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }
    [System.Serializable]
    class SavedData
    {
        public int highScore;
        public string playerName;
    }
    public void SaveWinner()
    {
        SavedData data = new SavedData();
        data.playerName = playerNameWinner;
        data.highScore = highScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + ending, json);
    }
    public void SetName()
    {
        playerNameSelf = playerNameRef.text.Length == 1 ? "Nameless" : playerNameRef.text;
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + ending;
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);
            playerNameWinner = data.playerName;
            highScore = data.highScore;
        }

    }
}
