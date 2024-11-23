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

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("<slider_agent> Slider hit ball.");
        // if slider collides with ball reward agent
        if(collision.gameObject.CompareTag("ball")){
            AddReward(0.3f);
        }
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

        // observe which bricks are broken and which aren't
        sensor.AddObservation(LevelBuilder.BrickStatusMap.BrickObservations());
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

        // Rewards
        float slider_position_x = Mathf.Abs( slider.transform.position.x);
        float ball_position_x = Mathf.Abs( target_ball.position.x );
        float distance_to_target = Mathf.Abs( slider_position_x - ball_position_x );

        // brick broke by ball
        if( collided_ball.ball_collided )
        {
            Debug.Log("<slider_agent> ball broke brick");
            collided_ball.ball_collided = false;
            AddReward(0.8f);
        }

        // if slider is <= 1 unit from ball, reward based on proximity of slider to ball
        if (distance_to_target <= 1.0f)
        {
            float reward = Mathf.Clamp(1.0f - distance_to_target, 0.0f, 0.1f);
            AddReward(reward);
        }

        // if slider misses ball
        if ( target_ball.position.y <= bottom_border.transform.position.y)
        {
            AddReward(-1.0f/lives_per_game);
            lives -= 1;
            if (lives == 0) {
                Debug.Log("<slider_agent> Game over! Out of lives.");
                EndEpisode();
            } else {
                // reset ball & slider position; restart play
                Debug.Log("<slider_agent> Continuing level with " + lives.ToString() + " lives.");
                LevelBuilder.StartingPositions();
                LevelBuilder.StartPlay();
            }
        }

        if ( LevelBuilder.brick_count == 0)
        {
            Debug.Log("<slider_agent> Game over! Level is clear.");
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
