using UnityEngine;
using UnityEngine.UI;

public class LifeTracker : MonoBehaviour
{
    public static LifeTracker Instance { get; private set; }
    public int Lives { get; private set; }
    public Text lives_text;

    private readonly int starting_lives = 5;

    private void Start()
    {
        Lives = starting_lives;
        UpdateLivesText();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateLivesText()
    {
        lives_text.text = "Lives: " + Lives.ToString();
    }

    public void LoseALife()
    {
        Lives -= 1;
        UpdateLivesText();
    }

    public void Reset()
    {
        Start();
    }
}
