using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class LevelBuilder : MonoBehaviour
{
    public BrickHandler BrickHandler;
    public const int num_cols = 12, num_rows = 8;
    public float brick_x_space = 1.05f, brick_y_space = 0.5f;
    public float brick_x_start = -5.8f, brick_y_start = 0f;
    public const int TotalBricks = num_cols * num_rows;

    public GameObject Ball;
    public float ball_x_start = 0f, ball_y_start = -4f;

    public GameObject Slider;
    public float slider_x_start = 0f, slider_y_start = -6f;
    public float slider_left_bound = -5.25f;
    public float slider_right_bound = 5.25f;

    public float[] brick_status_map {get; private set;}
    public bool building {get; private set;}

    public int BricksRemaining()
    {
        return brick_status_map.Count(b => b == 1.0f);

    }

    public void Build()
    {
        building = true;
        brick_status_map = new float[TotalBricks];

        Vector2 start_block_placement = new(brick_x_start, brick_y_start);
        for (int i = 0; i < num_cols * num_rows; i++)
        {
            Vector2 block_spacing = new(brick_x_space * (i % num_cols),
                brick_y_space * (i / num_cols));
            Vector2 next_block_placement = start_block_placement + block_spacing;
            Color brick_color = BrickHandler.Colors[(i / num_cols) % BrickHandler.Colors.Length];
            int points_value = (i / num_cols + 1) * 10;
            BrickHandler.PlaceBrick(next_block_placement, brick_color, this, i, points_value);
            brick_status_map[i] = 1.0f;
        }
        StartingPositions();
        building = false;
    }

    public void StartPlay()
    {
        Vector2 ball_force = new(0, -230);
        Rigidbody2D ball_rb = Ball.GetComponent<Rigidbody2D>();
        ball_rb.AddForce(ball_force);
        Debug.Log("LevelBuilder started play");
    }

    public void StartingPositions()
    {
        ResetGameObj(Ball, ball_x_start, ball_y_start);
        ResetGameObj(Slider, slider_x_start, slider_y_start);
    }

    private void ResetGameObj(GameObject gameObj, float x_start, float y_start)
    {
        gameObj.transform.position = new Vector2(x_start, y_start);
        Rigidbody2D rb = gameObj.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public void DestroyBrick(int brick_id) {
        brick_status_map[brick_id] = 0.0f;
    }
}