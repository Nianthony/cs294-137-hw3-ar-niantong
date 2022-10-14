using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerController : MonoBehaviour
{

	public GameObject particle;
	public static Vector3 hitPosition;
	public AudioClip HIT;
	public static bool hit = false,started=false;

	AudioSource audio;

	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	public IEnumerator Hit(Vector3 target)
	{
		// Hummer Down		
		transform.position = new Vector3(target.x, -0.6f, target.z);

		// Effect
		Instantiate(particle, target, Quaternion.identity);
		Camera.main.GetComponent<CameraShaker>().Shake();
		audio.PlayOneShot(HIT);
		yield return new WaitForSeconds(0.2f);

		// Hummer Up
		for (int i = 0; i < 6; i++)
		{
			transform.Translate(0, 0.05f, 0);
			yield return null;
		}
	}

	void Update()
	{
		if (hit && started)
		{
			Debug.Log(Input.mousePosition);
			StartCoroutine(Hit(hitPosition));
			hit = false;
			ScoreManager.score += 1;
		}
	}

	public void Started()
	{
		started = true;
	}
}
