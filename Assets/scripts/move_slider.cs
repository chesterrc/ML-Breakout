using UnityEngine;

public class move_slider : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 direction;
    // private float thrust = 0.25f;
    Vector2 left_bound = new Vector2(-5.50f, 0.0f);
    Vector2 right_bound = new Vector2(5.5f, 0.0f);
    private bool start_flag = false;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        Vector2 obj_position = rb.position;

        // Slider will only move when the game starts
        if (Input.GetKeyUp(KeyCode.Space) && start_flag == false){
            start_flag = true;
        }

        if (start_flag){
            if (Input.GetKey(KeyCode.LeftArrow) &&
            obj_position[0] > left_bound[0]){
                Debug.Log("Player input LEFT detected.");
                direction = new Vector3(-0.2f, 0.0f, 0.0f);
                rb.transform.Translate(direction);
            } else if (Input.GetKey(KeyCode.RightArrow) &&
                obj_position[0] < right_bound[0]){
                Debug.Log("Player input RIGHT detected.");
                direction = new Vector3(0.2f, 0.0f, 0.0f);
                rb.transform.Translate(direction);
            }
            direction = Vector3.zero;
            rb.transform.Translate(direction);
        }
    }
}
