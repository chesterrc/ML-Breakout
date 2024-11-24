using UnityEngine;
using UnityEngine.UI;

public class LifeTracker : MonoBehaviour
{
    public static LifeTracker Instance { get; private set; }
    public int Lives { get; private set; }
    public Text lives_text;

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
        SaveLS.SavedLives = Lives;
        Debug.Log("<LifeTracker> SaveLS.SavedLives updated to " + Lives);
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
        int lives = SaveLS.SavedLives;
        Debug.Log("<LifeTracker> SaveLS Loaded lives: " + lives);
        return lives;
    }

    public void Reset()
    {
        Start();
    }

    
}
