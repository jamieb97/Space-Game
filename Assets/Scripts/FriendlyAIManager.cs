using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyAIManager : MonoBehaviour {

    public static int amountOfFriendlies = 8;
    public Text amountOfFriendiesText;

    public static int activeFriendlies = 0;
    private int friendliesSpawn;

    public static int AIDifficulty;

    public Transform[] spawnLocations;
    public GameObject[] spawnPrefab;
    public GameObject[] spawnClone;

    private float spawnFriendlyTime;

    private bool spawnFriendly;
    private int spawnLocation;

    // Update is called once per frame
    void Update()
    {
        amountOfFriendiesText.text = "BACKUP: " + amountOfFriendlies.ToString();

        if (spawnFriendly == true && spawnFriendlyTime < 0)
        {
            SpawnFriendlies();
        }
        spawnFriendlyTime -= Time.deltaTime;
        if (spawnFriendlyTime < 0 && Manager.startGame == true)
        {
            spawnFriendly = true;
        }
    }

    private void SpawnFriendlies()
    {
        friendliesSpawn = 3 - activeFriendlies;
        if (friendliesSpawn > 0)
        {
            do
            {
                spawnLocation = UnityEngine.Random.Range(0, 2);
                spawnClone[0] = Instantiate(spawnPrefab[0], spawnLocations[spawnLocation].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                friendliesSpawn--;
                activeFriendlies++;
            } while (friendliesSpawn > 0);
        }
        spawnFriendly = false;
        spawnFriendlyTime = 25;
    }
}
