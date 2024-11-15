using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class miss_ball : MonoBehaviour
{
    public GameController game_controller;

    private void OnTriggerEnter2D(Collider2D collision)
      {
        //restarts scene. Add # of plays counter, return to menu, or ...
        Debug.Log("Missed slider.");
        game_controller.PlayerMissedBall();
      }
}
