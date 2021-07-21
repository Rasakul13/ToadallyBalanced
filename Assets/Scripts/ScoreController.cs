using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    public Text scoreText; 
    public int scoreValue;

    void Start()
    {
        scoreValue = 0;
    }

    void Update()
    {      
        scoreText.text = scoreValue.ToString();
    }

    public void Increase(int value)
    {
        scoreValue += value;
    }
}
