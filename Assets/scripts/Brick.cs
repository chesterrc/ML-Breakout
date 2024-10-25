using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject game_obj;
    public ScoreKeeper score_keeper;
    public int points_value;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Brick detects collision.");
        score_keeper.IncreaseScore(points_value);
        Destroy(game_obj);
    }
}
