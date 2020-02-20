using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Linq;
using Leap;
using Leap.Unity;

public class ChangeCharacter : MonoBehaviour
{

    private GameObject[] characterList;

    public Transform characterChangePoint;
    public GameObject player;

    public GameObject inGameCamera;
    public GameObject changeCharCamera;
    public GameObject changeCharText;
    public GameObject inGameText;

    Controller controller;

    private int index;

    public static bool choosingCharacter = true;
    public Frame frame;
    public Hand CurrentHand;
    public List<Hand> hands = new List<Hand>();
    Hand firsthand;
    Hand SecondHand;


    bool hasChanged = false;
    // Use this for initialization
    void Start()
    {
        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[0])
        {
            characterList[0].SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (choosingCharacter == true)
        {
            player.transform.position = characterChangePoint.position;
            inGameCamera.SetActive(false);
            changeCharCamera.SetActive(true);
            inGameText.SetActive(false);
            changeCharText.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.A))
            ToggleLeft();
        if (Input.GetKeyDown(KeyCode.D))
            ToggleRight();
        if (Input.GetKeyDown(KeyCode.Space))
            Confirm();


        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;

        int extendedFingers = 0;

        if (frame.Hands.Count == 2)
        {
            firsthand = hands[0];
            SecondHand = hands[1];
        }

        if (firsthand.IsLeft)
        {
            if (firsthand.PalmNormal.Roll > 1 && hasChanged == false)
            {
                hasChanged = true;
                ToggleLeft();
            }
            if (SecondHand.PalmNormal.Roll < -1 && hasChanged == false)
            {
                hasChanged = true;
                ToggleRight();
            }

            if (firsthand.PalmNormal.Roll < 1 && SecondHand.PalmNormal.Roll > -1)
            {
                hasChanged = false;
            }

            for (int f = 0; f < SecondHand.Fingers.Count; f++)
            {
                Finger digit = SecondHand.Fingers[f];
                if (digit.IsExtended)
                    extendedFingers++;
            }
            for (int f = 0; f < firsthand.Fingers.Count; f++)
            {
                Finger digit = firsthand.Fingers[f];
                if (digit.IsExtended)
                    extendedFingers++;
            }
        }
        if (SecondHand.IsLeft)
        {
            if (firsthand.PalmNormal.Roll < -1 && hasChanged == false)
            {
                hasChanged = true;
                ToggleRight();
            }
            if (SecondHand.PalmNormal.Roll > 1 && hasChanged == false)
            {
                hasChanged = true;
                ToggleLeft();
            }

            if (firsthand.PalmNormal.Roll > -1 && SecondHand.PalmNormal.Roll < 1)
            {
                hasChanged = false;
            }

            for (int f = 0; f < SecondHand.Fingers.Count; f++)
            {
                Finger digit = SecondHand.Fingers[f];
                if (digit.IsExtended)
                    extendedFingers++;
            }
            for (int f = 0; f < firsthand.Fingers.Count; f++)
            {
                Finger digit = firsthand.Fingers[f];
                if (digit.IsExtended)
                    extendedFingers++;
            }
        }

        if (extendedFingers == 0)
        {
            Confirm();
        }
    }


    private void ToggleLeft()
    {
        if (choosingCharacter == true)
        {
            characterList[index].SetActive(false);

            index--;
            if (index < 0)
            {
                index = characterList.Length - 1;
            }

            characterList[index].SetActive(true);
        }
    }

    private void ToggleRight()
    {
        if (choosingCharacter == true)
        {
            characterList[index].SetActive(false);

            index++;
            if (index == characterList.Length)
            {
                index = 0;
            }

            characterList[index].SetActive(true);
        }
    }

    private void Confirm()
    {
        if (choosingCharacter == true)
        {
            choosingCharacter = false;
            inGameCamera.SetActive(true);
            changeCharCamera.SetActive(false);
            Manager.startGame = true;
            inGameText.SetActive(true);
            changeCharText.SetActive(false);
        }
    }
}
