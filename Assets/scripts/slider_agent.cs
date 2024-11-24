using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class slider_agent : Agent
{
    public static slider_agent Instance { get; private set; }
    private Rigidbody2D slider;

    public GameObject bottom_border;
    public Transform target_ball;

    public LevelBuilder LevelBuilder;
    public ball_collision collided_ball;
    public int lives_per_game = 5;
    private int lives;

    void Start()
    {
        Debug.Log("<slider_agent> Training started.");
        slider = GetComponent<Rigidbody2D>();
    }

    // function that is called when an episode ends
    public override void OnEpisodeBegin()
    {
        Debug.Log("<slider_agent> New Episode Starting");

        // start new game:
        lives = lives_per_game;            // reset lives to max lives
        LevelBuilder.Build();              // create bricks
        LevelBuilder.StartingPositions();  // ball & slider to start pos
        LevelBuilder.StartPlay();          // begin play
    }

    // The Agent class calls this function before it makes a decision
    public override void CollectObservations(VectorSensor sensor)
    {
        // observe slider (agent) position
        sensor.AddObservation(this.transform.localPosition);

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

        // Rewards //

        // continuous survival reward; decreases as bricks are destroyed
        float reward_per_time = (0.01f / LevelBuilder.TotalBricks) * Time.deltaTime * LevelBuilder.brick_count;
        AddReward(reward_per_time);

        // if brick broke by ball
        if( collided_ball.ball_collided )
        {
            Debug.Log("<slider_agent> ball broke brick; " + LevelBuilder.brick_count.ToString() + " more to go");
            collided_ball.ball_collided = false;            
            AddReward(0.1f);
            AddReward(1.0f/LevelBuilder.brick_count);
        }

        // if slider hits the ball
        if(collided_ball.ball_hit_slider){
            Debug.Log("<slider_agent> Slider hit ball.");
            collided_ball.ball_hit_slider = false;
            AddReward(0.8f);
        }

        // if slider misses ball
        if ( target_ball.position.y <= bottom_border.transform.position.y)
        {
            AddReward(-1.0f);
            lives -= 1;
            if (lives == 0) {
                Debug.Log("<slider_agent> Game over! Out of lives.");
                AddReward(-1.0f);
                EndEpisode();
            } else {
                // reset ball & slider position; restart play
                Debug.Log("<slider_agent> Continuing level with " + lives.ToString() + " lives.");
                LevelBuilder.StartingPositions();
                LevelBuilder.StartPlay();
            }
        }
        
        // if level is cleared of bricks
        if ( LevelBuilder.brick_count == 0)
        {
            Debug.Log("<slider_agent> Game over! Level is clear.");
            AddReward(1.0f); // flat reward for beating level
            for (int i=1; i < lives; ++i) {
                // bonus reward for beating level with more than 1 life remaining
                AddReward(1.0f);
            }
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
