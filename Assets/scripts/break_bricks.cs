using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class break_bricks : MonoBehaviour
{
    public GameObject brick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Inside on collision enter");
        Destroy(brick);
    }
    // Update is called once per frame
    void Update()
    {
    
    }
}
