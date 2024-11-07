using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject game_obj;
    public ScoreKeeper ScoreKeeper;
    public int points_value;
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("<Brick.cs> Collision detected by a Brick.");
        ScoreKeeper.IncreaseScore(points_value);
        Destroy(game_obj);
        

    }

    
}
