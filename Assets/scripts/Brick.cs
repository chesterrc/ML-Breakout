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
    
    private bool unbroken = true;

    void Start()
    {
        // Fetch Collider from gameobject
        brick_collider = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (unbroken) {
            unbroken = false;
            Destroy(game_obj);
            Debug.Log("<Brick.cs> Collision detected by brick #" + brick_id.ToString() + ".");
            ScoreKeeper.IncreaseScore(points_value);
            levelBuilder.DestroyBrick(brick_id);
        }
    }

    
}
