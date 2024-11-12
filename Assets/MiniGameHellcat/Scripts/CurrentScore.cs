using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentScore : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    // Start is called before the first frame update
    
    public void addScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}
