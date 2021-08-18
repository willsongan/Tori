﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;

public class RedSerum : MonoBehaviour
{
    [SerializeField] private float amplifier = 1.8f;
    private float defaultjumpVelocity;
    private PlayerController player;
    private Sprite defaultskin;
    [Header("Timer")]
    [SerializeField] private float timeRemaining = 10f;
    [SerializeField] private bool timeIsRunning = false;

    private void Awake()
    {
        defaultskin = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D othercollider)
    {
        player = othercollider.GetComponent<PlayerController>();
        player.redSerum = true;
        //enhance jump
        defaultjumpVelocity = player.jumpVelocity;
        player.jumpVelocity = player.jumpVelocity*amplifier;

        //despawn item
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.GetComponent<Collider2D>().enabled = false;
        timeIsRunning = true;
    }

    private void Update()
    {
        //run respawn timer after picked up
        if (timeIsRunning)
        {
            if (timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0f;
                timeIsRunning = false;
                timeRemaining = 10f;
                //reset jump
                player.jumpVelocity = defaultjumpVelocity;
                //respawn item
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultskin;
                gameObject.GetComponent<Collider2D>().enabled = true;
                player.redSerum = false;
            }
        }
    }

}
