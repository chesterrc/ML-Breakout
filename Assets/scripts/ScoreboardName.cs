using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreboardName
{
    [SerializeField] public string playerName;
    [SerializeField] public int playerScore;

    public ScoreboardName(string name, int newScore)
    {
        playerName = name;
        this.playerScore = newScore;
    }
        
}
