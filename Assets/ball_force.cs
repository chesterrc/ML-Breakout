using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_force : MonoBehaviour
{
    public Rigidbody2D ball;
    public float ball_force_x;
    public float ball_force_y;
    private bool space_flag = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 ball_force = new Vector2 (ball_force_x, ball_force_y);
        if(Input.GetKeyUp(KeyCode.Space) && space_flag == false){
            space_flag = true;
            ball.AddForce(ball_force);
        }
    }
}
