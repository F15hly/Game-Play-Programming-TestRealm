using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatButton : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject levelManger;

    levelMangment levelMangment;

    GameObject fist;
    public Transform buttonsWall;
    public Transform buttonsWallout;

    public bool buttonin;
    public bool buttonout;

    public bool isPressed = false;

    private void Awake()
    {
        levelMangment = levelManger.GetComponent<levelMangment>();
        fist = GameObject.FindGameObjectWithTag("fist");
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider PlayerCollider = fist.GetComponent<Collider>();
        if (other == PlayerCollider)
        {
            if (levelMangment.isPunched)
            {
                isPressed = !isPressed;
                levelMangment.isPunched = false;
                buttonin = true;
                buttonout = false;
            }
        }
    }

    private void Update()
    {
        if(isPressed)
        {
            foreach (var item in platforms)
            {
                item.GetComponent<PlatformMovement>().speed = 0;
            }
        }
        else
        {
            foreach (var item in platforms)
            {
                item.GetComponent<PlatformMovement>().speed = item.GetComponent<PlatformMovement>().defaultSpeed;
            }
        }

        if(buttonin)
        {
            var step = Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, buttonsWall.position, step);
            StartCoroutine(Wait());
        }
        if(buttonout)
        {
            var step = Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, buttonsWallout.position, step);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        buttonout = true;
        buttonin = false;
    }    
}
