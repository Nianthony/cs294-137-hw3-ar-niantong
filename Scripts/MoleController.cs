using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour,OnTouch3D
{

	private float moveSpeed = 0.1f;
	private float waitTime = 1.0f;

	private float TOP = 0f;
	private float BOTTOM = -0.1f;
	private float tmpTime = 0;

	enum State
	{
		UNDER_GROUND,
		UP,
		ON_GROUND,
		DOWN,
		HIT,
	}
	State state;

	public void OnTouch()
	{
		bool isHit = Hit();
		// if hit the mole, score, hummer and effect
		if (isHit)
		{
			Debug.Log("Hit U: " + gameObject.transform.position);
			/*GameObject hummer = GameObject.Find("Hummer");
			hummer.GetComponent<HummerController>().hitPosition = gameObject.transform.position;
			hummer.GetComponent<HummerController>().hit = true;*/
			HummerController.hitPosition = gameObject.transform.position;
			HummerController.hit = true;
		}
	}

	//connect with mole controller
	public void Up()
	{
		gameObject.SetActive(true);
		if (state == State.UNDER_GROUND)
			state = State.UP;
	}

	public bool Hit()
	{
		// if mole is under ground, never hit
		if (this.state == State.UNDER_GROUND)
		{
			return false;
		}

		// hit to bottom
		transform.position =
			new Vector3(transform.position.x, BOTTOM, transform.position.z);

		this.state = State.UNDER_GROUND;

		return true;
	}

	void Start()
	{
		// all set to the bottom
		state = State.UNDER_GROUND;
		BOTTOM = transform.position.y;
		TOP = BOTTOM + 0.075f;
	}

	void Update()
	{
		// show up
		if (state == State.UP)
		{
			transform.Translate(0, moveSpeed, 0);
			if (transform.position.y > TOP)
			{
				transform.position = new Vector3(transform.position.x, TOP, transform.position.z);
				state = State.ON_GROUND;
				tmpTime = 0;
			}
		}

		// based on time to determine go bottom or not
		else if (state == State.ON_GROUND)
		{
			tmpTime += Time.deltaTime;
			if (tmpTime > waitTime)
				state = State.DOWN;
		}

		// go bottom
		else if (state == State.DOWN)
		{
			transform.Translate(0, -moveSpeed, 0);
			if (transform.position.y < BOTTOM)
			{
				transform.position = new Vector3(transform.position.x, BOTTOM, transform.position.z);
				state = State.UNDER_GROUND;
				gameObject.SetActive(false);
			}
		}
	}

}
