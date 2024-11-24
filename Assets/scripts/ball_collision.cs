using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_collision : MonoBehaviour
{
    private Rigidbody2D ball_col;
    // Start is called before the first frame update
    public GameController game_controller;
    public LevelBuilder LevelBuilder;
    public bool ball_collided = false;
    
    void Start()
    {
        //Cache the reference to the rigid body.
        ball_col = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("ball collision");
        if ( collision.gameObject.CompareTag("slider") )
        {
            //Get the difference in x to see if we hit the left or right side
            float halfWidth = collision.collider.bounds.size.x;
            float x = (transform.position.x - collision.transform.position.x) / halfWidth;
            if (x < 0.1 & x > -.1)
            {
                x = 0; 
            }


            Vector2 direction = new(3 * x, 1);
            direction = direction.normalized;

            float currentspead = ball_col.velocity.magnitude;
            ball_col.velocity = direction * currentspead;
        }
        else if (collision.gameObject.CompareTag("brick"))
        {
            // Debug.Log("hit a brick");
            LevelBuilder.brick_count--;
            ball_collided = true;
            //Get the difference in x to see if we hit the left or right side


        }
        else if (collision.gameObject.CompareTag("bottom"))
        {
            // Debug.Log("Missed slider.");
            game_controller.PlayerMissedBall();
        }
    }
}
    
