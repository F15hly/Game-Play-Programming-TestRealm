using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public GameObject player;
    public static int coinCount = 0;
    public Text coinText;

    private bool up;
    private float timer = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            Destroy(gameObject);
            coinCount += 1;
            coinText.text = "" + coinCount;
        }
    }
    void Update()
    {
        
        if(up)
        {
            transform.position = transform.position + new Vector3(0, Time.deltaTime / 4, 0);
        }
        if(!up)
        {
            transform.position = transform.position + new Vector3(0, -Time.deltaTime / 4, 0);
        }

        timer += Time.deltaTime;
        if(timer <= 0.5)
        {
            up = true;
        }
        if (timer > 0.5)
        {
            up = false;
        }
        if(timer >= 1)
        {
            timer = 0;
        }
    }
}
