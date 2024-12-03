using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public ScoreKeeper ScoreKeeper;
    public LifeTracker LifeTracker;
    public LevelBuilder LevelBuilder;
    public BrickHandler BrickHandler;
    public GameObject Slider;
    private Rigidbody2D slider_rb;
    public Scene CurrentLevel;
    public static readonly string ScoreKey = "LevelEndScore";
    public static readonly string LivesKey = "NumLives";
    private int currentLives;
    private int currentScore;
    private int tmpScore;
    private int tmpLives;
    public bool PlayStarted { get; private set; } = false;


    void Start()
    {
        CurrentLevel = SceneManager.GetActiveScene();
        
        StartGame();
        
        
        slider_rb = Slider.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !PlayStarted)
        { // press SPACE key to start playing
            Debug.Log("<GameController> Play started.");
            PlayStarted = true;
            LevelBuilder.StartPlay();
        }

        if (Input.GetKey(KeyCode.LeftArrow) && PlayStarted && 
            slider_rb.position.x > LevelBuilder.slider_left_bound)
        { // press LEFT ARROW key to move slider
            Debug.Log("<GameController> Slider instructed to move LEFT.");
            Vector3 direction = new(-0.05f, 0.0f, 0.0f);
            slider_rb.transform.Translate(direction);
        }

        if (Input.GetKey(KeyCode.RightArrow) && PlayStarted &&
            slider_rb.position.x < LevelBuilder.slider_right_bound)
        { // press RIGHT ARROW key to move slider
            Debug.Log("<GameController> Slider instructed to move RIGHT.");
            Vector3 direction = new(0.05f, 0.0f, 0.0f);
            slider_rb.transform.Translate(direction);
        }

        if (Input.GetKeyUp(KeyCode.K) && PlayStarted)
        { // press K key to kill ball
            Debug.Log("<GameController> Ball killed.");
            PlayerMissedBall();
        }

        if (Input.GetKeyUp(KeyCode.R))
        { // press R key to reset game
            Debug.Log("<GameController> Game instructed to restart.");
            StartGame();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        { // press Q key to quit game and return to main menu
            Debug.Log("<GameController> Game instructed to return to main menu.");
            GameOver();
        }

        if (LifeTracker.Lives == 0)
        {
            GameOver();
        }

        //This is edited for testing level progression/score/lives. Correct = if (ScoreKeeper.BrickCount == LevelBuilder.TotalBricks)
        if (ScoreKeeper.BrickCount == LevelBuilder.TotalBricks) 
        {
            if (CurrentLevel.name == "level1" || CurrentLevel.name == "level2")
            {
                NextLevel();
            }
            else if (CurrentLevel.name == "level3")
            {
                Debug.Log("Winner");
                GameOver();
            }
            
        }

    }

    public void StartGame()
    {
        PlayStarted = false;
        LevelBuilder.Build();
    }

    public void PlayerMissedBall()
    {
        LifeTracker.LoseALife();
        LevelBuilder.StartingPositions();
        PlayStarted = false;
    }

    void GameOver()
    {
        SceneManager.LoadScene(0);
        SaveData(0, 5);
        
    }

    void NextLevel()
    {
        //loads next scene/level
        tmpScore = ScoreKeeper.GetScore();
        tmpLives = LifeTracker.GetLives();
        SaveData(tmpScore, tmpLives);
        if (CurrentLevel.name == "level1") {
            SceneManager.LoadScene("level2");
        }
        if (CurrentLevel.name == "level2") 
        {
            SceneManager.LoadScene("level3");
        }
        currentLives = LoadLives();
        LifeTracker.UpdateLives(currentLives);
        currentScore = LoadScore();
        ScoreKeeper.UpdateScore(currentScore);

        
    }

    public static void SaveData(int score, int lives)
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