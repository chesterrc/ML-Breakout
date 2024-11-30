using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using System.Collections;

public class slider_agent : Agent
{
    public static slider_agent Instance { get; private set; }
    private Rigidbody2D slider;

    public GameObject bottom_border;
    public Transform target_ball;

    public LevelBuilder LevelBuilder;
    public ball_collision collided_ball;

    private int lives;

    private float ball_last_y;
    private int ball_y_count;
    private const int max_obs_for_ball_at_same_y = 10;

    void Start()
    {
        Debug.Log("<slider_agent> Training started.");
        slider = GetComponent<Rigidbody2D>();
    }

    // function that is called when an episode ends
    public override void OnEpisodeBegin()
    {
        lives = 5;
        ball_y_count = 0;
        ball_last_y = get_ball_y_position();

        Debug.Log("<slider_agent> New Episode Starting");

        // start new game:
        LevelBuilder.Build();              // create bricks
        StartCoroutine(WaitForLevelConstruction());
        LevelBuilder.StartPlay();          // begin play
    }

    private IEnumerator WaitForLevelConstruction() {
        yield return new WaitUntil(() => !LevelBuilder.building);
    }

    private float get_ball_y_position()
    {
        return target_ball.localPosition.y;
    }

    // The Agent class calls this function before it makes a decision
    public override void CollectObservations(VectorSensor sensor)
    {
        // observe slider (agent) position
        sensor.AddObservation(this.transform.localPosition);

        // observe slider velocity
        sensor.AddObservation(this.GetComponent<Rigidbody2D>().velocity);

        // observe ball position
        sensor.AddObservation(target_ball.localPosition);

        // observe ball velocity:
        sensor.AddObservation(target_ball.GetComponent<Rigidbody2D>().velocity);

        // observe ball angular velocity
        sensor.AddObservation(target_ball.GetComponent<Rigidbody2D>().angularVelocity);

        // observe which bricks are broken and which aren't
        sensor.AddObservation(LevelBuilder.brick_status_map);
    }

    // This is called after CollectObservations
    public override void OnActionReceived(ActionBuffers actions)
    {
        //Actions, size = 1 (only moving in one axis)
        Vector3 ControlSignal = Vector3.zero;
        ControlSignal.x = actions.ContinuousActions[0] * 0.2f;
        slider.transform.Translate(ControlSignal);

        // keep slider in boundaries
        if (slider.transform.position.x < LevelBuilder.slider_left_bound || slider.transform.position.x > LevelBuilder.slider_right_bound)
        {
            slider.transform.position = new Vector3(
                Mathf.Clamp(slider.transform.position.x, LevelBuilder.slider_left_bound, LevelBuilder.slider_right_bound),
                slider.transform.position.y,
                slider.transform.position.z
            );
        }

        // if ball gets stuck, kill it
        if (ball_last_y == get_ball_y_position())
        {
            ball_y_count++;
        }
        if (ball_y_count > max_obs_for_ball_at_same_y)
        {
            ball_y_count = 0;
            LevelBuilder.StartingPositions();
            LevelBuilder.StartPlay();            
        }
        ball_last_y = get_ball_y_position();

        // Rewards //

        int bricks_remaining = LevelBuilder.BricksRemaining();

        // continuous survival reward; decreases as bricks are destroyed
        //float reward_per_time = (0.01f / LevelBuilder.TotalBricks) * Time.deltaTime * LevelBuilder.BricksRemaining();
        //AddReward(reward_per_time);

        // if brick broke by ball
        if( collided_ball.ball_collided )
        {
            Debug.Log("<slider_agent> ball broke brick; " + bricks_remaining.ToString() + " more to go");
            collided_ball.ball_collided = false;
            float brick_break_reward = 1.0f/LevelBuilder.TotalBricks;
            AddReward(brick_break_reward);
        }

        // if slider hits the ball
        if(collided_ball.ball_hit_slider){
            collided_ball.ball_hit_slider = false;
            // decidedly not rewarding for this
        }

        // if slider misses ball
        if ( target_ball.position.y <= bottom_border.transform.position.y)
        {
            lives--;
            float ball_miss_penalty = -1.0f;
            AddReward(ball_miss_penalty);
            // reset ball & slider position; restart play
            LevelBuilder.StartingPositions();
            LevelBuilder.StartPlay();

        }
        
        // if level is cleared of bricks
        if ( bricks_remaining == 0)
        {
            float reward_for_clearing_level = 1.0f;
            float reward_per_life = 1.0f;
            for (int i=0; i < lives; ++i)
            {
                AddReward(reward_per_life);
            }
            Debug.Log("<slider_agent> Game over! Level is clear.");
            AddReward(reward_for_clearing_level); // flat reward for beating level
            EndEpisode();
        }
    }

    // for testing the environment manually
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //Debug.Log("<Heuristic> Controlling slider");
        ActionSegment<float> continuous_actions_out = actionsOut.ContinuousActions;

        if (Input.GetKey(KeyCode.LeftArrow) &&
            slider.position.x > LevelBuilder.slider_left_bound){
                
                continuous_actions_out [0] = -.8f;

            } 
        else if (Input.GetKey(KeyCode.RightArrow) &&
                slider.position.x < LevelBuilder.slider_right_bound){
                
                continuous_actions_out [0] = .8f;
            }
        else if (slider.transform.position.x < LevelBuilder.slider_left_bound || slider.transform.position.x > LevelBuilder.slider_right_bound)
        {
            slider.transform.position = new Vector3(
                Mathf.Clamp(slider.transform.position.x, LevelBuilder.slider_left_bound, LevelBuilder.slider_right_bound),
                slider.transform.position.y,
                slider.transform.position.z
            );
        }
    }
}
