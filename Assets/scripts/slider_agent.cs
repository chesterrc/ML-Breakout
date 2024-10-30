using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using Unity.MLAgents.Actuators;

public class slider_agent : Agent
{
    // Start is called before the first frame update
    Rigidbody2D slider;
    public Transform target_ball;
    Vector2 left_bound = new Vector2(-5.5f, 0.0f);
    Vector2 right_bound = new Vector2(5.5f, 0.0f);
    
    void Start()
    {
        slider = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter(Collision collision){

        // if slider collides with ball reward agent
        if(collision.gameObject.CompareTag("ball")){
            AddReward(1.0f);
        }
    }
    public override void OnEpisodeBegin()
    {
        //Reset the ball at the starting position if it falls pass the slider
        if (target_ball.position.y < slider.position.y){
            target_ball.position = new Vector3(0.0f, -4.0f, 0.0f);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Target and Agent positions
        sensor.AddObservation(target_ball.localPosition);
        sensor.AddObservation(this.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Actions, size = 1 (only moving in one axis)
        Vector3 ControlSignal = Vector3.zero;
        ControlSignal.x = actions.ContinuousActions[0] * 0.2f;
        slider.transform.Translate(ControlSignal);
        Debug.Log("OnActionReceived");
        Debug.Log(ControlSignal);

        // Rewards
        float DistanceToTarget = slider.position.x - this.transform.position.x;

        
        if (DistanceToTarget < 0.2f){
            // Reached ball
            AddReward(1.0f);
        }else if(target_ball.position.y < slider.position.y){
            // Slider failed to hit ball
            AddReward(-1.0f);
            EndEpisode();
        }
    }
    // for testing the environment
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        
        if (Input.GetKey(KeyCode.LeftArrow) &&
            slider.position.x > left_bound.x){
                
                continuousActions[0] = -1.0f;

            } else if (Input.GetKey(KeyCode.RightArrow) &&
                slider.position.x < right_bound.x){
                
                continuousActions[0] = 1.0f;
            }
    }
}
