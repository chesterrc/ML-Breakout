using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class start_menu : MonoBehaviour
{
    //instantiates the Start Button object
    public GameObject btn_start;
    //instantiates the sub-menu for if player clicks Start
    public GameObject start_sub;
    public void Start()
    {

    }
    public void start_click()
    {
        //deactivate start button, activate sub-menu for either 1P or vs CPU
        btn_start.SetActive(false);
        start_sub.SetActive(true);
        Debug.Log("Opening Start game sub-menu/deactivating initial Start GameObject");
    }

    public void exit()
    {
        //for exit button
        Application.Quit();
        Debug.Log("Exiting");
    }

    public void start_1p()
    {
        //starts standard 1-player game.
        //uses scene index in Build, can be updated to specifc scene
        SceneManager.LoadScene("level1");
        Debug.Log("Starting 1P mode");
    }


    public void start_vs()
    {
        //TO-DO: for vs CPU scene

        Debug.Log("Starting vs CPU mode");
    }


}
