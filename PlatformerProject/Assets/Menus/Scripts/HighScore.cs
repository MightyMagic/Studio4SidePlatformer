using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highscoreText;
    // Start is called before the first frame update
    void Start()
    {
        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Highscore is " + highscore;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
