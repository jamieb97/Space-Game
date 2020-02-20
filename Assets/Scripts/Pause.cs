using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class Pause : MonoBehaviour {

    public Frame frame;
    public Hand CurrentHand;
    public List<Hand> hands = new List<Hand>();
    Hand firsthand;
    Hand SecondHand;
    int extendedFingers = 0;
    public static bool isPaused = false;
    Controller controller;
    public GameObject pauseMenu;


    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;


        if (frame.Hands.Count < 2 && Manager.startGame)
        {
            Paused();
        }
    }

    void Paused()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
        GetComponent<PauseMenu>().enabled = true;
    }
}
