using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIManager : MonoBehaviour {

    public static int amountOfEnemies = 20;
    public Text amountOfEnemiesText;

    public static int activeEnemies = 0;
    private int enemiesSpawn;

    public static int AIDifficulty;

    public Transform[] spawnLocations;
    public GameObject[] spawnPrefab;
    public GameObject[] spawnClone;

    private float spawnEnemyTime;
    private bool spawnEnemy;
    private int spawnLocation;

	
	// Update is called once per frame
	void Update ()
    {
        amountOfEnemiesText.text = "ENEMIES: " + amountOfEnemies.ToString();
		if(Player.killStreak <= 5)
        {
            AIDifficulty = 1;
        }
        else if (Player.killStreak <= 10 && Player.killStreak > 5)
        {
            AIDifficulty = 2;
        }
        else if (Player.killStreak <= 15 && Player.killStreak > 10)
        {
            AIDifficulty = 3;
        }
        else if (Player.killStreak > 15)
        {
            AIDifficulty = 4;
        }

        if(spawnEnemy == true && spawnEnemyTime < 0)
        {
            SpawnEnemies();
        }
        spawnEnemyTime -= Time.deltaTime;
        if(spawnEnemyTime < 0 && Manager.startGame == true)
        {
            spawnEnemy = true;
        }
    }

    private void SpawnEnemies()
    {
        enemiesSpawn = 7 - activeEnemies;
        if (enemiesSpawn > 0)
        {
            do
            {
                spawnLocation = UnityEngine.Random.Range(0, 9);
                spawnClone[0] = Instantiate(spawnPrefab[0], spawnLocations[spawnLocation].transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
                enemiesSpawn--;
                activeEnemies++;
            } while (enemiesSpawn > 0);
        }
        spawnEnemy = false;
        spawnEnemyTime = 25;
    }
}
