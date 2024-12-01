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
        LevelBuilder.Build();              // create bricks
        LevelBuilder.StartingPositions();  // ball & slider to start pos
        LevelBuilder.StartPlay();          // begin play
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            Debug.Log("Collision of ball to slider adding reward");
            AddReward(1.0f);
        }
    }
    // The Agent class calls this function before it makes a decision
    public override void CollectObservations(VectorSensor sensor)
    {
        // observe slider (agent) position's collider frame
        BoxCollider2D slider_frame = this.GetComponent<BoxCollider2D>();
        sensor.AddObservation(this.transform.localPosition.x + slider_frame.bounds.size.x / 2);
        sensor.AddObservation(this.transform.localPosition.x - slider_frame.bounds.size.x / 2);

        // observe ball position
        sensor.AddObservation(target_ball.localPosition);

        // observe ball velocity:
        sensor.AddObservation(target_ball.GetComponent<Rigidbody2D>().velocity);
    }

    // This is called after CollectObservations
    public override void OnActionReceived(ActionBuffers actions)
    {
        //Actions, size = 1 (only moving in one axis)
        Vector3 ControlSignal = Vector3.zero;
        ControlSignal.x = actions.ContinuousActions[0] * 0.3f;
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

        // // Rewards /
        // if (Mathf.Abs(target_ball.localPosition.x) - 0.2 <= Mathf.Abs(this.transform.localPosition.x) &&
        //     Mathf.Abs(this.transform.localPosition.x) <= Mathf.Abs(target_ball.localPosition.x) + 0.2)
        // {
        //     AddReward(0.1f);
        // }

        // if slider misses ball
        if ( target_ball.position.y <= bottom_border.transform.position.y)
        {
            AddReward(-5f);
            EndEpisode();
        }
        
        // if level is cleared of bricks
        if ( LevelBuilder.brick_count == 0)
        {
            Debug.Log("<slider_agent> Game over! Level is clear.");
            AddReward(5.0f); // flat reward for beating level
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

    public void BrickBroken()
    {
        Debug.Log("Adding reward to broken brick");
        AddReward(1.0f);
    }
}
