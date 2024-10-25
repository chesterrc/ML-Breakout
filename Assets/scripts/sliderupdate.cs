using UnityEngine;

public class move_slider2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float thrust = 0.25f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Player input LEFT detected.");
            direction = Vector2.left;
            Debug.Log("Adding force " + (direction * thrust).ToString("F4"));
            rb.AddForce(direction * thrust, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Player input RIGHT detected.");
            direction = Vector2.right;
            Debug.Log("Adding force " + (direction * thrust).ToString("F4"));
            rb.AddForce(direction * thrust, ForceMode2D.Impulse);
        }
        else
        {
            direction = Vector2.zero;
            Debug.Log("Adding force " + (direction * thrust).ToString("F4"));
            rb.AddForce(direction * thrust, ForceMode2D.Impulse);
        }
    }
}
