using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Runtime;

public class GameController : MonoBehaviour
{
	public float parallaxSpeed = 0.5f;
	public RawImage background;
	public GameObject uiIdle;
	public GameObject uiScore;
	public GameObject greenFire;
	public Text pointsText;
	public Text recordText;

	public enum GameState {Idle, Playing, Ended, Ready}
	public GameState gameState = GameState.Idle;

	public GameObject player;
	public GameObject enemyGenerator;

	public float scaleTime = 3f;
	public float scaleInc = .10f;
	private int points = 0;

	private AudioSource musicPlayer;

	void Start()
	{
		uiScore.SetActive(false);
		greenFire.SetActive(false);
		musicPlayer = GetComponent<AudioSource>();
		recordText.text = "Mejor: " + GetMaxScore().ToString();
		musicPlayer.Play();
	}

	void Update()
	{
		if (gameState == GameState.Idle && Input.GetKeyDown("up"))
		{
			gameState = GameState.Playing;
			greenFire.SetActive(true);
			uiIdle.SetActive(false);
			uiScore.SetActive(true);
			player.SendMessage("UpdateState", "CorriendoMedio");
			player.SendMessage("UpdatePosicion", 1);
			enemyGenerator.SendMessage("StartGenerator");
			InvokeRepeating("GameTimeScale", scaleTime, scaleTime);
		}
		else if (gameState == GameState.Playing)
		{
			Parallax();
		}
		else if (gameState == GameState.Ready)
		{
			uiIdle.SetActive(true);
			if (Input.GetKeyDown("up"))
            {
				RestartGame();
            }
			if (Input.GetKeyDown("down"))
            {
				Application.Quit();
            }
		}
		if (gameState == GameState.Idle && Input.GetKeyDown("down"))
        {
			Application.Quit();
        }
	}

	void Parallax()
    {
		float finalSpeed = parallaxSpeed * Time.deltaTime;
		background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
	}

	public void RestartGame()
    {
		ResetTimeScale();
		SceneManager.LoadScene("SampleScene");
    }

	void GameTimeScale()
    {
		Time.timeScale += scaleInc;
		UnityEngine.Debug.Log("Velocidad incrementada" + Time.timeScale.ToString());
	}

	public void ResetTimeScale(float newTimeScale = 1f)
    {
		CancelInvoke("GameTimeScale");
		Time.timeScale = newTimeScale;
		UnityEngine.Debug.Log("Velocidad reestablecido" + Time.timeScale.ToString());

	}

	public void IncreasePoints()
    {
		pointsText.text = (++points).ToString();
		if (points >= GetMaxScore())
        {
			recordText.text = "Mejor: " + points.ToString();
			SaveScore(points);
        }
    }

	public int GetMaxScore()
    {
		return PlayerPrefs.GetInt("Puntaje máximo: ", 0);
    }

	public void SaveScore(int currentPoints)
    {
		PlayerPrefs.SetInt("Puntaje máximo: ", currentPoints);
    }
}
