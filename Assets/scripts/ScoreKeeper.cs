using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance { get; private set; }
    public int Score { get; private set; }
    public int BrickCount {get; private set; }
    public Text score_text;

    private void Start()
    {
        Score = LoadScore();
        BrickCount = 0;
        UpdateScoreText();
        SaveLS.BricksRemaining = 96;
        
    }

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScoreText()
    {
        score_text.text = "Score: " + Score.ToString("D6");
    }

    public void IncreaseScore(int points)
    {
        Score += points;
        UpdateScoreText();
        BrickCount++;
        Debug.Log("Bricks destroyed: " + BrickCount);
        SaveLS.SavedScore = Score;
        Debug.Log("<ScoreKeeper> SaveLS.SavedScore updated to: " + Score);
        SaveLS.BricksRemaining--;
        Debug.Log("<ScoreKeeper> SaveLS.BricksRemaining updated to: " + SaveLS.BricksRemaining);
        
    }

    public void resetBrickCount() 
    {
        BrickCount = 0;
        
    }

    public int GetScore()
    {
        return Score;
    }

    
    public void UpdateScore(int NewScore)
    {
        Score = NewScore;
        UpdateScoreText();
    }

    public static int LoadScore()
    {
        int score = SaveLS.SavedScore;
        Debug.Log("Loaded score: " + score);
        return score;
    }

    public void Reset()
    {
        Start();
    }
}
