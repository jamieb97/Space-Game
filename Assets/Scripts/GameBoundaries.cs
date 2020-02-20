using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoundaries : MonoBehaviour {

    
    private float warningTimer = 7.0f;

    private bool outOfArea = false;

    public Text warningText;

    private void Update()
    {
        if(outOfArea == true)
        {
            warningTimer -= Time.deltaTime;
            warningText.text = "WARNING: RETURN TO AREA IN " + ((int)warningTimer).ToString() + " SECONDS";
        }
        if(warningTimer <= 0)
        {
            Player.lives--;
            Player.killStreak = 0;
            ChangeCharacter.choosingCharacter = true;
            Player.spawned = false;
            warningTimer = 7.0f;
            Player.health = 100f;
            warningText.text = "";
            outOfArea = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Manager.startGame == true)
        {

            outOfArea = false;
            warningText.text = "";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && Manager.startGame == true)
        { 
            outOfArea = true;
            warningTimer = 7.0f;
        }
    }
}
