﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private Vector2 player_Position;

    public GameObject player;

    private float walk_counter = 0f;

    private float walk_Rate = 20f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_Position = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.GetComponent<Player>().move.sqrMagnitude > 0)
            {
                walk_counter += 1*Time.deltaTime;
            }
            if (walk_counter > walk_Rate && player_Position.sqrMagnitude != collision.transform.position.sqrMagnitude)
            {
                StartCoroutine(enterBattle());
                walk_counter = 0;
            }
        }
    }
    IEnumerator enterBattle()
    {
        player.GetComponent<Player>().move = Vector2.zero;
        player.GetComponent<Player>().enabled = false;
        Debug.Log("Encounter");
        player_Position = player.transform.position;
        yield return new WaitForSeconds(0.2f);    
        //FindObjectOfType<SwitchCamera>().cameraPositonM();
        
    }
}