using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static int scoreGoal = 0;
    private static int hp = 0;
    
    public static Tuple<int, int> GetLeveldata() 
    { 
        var tuple = Tuple.Create(scoreGoal, hp);
        return tuple;
    }

    public void LoadLevel(int levelnumber) 
    {   
        switch(levelnumber)
        {
            case 1:
                scoreGoal = 1000;
                hp = 5;
                break;
            case 2:
                scoreGoal = 1200;
                hp = 4; 
                break;
            case 3:
                scoreGoal = 1200;
                hp = 3; 
                break;
            case 4:
                scoreGoal = 1000;
                hp = 3; 
                break;
        }

        SceneManager.LoadScene(levelnumber);
    }
}