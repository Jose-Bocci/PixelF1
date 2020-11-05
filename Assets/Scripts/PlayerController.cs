using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject game;
    public GameObject enemyGenerator;
    public AudioClip giroClip;
    public AudioClip dieClip;

    private Animator animator;
    private AudioSource audioPlayer;
    int posicion = 1;
    public void UpdatePosicion(int posicion)
    {
        this.posicion = posicion;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        bool gamePlaying = game.GetComponent<GameController>().gameState == GameController.GameState.Playing;
        if (gamePlaying)
        {
            if (posicion == 1)
            {
                if (Input.GetKeyDown("up"))
                {
                    UpdateState("CarrilArriba");
                    UpdatePosicion(2);
                    audioPlayer.clip = giroClip;
                    audioPlayer.Play();
                }
                else if (Input.GetKeyDown("down"))
                {
                    UpdateState("CarrilAbajo");
                    UpdatePosicion(0);
                    audioPlayer.clip = giroClip;
                    audioPlayer.Play();
                }
            }
            if (posicion == 2)
            {
                if (Input.GetKeyDown("up"))
                {
                }
                else if (Input.GetKeyDown("down"))
                {
                    UpdateState("CarrilMedioArriba");
                    UpdatePosicion(1);
                    audioPlayer.clip = giroClip;
                    audioPlayer.Play();
                }
            }
            if (posicion == 0)
            {
                if (Input.GetKeyDown("up"))
                {
                    UpdateState("CarrilMedioAbajo");
                    UpdatePosicion(1);
                    audioPlayer.clip = giroClip;
                    audioPlayer.Play();
                }
                else if (Input.GetKeyDown("down"))
                {
                }
            }
        }
    }

 

    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            animator.Play(state);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            UpdateState("PlayerDie");
            game.GetComponent<GameController>().gameState = GameController.GameState.Ended;
            enemyGenerator.SendMessage("CancelGenerator", true);
            game.SendMessage("ResetTimeScale", 0.5f);

            game.GetComponent<AudioSource>().Stop();
            audioPlayer.clip = dieClip;
            audioPlayer.Play();
        }
        else if (other.gameObject.tag == "Point")
        {
            game.SendMessage("IncreasePoints");
        }
    }

    void GameReady()
    {
        game.GetComponent<GameController>().gameState = GameController.GameState.Ready;
    }
}
