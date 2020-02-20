using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static float health = 100.0f;
    public static int lives = 3;
    public Text healthText;
    public Text livesText;
    public static int killStreak = 0;
    public Transform spawnPoint;
    public static bool spawned = false;

    private void Update()
    {
        if (ChangeCharacter.choosingCharacter == false && spawned == false)
        {
            gameObject.transform.position = spawnPoint.position;
            spawned = true;
        }

        livesText.text = "LIVES: " + lives.ToString();
        healthText.text = "HEALTH: " + health.ToString();

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //move player out of area to choose character screen
        lives--;
        killStreak = 0;
        ChangeCharacter.choosingCharacter = true;
        spawned = false;
        health = 100.0f;
    }
}
