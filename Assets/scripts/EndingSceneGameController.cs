using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneGameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public ScoreKeeper ScoreKeeper;
    public LifeTracker LifeTracker;
    public static readonly string ScoreKey = "LevelEndScore";
    public static readonly string LivesKey = "NumLives";
    private int currentLives;
    private int currentScore;
    private int tmpScore;
    private int tmpLives;

    void Start()
    {
        currentLives = LoadLives();
        LifeTracker.UpdateLives(currentLives);
        currentScore = LoadScore();
        ScoreKeeper.UpdateScore(currentScore);
    }

    public void SaveData(int score, int lives)
    {
        //saves score, lives to file
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.SetInt(LivesKey, lives);
        PlayerPrefs.Save();
        Debug.Log("Score saved: " + score);
        Debug.Log("Lives saved: " + lives);
    }

    public static int LoadScore()
    {
        //load score from file
        int score = PlayerPrefs.GetInt(ScoreKey);
        Debug.Log("Loaded score: " + score);
        return score;
    }

    public static int LoadLives()
    {
        int lives = PlayerPrefs.GetInt(LivesKey);
        Debug.Log("Loaded lives: " + lives);
        return lives;
    }
}