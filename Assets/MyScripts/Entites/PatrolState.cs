using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{

    public LayerMask playerMask;
    public Transform playerCheck;
    public Transform selfTransform;
    public float areaRadius;

    float timer;

    public float test;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            if(Physics.CheckSphere(playerCheck.position, areaRadius, playerMask))
            {
                gameObject.GetComponent<MonsterBehaviour>().enabled = true;
            }
            if(!Physics.CheckSphere(playerCheck.position, areaRadius, playerMask))
            {
               gameObject.GetComponent<MonsterBehaviour>().enabled = false;
            }
        }
    }
}
