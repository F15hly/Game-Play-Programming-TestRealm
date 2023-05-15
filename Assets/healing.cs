using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healing : MonoBehaviour
{
    public GameObject player;
    playerStats playerStats;
    private PlayerMovement play;

    public bool heal;
    public float timer;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        play = player.GetComponent<PlayerMovement>();
        playerStats = player.GetComponent<playerStats>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            heal = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            heal = false;
        }
    }

    private void Update()
    {
        if(heal)
        {
            timer += Time.deltaTime;
            if(timer >= 1)
            {
                playerStats.HP += 10;
                timer = 0;
            }
            play.helling.Play();
        }
        else
        {
            play.helling.Stop();
        }
    }
}
