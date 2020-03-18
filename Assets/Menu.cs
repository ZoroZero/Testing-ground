﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    // Player
    private PlayerMovement playerMovement;

    private Player player;

    // Pause menu UI
    public GameObject paused_UI;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }


    //Open paused menu
    public void openMenu()
    {
        playerMovement.enabled = false;
        paused_UI.SetActive(true);
    }

    //Close paused menu
    public void closeMenu()
    {
        playerMovement.enabled = true;
        paused_UI.SetActive(false);
    }


    // Save player position
    public void savePlayerData()
    {
        SaveSystem.savePlayer(player);
    }

    // Load player position
    public void loadPlayerData()
    {
        PlayerData data = SaveSystem.loadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        player.transform.position = position;
    }
}
