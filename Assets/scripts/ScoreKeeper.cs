using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance { get; private set; }
    public int score { get; private set; }

    public Text score_text;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateScoreText()
    {
        score_text.text = "Score: " + score.ToString("D6");
    }

    public void IncreaseScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void Reset()
    {
        score = 0;
    }
}
