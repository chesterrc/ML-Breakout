using UnityEngine;
using UnityEngine.SceneManagement;

public class start_menu : MonoBehaviour
{
    public EndingSceneGameController end_scene_game_controller;

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

    public void start_mach()
    {
        //starts machine game
        SceneManager.LoadScene("ml_training_scene");
        Debug.Log("Starting machine mode");
    }

    public void to_menu()
    {
        //go back to main menu
        SceneManager.LoadScene("menu");
        end_scene_game_controller.SaveData(0, 5);
    }


}
