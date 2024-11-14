using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Starting 1P mode");
    }


    public void start_vs()
    {
        //TO-DO: for vs CPU scene

        Debug.Log("Starting vs CPU mode");
    }


}
