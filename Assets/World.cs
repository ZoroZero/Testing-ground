﻿using UnityEngine;

public class World : SceneController
{

    public Transform player;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        if (prevScene == "RoomScene")
        {
            player.position = new Vector2(4.5f, 0.6f);
        }
    }

}
