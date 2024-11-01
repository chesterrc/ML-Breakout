using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    void Update() {
        Application.Quit();
        Debug.Log("exiting");
    }
    
}
