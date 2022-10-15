# HW3 Part 1: A Real AR Game, with Image Tracking

In this homework, I build a simple AR Game, Whack a Mole. Here is the reference, features of my game, and google drive link for my unity package and introduction video.  

## Reference

https://assetstore.unity.com/packages/templates/whack-a-mole-82155

I used the same prefabs (moles, trees, and stones), font, audio, and part of the animation in the previous version game to set up my unity scene. 

## Link

Here is my demo video

https://drive.google.com/file/d/1hAv4KVm8R9pzj6jfY7BMVK_JilvQ6gq4/view?usp=sharing

## Game changes

To make the game more suit for AR playing, I delete the Camera Follower script and Camera Shaker script. Because when you’re holding your phone, you won’t be able to hold it still, it will shake. You don’t have the need to add a shake effect in your game, which may make people dizzy. And I changed the detect function script for the previous OnTouch() function 
and the Ray cast manager doesn’t work in the AR environment. I used the collider and new ray castor to judge whether the hammer hit the mole or not. 

```C++
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARFollowTarget : MonoBehaviour
{
    // follow parameter 
    public float distanceAway = 10;           
    public float distanceUp = 2;             
    public float smooth = 3; 

    public GameObject gameBoard;
    Transform follow;

    // store the original position
    private Vector3 targetPosition, originalPosition;
    private Quaternion originalRotation;

    public bool placed = true;

    void Start()
    {
        follow = gameBoard.transform;
        originalPosition = this.transform.localPosition;
        originalRotation = this.transform.localRotation;
    }

    void Update()
    {
        // follow the board
        if (gameBoard.active && placed)
        {
            targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
            transform.LookAt(follow);
        }      
        // back to the original position
        else if (gameBoard.active && !placed)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * smooth);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * smooth);
        }
    }

    public void moveBoard( )
    {
        placed = false;

    }
}

```
![overview.png](https://github.com/Nianthony/cs294-137-hw3-ar-niantong/blob/main/AR%20Documentation/overview.png)

For the image tracking part, I tracked a real green cloth, where the game board will be generated when detecting the cloth. The android camera was not so precise, I took 4 photos of the same cloth in different lighting and environment to make sure it can recognize my cloth. 

![reference.png](https://github.com/Nianthony/cs294-137-hw3-ar-niantong/blob/main/AR%20Documentation/reference.png)
