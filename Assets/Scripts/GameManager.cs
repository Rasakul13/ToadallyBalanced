using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public UdpSocket client;

    public SceneLoader sceneLoader;

    public Animator animator;

    public GameObject player;
    public PlayerMovement movement;

    public Text gameOver;
    public Text levelAccomplished;
    public bool gameHasEnded;

    public CountdownController countdownController;
    public int countdown;
    public bool gameHasStarted;

    public TimerController timer;
    float timerValue;

    public ScoreController score;
    public Text scoreGoal; 
    public int scoreGoalValue;

    public Text hpCounter;
    int hp;

    void Awake()
    {
        var tuple = SceneLoader.GetLeveldata();
        
        scoreGoalValue = tuple.Item1;
        scoreGoal.text = scoreGoalValue.ToString(); 

        timerValue = tuple.Item2;
        timer.SetTimer(timerValue);

        hp = 3;
    }

    void Start()
    {   
        countdown = (int)PlayerPrefs.GetFloat("countdown");
        countdownController.StartCountdown(countdown);
        
        player = GameObject.FindWithTag("Player");

        gameHasEnded = false;
        gameOver.GetComponent<Text>().enabled = false;
        levelAccomplished.GetComponent<Text>().enabled = false;
    }

    void Update()
    {}

    void FixedUpdate()
    {   
        hpCounter.text = hp.ToString(); 

        if(score.scoreValue == scoreGoalValue && gameHasEnded == false) {
            GameEnd(true);
        }
    }

    public void BeginGame() 
    {
        Debug.Log("Start Game");
        
        //client.CreateSocket(5555, "192.168.0.31");
        client.CreateSocket(5555);
        timer.StartTimer();
        FindObjectOfType<SpawnManager>().StartSpawn();

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
        }

        SceneManager.LoadScene(0);
    }

    public void GameEnd(bool accomplished)
    {   
        if(gameHasEnded == false) 
        {
            gameHasEnded = true;

            movement.enabled = false;
            animator.Play("DisappearingAnimation", 0, 3f);
            animator.SetBool("gameHasEnded", true);

            

            client.CloseSocket();

            if(accomplished == true)
            {
                Debug.Log("LEVEL ACCOMPLISHED");
                levelAccomplished.GetComponent<Text>().enabled = true;
                FindObjectOfType<AudioManager>().Play("LevelCompleted");
            }
            else
            {
                Debug.Log("GAME OVER");
                gameOver.GetComponent<Text>().enabled = true;
                FindObjectOfType<AudioManager>().Play("GameOver");

            }

            FindObjectOfType<SpawnManager>().DespawnFruit();
            
            StartCoroutine(WaitCoroutine(1.0f));
            
        }
    }
    
    public void TakeDamage()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
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

    private IEnumerator WaitCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.SetActive(false);
        GameObject.FindWithTag("Feedback").SetActive(false);
    } 
}