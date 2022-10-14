using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour 
{
	List<MoleController> moles = new List<MoleController>();
	bool generate;
	// Define the count
	public AnimationCurve maxMoles;

	void Start () 
	{
		// Find all the moles
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Mole");
		foreach (GameObject go in gos) 
		{
			moles.Add (go.GetComponent<MoleController> ());
			// Hide the moles
			go.SetActive(false);
		}
		generate = false;
	}

	public void StartGenerate()
	{
		StartCoroutine ("Generate");
	}

	public void StopGenerate()
	{
		generate = false;
	}
		
	IEnumerator Generate()
	{
		generate = true;
		while (generate) 
		{
			// wait to generate next group of moles
			yield return new WaitForSeconds (1.0f);

			// generate moles
			int n = moles.Count;
			int maxNum = (int)maxMoles.Evaluate ( GameManager.time );
			for (int i = 0; i < maxNum; i++) 
			{
				// select mole to up
				moles [Random.Range (0, n)].Up ();								
				yield return new WaitForSeconds (0.3f);
			}
		}
	}
}
