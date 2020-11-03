using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public float parallaxSpeed = 0.5f;
	public RawImage background;
	public GameObject uiIdle;
	public GameObject greenFire;

	public enum GameState {Idle, Playing}
	public GameState gameState = GameState.Idle;

	public GameObject player;

	void Start()
	{
		greenFire.SetActive(false);
	}

	void Update()
	{
		if (gameState == GameState.Idle && Input.GetKeyDown("up"))
		{
			gameState = GameState.Playing;
			greenFire.SetActive(true);
			uiIdle.SetActive(false);
			player.SendMessage("UpdateState", "CorriendoMedio");
			player.SendMessage("UpdatePosicion", 1);
		}
		else if (gameState == GameState.Playing)
		{
			Parallax();
		}
	}

	void Parallax()
    {
		float finalSpeed = parallaxSpeed * Time.deltaTime;
		background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
	}
}
