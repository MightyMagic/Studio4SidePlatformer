using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarItem : MonoBehaviour
{
    [SerializeField] int scoreToAdd;
    [SerializeField] AudioClip starCollected;

    GameManager gameManager;
    AudioSource source;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        source = gameManager.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameManager.AddScore(scoreToAdd);
            source.PlayOneShot(starCollected);
            gameManager.score.text = "Score: " + gameManager.scoreInt;
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        gameManager.starPresent = false;
    }
}
