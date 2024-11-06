using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance { get; private set; }
    public int Score { get; private set; }

    public Text score_text;

    private void Start()
    {
        Score = 0;
        UpdateScoreText();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateScoreText()
    {
        score_text.text = "Score: " + Score.ToString("D6");
    }

    public void IncreaseScore(int points)
    {
        Score += points;
        UpdateScoreText();
    }

    public void Reset()
    {
        Start();
    }
}
