using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour {

	Vector3 defaultPos;
	public float MAGNITUDE = 0.1f;

	public void Shake()
	{		
		StartCoroutine (Shake_Resilience());
	}

	IEnumerator Shake_Resilience()
	{
		for (int i = 0; i <= 360; i += 45) 
		{
			transform.position = new Vector3 (defaultPos.x, defaultPos.y + MAGNITUDE*Mathf.Sin (i * Mathf.Deg2Rad), defaultPos.z);
			yield return null;
		}
	}

	public void defaultPosition()
    {
		defaultPos = transform.position;
	}
}
