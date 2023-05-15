using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour
{
    public Text healthPercent;
    public int HP = 100;
    public bool IFrames = false;
    public float ITimer = 1;

    private void Update()
    {
        if(HP >= 100)
        {
            HP = 100;
        }
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        healthPercent.text = HP + "%";
    }
}
