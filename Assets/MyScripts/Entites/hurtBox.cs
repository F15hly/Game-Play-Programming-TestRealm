using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtBox : MonoBehaviour
{
    playerStats playerStats;
    public GameObject player;
    public int damage;

    private void Awake()
    {
        playerStats = player.GetComponent<playerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<Collider>() && !playerStats.IFrames)
        {
            playerStats.IFrames = true;
            playerStats.HP -= damage;
        }
    }

    private void Update()
    {
        if(playerStats.IFrames)
        {
            playerStats.ITimer -= Time.deltaTime;
            if(playerStats.ITimer <= 0)
            {
                {
                    playerStats.IFrames = false;
                    playerStats.ITimer = 1;
                }
            }
        }
    }
}
