using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; //TO_ADD
using UnityEngine.XR.ARSubsystems; //TO_ADD
public class GetTouch : MonoBehaviour
{
    public GameObject text;
    // These will store references to our other components.
    private ARRaycastManager raycastManager;
    private bool touched = true;
    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            if (touched)
            {
                text.SetActive(true);
                touched = false;
            }
            else
            {
                text.SetActive(false);
                touched = true;
            }
        }
    }
}
