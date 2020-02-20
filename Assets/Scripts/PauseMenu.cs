using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Frame frame;
    public Hand CurrentHand;
    public List<Hand> hands = new List<Hand>();
    Hand firsthand;
    Hand SecondHand;

    Controller controller;

    public Text resumeText;
    public Text exitText;

    public GameObject pauseMenu;
    // Use this for initialization
    void Start () {
        resumeText.color = Color.grey;
        exitText.color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {


        if (Pause.isPaused)
        {
            controller = new Controller();
            Frame frame = controller.Frame();
            List<Hand> hands = frame.Hands;

            if (frame.Hands.Count == 2)
            {
                firsthand = hands[0];
                SecondHand = hands[1];
            }
            int leftFingers = 0;
            int rightFingers = 0;

            #region resumeHand
            if (firsthand.IsLeft)
            {
                for (int f = 0; f < firsthand.Fingers.Count; f++)
                {
                    Finger digit = firsthand.Fingers[f];
                    if (digit.IsExtended)
                        leftFingers++;
                }
                if (firsthand.PalmPosition.x < -200)
                {
                    resumeText.color = Color.white;
                    if (leftFingers == 0)
                        Resume();
                }
                else
                    resumeText.color = Color.grey;
            }
            else if (SecondHand.IsLeft)
            {
                for (int f = 0; f < SecondHand.Fingers.Count; f++)
                {
                    Finger digit = SecondHand.Fingers[f];
                    if (digit.IsExtended)
                        leftFingers++;
                }
                if (SecondHand.PalmPosition.x < -200)
                {
                    resumeText.color = Color.white;
                    if (leftFingers == 0)
                        Resume();
                }
                else
                    resumeText.color = Color.grey;
            }
            #endregion
            #region exitHand
            if (!firsthand.IsLeft)
            {
                for (int f = 0; f < firsthand.Fingers.Count; f++)
                {
                    Finger digit = firsthand.Fingers[f];
                    if (digit.IsExtended)
                        rightFingers++;
                }
                if (firsthand.PalmPosition.x > 200)
                {
                    exitText.color = Color.white;
                    if (rightFingers == 0)
                        Quit();
                }
                else
                    exitText.color = Color.grey;
            }
            else if (!SecondHand.IsLeft)
            {
                for (int f = 0; f < SecondHand.Fingers.Count; f++)
                {
                    Finger digit = SecondHand.Fingers[f];
                    if (digit.IsExtended)
                        rightFingers++;
                }
                if (SecondHand.PalmPosition.x > 200)
                {
                    exitText.color = Color.white;
                    if (rightFingers == 0)
                        Quit();
                }
                else
                    exitText.color = Color.grey;
            }
            #endregion
        }
    }

    void Quit()
    {
        Application.Quit();
    }

    void Resume()
    {
        pauseMenu.SetActive(false);
        Pause.isPaused = false;
        Time.timeScale = 1;
        GetComponent<PauseMenu>().enabled = false;
    }
}
