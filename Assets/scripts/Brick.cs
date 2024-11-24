using UnityEngine;
using System;
using System.Collections;

public class Brick : MonoBehaviour
{
    public GameObject game_obj;
    public ScoreKeeper ScoreKeeper;
    public int points_value;
    public int brick_id;
    public float[] brick_status_map;
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
        brick_status_map[brick_id] = 0.0f;
        Destroy(game_obj);
    }

    
}
