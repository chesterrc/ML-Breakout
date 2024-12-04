using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class slider_agent : Agent
{
    public static slider_agent Instance { get; private set; }
    private Rigidbody2D slider;
    public GameObject bottom_border;
    public GameObject left_border;
    public GameObject right_border;
    public Transform target_ball;
    public LevelBuilder LevelBuilder;
    public ml_ball_collision collided_ball;

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

    // The Agent class calls this function before it makes a decision
    public override void CollectObservations(VectorSensor sensor)
    {
        // observe slider (agent) position's collider frame
        BoxCollider2D slider_frame = this.GetComponent<BoxCollider2D>();
        float left_border_position = left_border.transform.localPosition.x;
        float right_border_position = right_border.transform.localPosition.x;
        sensor.AddObservation(right_border_position - (this.transform.localPosition.x + slider_frame.bounds.size.x / 2));
        sensor.AddObservation(left_border_position - (this.transform.localPosition.x - slider_frame.bounds.size.x / 2));
        sensor.AddObservation(this.transform.localPosition.x + slider_frame.bounds.size.x / 5);
        sensor.AddObservation(this.transform.localPosition.x - slider_frame.bounds.size.x / 5);

        // observe ball position
        sensor.AddObservation(target_ball.localPosition);
        sensor.AddObservation(target_ball.localPosition.x - this.transform.localPosition.x);
        sensor.AddObservation(Vector2.Distance(target_ball.localPosition, this.transform.localPosition));

        // observe ball velocity:
        sensor.AddObservation(target_ball.GetComponent<Rigidbody2D>().velocity.x);
        sensor.AddObservation(target_ball.GetComponent<Rigidbody2D>().velocity.y);

        //observe walls
        sensor.AddObservation(left_border);
        sensor.AddObservation(right_border);
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

        // Debug.Log("ball " + target_ball.localPosition.x + " paddle " + this.transform.localPosition.x);

        // Rewards /
        if (Mathf.Abs(target_ball.localPosition.x) - 0.05 <= Mathf.Abs(this.transform.localPosition.x) &&
            Mathf.Abs(this.transform.localPosition.x) <= Mathf.Abs(target_ball.localPosition.x) + 0.05)
        {
            Debug.Log("rewarded for being close to ball");
            this.AddReward(0.001f);
        }

        // if slider misses ball
        if ( target_ball.position.y <= bottom_border.transform.position.y)
        {
            this.AddReward(-5f);
            EndEpisode();
        }
        
        // if level is cleared of bricks
        if ( LevelBuilder.brick_count == 0)
        {
            Debug.Log("<slider_agent> Game over! Level is clear.");
            this.AddReward(5.0f); // flat reward for beating level
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            Debug.Log("Collision of ball to slider adding reward");
            this.AddReward(0.01f);
        }
    }

    public void BrickBroken()
    {
        Debug.Log("Adding reward to broken brick");
        this.AddReward(1f);
    }
}
