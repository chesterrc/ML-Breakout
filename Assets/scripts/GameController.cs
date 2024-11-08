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

    public bool PlayStarted { get; private set; } = false;

    void Start()
    {
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

        if (LifeTracker.Lives == 0)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        PlayStarted = false;
        LifeTracker.Reset();
        ScoreKeeper.Reset();
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
    }
}