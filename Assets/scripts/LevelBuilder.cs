using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public BrickHandler BrickHandler;
    public int num_cols = 12, num_rows = 8;
    public float brick_x_space = 1.05f, brick_y_space = 0.8f;
    public float brick_x_start = -5.8f, brick_y_start = 0f;

    public GameObject Ball;
    public float ball_x_start = 0f, ball_y_start = -4f;

    public GameObject Slider;
    public float slider_x_start = 0f, slider_y_start = -6f;
    public float slider_left_bound = -5.5f;
    public float slider_right_bound = 5.5f;

    public void Build()
    {
        Vector3 start_block_placement = new(brick_x_start, brick_y_start);
        for (int i = 0; i < num_cols * num_rows; i++)
        {
            Vector3 block_spacing = new(brick_x_space * (i % num_cols),

            brick_y_start + (brick_y_space * (i / num_cols)));

            Vector3 next_block_placement = start_block_placement + block_spacing;
            Color brick_color = BrickHandler.Colors[(i / num_cols) % BrickHandler.Colors.Length];
            int points_value = (i / num_cols + 1) * 10;
            BrickHandler.PlaceBrick(next_block_placement, brick_color, points_value);
        }
        StartingPositions();
    }

    public void StartPlay()
    {
        Vector2 ball_force = new(0, -200);
        Rigidbody2D ball_rb = Ball.GetComponent<Rigidbody2D>();
        ball_rb.AddForce(ball_force);
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
}