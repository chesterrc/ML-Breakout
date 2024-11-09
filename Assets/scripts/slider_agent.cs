using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using Unity.MLAgents.Actuators;
using UnityEngine.SocialPlatforms.Impl;
using Unity.Barracuda;

public class slider_agent : Agent
{
    public static slider_agent Instance { get; private set; }
    // Start is called before the first frame update
    Rigidbody2D slider;

    public GameObject bottom_border;
    public Transform target_ball;

    public LevelBuilder LevelBuilder;
    public ball_collision collided_ball;

    void Start()
    {
        Debug.Log("<slider_agent> Started");
        slider = GetComponent<Rigidbody2D>();
        // StartGame();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("<slider_agent> entered collision");
        // if slider collides with ball reward agent
        if(collision.gameObject.CompareTag("ball")){
            AddReward(0.3f);
        }
    }

    // function that is called when an episode ends
    public override void OnEpisodeBegin()
    {
        Debug.Log("<slider_agent> New Episode Starting");

        //Reset the ball and agent at the starting position
        LevelBuilder.StartingPositions();
        StartGame();
    }

    // The Agent class calls this function
    // before it uses the observation vector to make a decision
    public override void CollectObservations(VectorSensor sensor)
    {
        Debug.Log("<slider_agent> Obtaining observations");

        //Target and Agent positions
        sensor.AddObservation(target_ball.localPosition);
        sensor.AddObservation(this.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("<slider_agent> OnActionReceived");

        //Actions, size = 1 (only moving in one axis)
        Vector3 ControlSignal = Vector3.zero;
        ControlSignal.x = actions.ContinuousActions[0] * 0.2f;
        slider.transform.Translate(ControlSignal);
        // Debug.Log("OnActionReceived");
        // Debug.Log(ControlSignal);

        // Rewards
        float slider_position_x = Mathf.Abs( slider.transform.position.x);
        float ball_position_x = Mathf.Abs( target_ball.position.x );
        float distance_to_target = Mathf.Abs( slider_position_x - ball_position_x );
        // Debug.Log( distance_to_target );

        // brick broke by ball
        if( collided_ball.ball_collided )
        {
            Debug.Log("<slider_agent> ball broke brick");
            collided_ball.ball_collided = false;
            AddReward(0.8f);
        }
        if( distance_to_target <= 1.0f )
        {
            AddReward(0.1f);
        }
        
        if( target_ball.position.y <= bottom_border.transform.position.y)
        {
            // Slider failed to hit ball
            AddReward(-1.0f);
            EndEpisode();
        }

        if ( LevelBuilder.brick_count == 0)
        {
            AddReward(1.0f);
            EndEpisode();
        }
    }
    // for testing the environment manually
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Debug.Log("<Heuristic> Controlling slider");
        ActionSegment<float> continuous_actions_out = actionsOut.ContinuousActions;

        if (Input.GetKey(KeyCode.LeftArrow) &&
            slider.position.x > LevelBuilder.slider_left_bound){
                
                continuous_actions_out [0] = -1.0f;

            } else if (Input.GetKey(KeyCode.RightArrow) &&
                slider.position.x < LevelBuilder.slider_right_bound){
                
                continuous_actions_out [0] = 1.0f;
            }
    }

    public void StartGame()
    {
        // PlayStarted = false;
        LevelBuilder.Build();
        LevelBuilder.StartPlay();
    }

}
