using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public ScoreKeeper ScoreKeeper;
    public LifeTracker LifeTracker;
    public LevelBuilder LevelBuilder;
    public BrickHandler BrickHandler;
    public GameObject Slider;
    public GameObject Ball;
    private Rigidbody2D slider_rb;
    public Scene CurrentLevel;
    public bool PlayStarted { get; private set; } = false;
    public string[] names = new string [10];
    public int[] scores = new int [10];


    void Start()
    {
        CurrentLevel = SceneManager.GetActiveScene();
        StartGame(); 
        slider_rb = Slider.GetComponent<Rigidbody2D>();

        //on start, load existing high scores
        LoadScores();

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
            Vector3 direction = new(-0.2f, 0.0f, 0.0f);
            slider_rb.transform.Translate(direction);
        }

        if (Input.GetKey(KeyCode.RightArrow) && PlayStarted &&
            slider_rb.position.x < LevelBuilder.slider_right_bound)
        { // press RIGHT ARROW key to move slider
            Debug.Log("<GameController> Slider instructed to move RIGHT.");
            Vector3 direction = new(0.2f, 0.0f, 0.0f);
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
        if (ScoreKeeper.BrickCount > 3) //LevelBuilder.TotalBricks) 
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
        if (CurrentLevel.name == "level1") {
            SaveLS.InitGame();
        }

    }

    public void PlayerMissedBall()
    {
        LifeTracker.LoseALife();
        LevelBuilder.StartingPositions();
        PlayStarted = false;
    }

    void GameOver()
    {
        //check score, update files if necessary
        if (ScoreKeeper.Score >= scores[9]) 
        {
            AddHighScore();
        }

        SceneManager.LoadScene(0);
        SaveLS.InitGame();
        
        
    }

    void NextLevel()
    {
        //loads next scene/level
        if (CurrentLevel.name == "level1") {
            SceneManager.LoadScene("level2");
        }
        if (CurrentLevel.name == "level2") 
        {
            SceneManager.LoadScene("level3");
        }

        
    }

    void LoadScores()
    {

        string scoresLoc = Application.dataPath + "/scores.txt";
        string namesLoc = Application.dataPath + "/names.txt";
        StreamReader readNames = new StreamReader(namesLoc);
        StreamReader readScores = new StreamReader(scoresLoc);
        for (int x = 0; x < 10; ++x) 
        {
            string tmpName = readNames.ReadLine();
            Debug.Log("tmpName: " + tmpName);
            string tmpScore = readScores.ReadLine();
            int tmpScoreInt = System.Int16.Parse(tmpScore);
            Debug.Log("tmpScoreInt: " + tmpScoreInt);
            names[x] = tmpName;
            scores[x] = tmpScoreInt;
        }

        
        
    }
    
    void AddHighScore() 
    {
        for (int i = 0; i < 10; ++i) 
        {
            if (ScoreKeeper.Score >= scores[i])
            {
                //change scores and names here
            }
        }
    }
    
}
