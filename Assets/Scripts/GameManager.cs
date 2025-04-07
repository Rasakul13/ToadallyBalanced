using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    private AudioManager audioManager;
    private SpawnManager spawnManager;

    public UdpSocket client;

    public Animator animator;

    public GameObject player;
    public PlayerMovement movement;

    public VASDialogController vasDialogController;
    public Text gameOver;
    public Text levelCompletedText;
    public bool levelCompleted;
    public bool gameHasEnded;

    public CountdownController countdownController;
    public int countdown;
    public bool gameHasStarted;

    public TimerController timer;
    float timerValue;

    public ScoreController score;
    public Text scoreGoal; 
    public int scoreGoalValue;
    int scoreGoalMultiplier;

    public Text hpCounter;
    private int hp;

    private int difficulty;

    public int currentLevel;

    private int port;

    void Awake()
    {   
        audioManager = FindFirstObjectByType<AudioManager>();
        spawnManager = FindFirstObjectByType<SpawnManager>();

        port = PlayerPrefs.GetInt("port", 5555);

        countdown = (int)PlayerPrefs.GetFloat("countdown");
        countdownController.StartCountdown(countdown);
        
        timerValue = PlayerPrefs.GetFloat("time");
        timer.SetTimer(timerValue*30);
        
        difficulty = (int)PlayerPrefs.GetFloat("difficulty");
        hp = 5-difficulty;

        switch(difficulty)
        {
            case 0:
                scoreGoalMultiplier = 250;
                break;
            case 1:
                scoreGoalMultiplier = 300;
                break;
            case 2:
                scoreGoalMultiplier = 400;
                break;
        }

        currentLevel = SceneManager.GetActiveScene().buildIndex;

        scoreGoalValue = (int)timerValue * scoreGoalMultiplier;

        if(scoreGoalValue%100 == 50) 
        {
            scoreGoalValue -= 50;
        }

        scoreGoal.text = scoreGoalValue.ToString(); 

        
    }

    void Start()
    {   
        player = GameObject.FindWithTag("Player");

        gameHasEnded = false;
        levelCompleted = false;
        gameOver.GetComponent<Text>().enabled = false;
        levelCompletedText.GetComponent<Text>().enabled = false;
    }

    void Update()
    {}

    void FixedUpdate()
    {   
        hpCounter.text = hp.ToString(); 

        if(score.scoreValue == scoreGoalValue && gameHasEnded == false) {
            levelCompleted = true;
            GameEnd(false);
        }
    }

    public void BeginGame() 
    {
        Debug.Log("Start Game");
        
        client.CreateSocket(port);
        timer.StartTimer();
        spawnManager?.StartSpawn();

        gameHasStarted = true;


    }

    public void Restart()
    {   
        if(gameHasStarted) 
        {
            client.CloseSocket();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {   
        if(gameHasStarted) 
        {
            client.CloseSocket();
            GameEnd(true);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameEnd(bool quitButtonClicked)
    {   
        if(gameHasEnded == false) 
        {
            gameHasEnded = true;

            movement.enabled = false;
            animator.Play("DisappearingAnimation", 0, 3f);
            animator.SetBool("gameHasEnded", true);

            

            client.CloseSocket();

            if(levelCompleted)
            {
                Debug.Log("LEVEL ACCOMPLISHED");
                levelCompletedText.GetComponent<Text>().enabled = true;
                audioManager?.Play("LevelCompleted");
            }
            else
            {
                Debug.Log("GAME OVER");
                gameOver.GetComponent<Text>().enabled = true;
                audioManager?.Play("GameOver");

            }

            spawnManager?.DespawnFruit();

            if(quitButtonClicked == true)
            {
                StartCoroutine(HideGameElements(0.5f));
                StartCoroutine(DelayedShowDialog(2.0f));
            }
            else 
            {
                StartCoroutine(HideGameElements(1.0f));
                StartCoroutine(DelayedShowDialog(4.0f));
            }
        }
    }
    
    public void TakeDamage()
    {
        audioManager?.Play("PlayerDeath");
        if(hp > 1)
        {
            hp -= 1;
        }
        else
        {   
            hp = 0;
            GameEnd(false);
        }
    }



    private IEnumerator HideGameElements(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.SetActive(false);
        GameObject.FindWithTag("Feedback").SetActive(false);
    }

    private IEnumerator DelayedShowDialog(float delay)
    {
        yield return new WaitForSeconds(delay);
        vasDialogController.ShowDialog();
    }
}