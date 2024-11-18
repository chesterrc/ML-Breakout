using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class start_menu : MonoBehaviour
{
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
        // starts player vs cpu game
        SceneManager.LoadScene("versus");
        Debug.Log("Starting vs CPU mode");
    }


}
