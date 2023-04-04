using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static int scoreGoal = 0;
    private static float time = 0f;
    
    public static Tuple<int, float> GetLeveldata() 
    { 
        var tuple = Tuple.Create(scoreGoal, time);
        return tuple;
    }

    public void LoadLevel(int levelnumber) 
    {   
        switch(levelnumber)
        {
            case 1:
                scoreGoal = 1000;
                time = 90f; 
                break;
            case 2:
                scoreGoal = 1200;
                time = 90f; 
                break;
            case 3:
                scoreGoal = 1200;
                time = 60f; 
                break;
            case 4:
                scoreGoal = 1000;
                time = 90f; 
                break;
        }

        SceneManager.LoadScene(levelnumber);
    }
}