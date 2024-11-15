using UnityEngine;
using UnityEngine.UI;

public class LifeTracker : MonoBehaviour
{
    public static LifeTracker Instance { get; private set; }
    public int Lives { get; private set; }
    public Text lives_text;
    public static readonly string LivesKey = "NumLives";
    private int currentLives;
    private int tmpLives;

    private readonly int starting_lives;

    private void Start()
    {
        Lives = LoadLives();
        UpdateLivesText();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateLivesText()
    {
        lives_text.text = "Lives: " + Lives.ToString();
    }

    public void LoseALife()
    {
        Lives -= 1;
        UpdateLivesText();
    }

    public int GetLives()
    {
        return Lives;
    }

    public void UpdateLives(int CurrentLives)
    {
        Lives = CurrentLives;
        UpdateLivesText();
        Debug.Log("UpdateLives/CurrentLives: " + Lives);
    }

    public static int LoadLives()
    {
        int lives = PlayerPrefs.GetInt(LivesKey);
        Debug.Log("Loaded lives: " + lives);
        return lives;
    }

    public void Reset()
    {
        Start();
    }

    
}
