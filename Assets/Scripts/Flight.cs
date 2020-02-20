using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using System.Linq;
using Leap;
using Leap.Unity;

public class Flight : MonoBehaviour {

    [SerializeField]
    float speed = 50f;
    [SerializeField]
    float currentSpeed;
    public Text speedText;

    Controller controller;
    private float HandPalmPitch;
    private float HandPalmRoll;
    private float HandPosition;

    public Frame frame;
    public Hand CurrentHand;
    public List<Hand> hands = new List<Hand>();
    Hand firsthand;
    Hand SecondHand;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;

        if (frame.Hands.Count == 2)
        {
            firsthand = hands[0];
            SecondHand = hands[1];
        }

        if (firsthand.IsLeft)
        {
            HandPalmPitch = firsthand.PalmNormal.Pitch + 1.5f;
            HandPalmRoll = firsthand.PalmNormal.Roll;
            HandPosition = SecondHand.PalmPosition.z;
        }
        else if (SecondHand.IsLeft)
        {
            HandPalmPitch = SecondHand.PalmNormal.Pitch + 1.5f;
            HandPalmRoll = SecondHand.PalmNormal.Roll;
            HandPosition = firsthand.PalmPosition.z;
        }

        if (HandPosition < 30 && -30 < HandPosition)
        {
            speed = 60 + HandPosition;
        }
        else if (HandPosition > 20)
        {
            speed = 80;
        }
        else if (HandPosition < -20)
        {
            speed = 30;
        }

        if (HandPalmRoll > 1f)
        {
            HandPalmRoll = 1f;
        }
        if (HandPalmRoll < 0.1f && HandPalmRoll > -0.1f)
        {
            HandPalmRoll = 0;
        }
        if (HandPalmRoll < -1f)
        {
            HandPalmRoll = -1f;
        }

        if (ChangeCharacter.choosingCharacter == false && Pause.isPaused == false)
        {
            transform.position += transform.forward * Time.deltaTime * speed;

            transform.Rotate(-HandPalmPitch * 3, 0.0f, HandPalmRoll);

            speedText.text = "SPEED: " + ((int)speed).ToString();
        }      
    }


}
