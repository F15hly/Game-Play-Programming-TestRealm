using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject levelManger;
    levelMangment levelMangment;

    public GameObject door;
    GameObject fist;
    public Transform buttonsWall;

    private bool isPressed;
    private bool wasPressed;

    private void Awake()
    {
        levelMangment = levelManger.GetComponent<levelMangment>();
        fist = GameObject.FindGameObjectWithTag("fist");
        wasPressed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Collider PlayerCollider = fist.GetComponent<Collider>();
        if (other == PlayerCollider)
        {
            if (levelMangment.isPunched &! wasPressed)
            {
                isPressed = true;
                levelMangment.isPunched = false;
                wasPressed = true;
            }
        }
    }
    private void Update()
    {
        if (isPressed)
        {
            var step =  Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, buttonsWall.position, step);
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        door.GetComponent<doorOpen>().enabled = true;
        isPressed = false;
    }
}
