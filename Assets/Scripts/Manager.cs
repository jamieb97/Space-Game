using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    [SerializeField]
    string level;
    
    public static bool startGame = false;

    public Text endGameText;
    public Text timerText;

    private float timer = 0;

    private float gameTimer = 300;

    public GameObject playerCam;
    public GameObject endGameCamera;
    public GameObject inGameText;
    public GameObject endGame;
    // Update is called once per frame

    private void Start()
    {
        startGame = false;
    }

    void Update ()
    {
        if (startGame == true )
        {
            gameTimer -= Time.deltaTime;
            timerText.text = ((int)gameTimer).ToString();
            inGameText.SetActive(true);
        }
		
        if(Player.lives <= 0 || gameTimer <= 0)
        {
            timer += Time.deltaTime;
            playerCam.SetActive(false);
            endGameCamera.SetActive(true);
            inGameText.SetActive(false);
            endGame.SetActive(true);
            endGameText.text = "YOU LOSE";
            if (timer >= 10)
            {
                Application.Quit();
            }
        }
        if(EnemyAIManager.amountOfEnemies <= 0)
        {
            timer += Time.deltaTime;
            playerCam.SetActive(false);
            endGameCamera.SetActive(true);
            inGameText.SetActive(false);
            endGame.SetActive(true);
            endGameText.text = "YOU WIN";
            if (timer >= 10)
            {
                Application.Quit();
            }
        }
	}
}
