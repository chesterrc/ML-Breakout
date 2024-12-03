using UnityEngine;
using System;
using System.Collections;

public class Brick : MonoBehaviour
{
    public GameObject game_obj;
    public ScoreKeeper ScoreKeeper;
    public int points_value;
    public int brick_id;
    public LevelBuilder levelBuilder;
    public BoxCollider2D brick_collider;

    void Start()
    {
        // Fetch Collider from gameobject
        brick_collider = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("<Brick.cs> Collision detected by a Brick.");
        ScoreKeeper.IncreaseScore(points_value);
        levelBuilder.brick_status_map[brick_id] = 0.0f;
        levelBuilder.brick_count--;
        Destroy(game_obj);
    }

    
}
