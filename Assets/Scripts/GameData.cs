using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    public string userName;
    public string bestScoreUserName;
    public int bestScore = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        bestScoreUserName = "you're First";
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void NewScore(int score)
    {
        bestScoreUserName = userName;
        bestScore = score;
        Save();
    }

    void Save()
    {
        SaveData saveData = new SaveData();
        saveData.bestScoreUserName = bestScoreUserName;
        saveData.bestScore = bestScore;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    void Load()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData savedata = JsonUtility.FromJson<SaveData>(json);

            bestScoreUserName = savedata.bestScoreUserName;
            bestScore = savedata.bestScore;
        }
    }

    class SaveData
    {
        public string bestScoreUserName;
        public int bestScore;
    }
}
