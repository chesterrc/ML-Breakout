using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour 
{
    List<ScoreboardName> scoreboardNames = new List<ScoreboardName> ();
    [SerializeField] int maxEntries = 10;
    [SerializeField] string jsonLoc = Application.dataPath + "/scores1.json";

    private void Start()
    {
        LoadHighScores();
    }


    public void SaveHighScores() 
    {
        //set up new saveScore object in case user gets a high score
        ScoreboardName brampton = new ScoreboardName("brampton", 210);
        string jsonTest = JsonUtility.ToJson(brampton);
        File.WriteAllText(jsonLoc, jsonTest);    
    }

    public void LoadHighScores()
    {
        Debug.Log("trample");
        /*
        string tmpScores = File.ReadAllText(jsonLoc);
        Debug.Log(tmpScores);
        ScoreboardName jsonLL = JsonUtility.FromJson<ScoreboardName>(tmpScores);
        Debug.Log(jsonLL.playerName);*/
    }

    public void AddNewScore(ScoreboardName newEntry)
    {
        //test
    }

}


