using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	enum State
	{
		START,
		PLAY,
		GAMEOVER,
	}

	public static float time;
	public float timeLimit = 30;
	const float waitTime = 2;

	MoleManager moleManager;
	Text remainTime;
	AudioSource audio;

	State state;
	float timer;
	bool startgame = false;
	public bool reload = false;
	public GameObject gameover,vanish1,vanish2;

	void Start()
	{
		Application.targetFrameRate = 60;
		state = State.START;
		timer = 0;
		moleManager = GameObject.Find("GameManager").GetComponent<MoleManager>();

		// time change
		remainTime = GameObject.Find("Timer").GetComponent<Text>();
		audio = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (state == State.START && startgame)
		{
			// vanish button
			vanish1.SetActive(false);
			vanish2.SetActive(false);
			state = State.PLAY;
				// start to generate moles
				moleManager.StartGenerate();
				audio.Play();
				audio.loop = true;
		}

		else if (state == State.PLAY)
		{
			timer += Time.deltaTime;
			if (timer > timeLimit)
			{
				state = State.GAMEOVER;
				startgame = false;
				// show gameover label
				gameover.SetActive(true);
				// stop to generate moles
				moleManager.StopGenerate();
				// reset timer
				timer = 0;
				// stop audio
				audio.loop = false;
			}
			remainTime.text = "Time: " + ((int)(timeLimit - timer)).ToString("D2");
		}

        else if (state == State.GAMEOVER)
        {
            timer += Time.deltaTime;
			// reboot the game
            if ((timer > waitTime) && reload)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            remainTime.text = "";
        }
    }

	// For OnClick Button
	public void startGame()
	{
		startgame = true;
	}
	public void reloadGame()
    {
		reload = true;
    }
}


