using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_menu : MonoBehaviour
{
    public GameObject btn_start;
    public GameObject start_sub;
    public void Start() {
        
    }
    public void start_click() {
        //deactivate start button, activate sub-menu for 1p/cpu 
        btn_start.SetActive(false);
        start_sub.SetActive(true);
    }

    public void exit() {
        Application.Quit();
        Debug.Log("exiting");
    }

    public void start_1p() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void start_vs() {
        //for vs CPU scene
    }
}
