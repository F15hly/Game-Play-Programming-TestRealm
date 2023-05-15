using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    InputManager inputManager;

    public Transform cutSceneCamTransform;
    public Transform camOrigin;
    public Transform door;
    public Transform playerSpot;

    public GameObject button;
    public GameObject thingThatOpens;
    public GameObject player;
    public GameObject mainCam;
    public GameObject cutSceneCam;

    public ParticleSystem confetti;

    private float speed = 0f;
    public bool zoomIn = false;
    public bool zoomOut = false;
    public bool inCutScene = false;

    private void Awake()
    {
        zoomIn = true;
        inCutScene = true;
        inputManager = player.GetComponent<InputManager>();
    }
    void Update()
    {
        player.transform.position = playerSpot.position;
        if (zoomIn)
        {
            inputManager.enabled = false;
            mainCam.SetActive(false);
            cutSceneCam.SetActive(true);

            cutSceneCamTransform.LookAt(door);
            speed = 10f;
            var step = speed * Time.deltaTime;
            cutSceneCamTransform.position = Vector3.MoveTowards(cutSceneCamTransform.position, door.position, step);
            if (Vector3.Distance(cutSceneCamTransform.position, door.position) < 15)
            {
                zoomIn = false;
                StartCoroutine(Cutscene());  
            }
        }
        if(zoomOut)
        {
            speed = 10f;
            var step = speed * Time.deltaTime;
            cutSceneCamTransform.position = Vector3.MoveTowards(cutSceneCamTransform.position, camOrigin.position, step);
            if (Vector3.Distance(cutSceneCamTransform.position,camOrigin.position) < 1)
            {
                zoomIn = false;
                zoomOut = false;
                inCutScene = false;
            }
        }
        if(!zoomIn &! zoomOut &! inCutScene)
        {
            cutSceneCam.SetActive(false);
            mainCam.SetActive(true);
            inputManager.enabled = true;
            gameObject.GetComponent<doorOpen>().enabled = false;
        }
    }
    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1);
        GetComponent<AudioSource>().pitch = 1f;
        GetComponent<AudioSource>().Play();
        Destroy(thingThatOpens);
        yield return new WaitForSeconds(2);
        confetti.GetComponent<AudioSource>().pitch = 1f;
        confetti.GetComponent<AudioSource>().Play();
        confetti.Play();
        yield return new WaitForSeconds(3);
        zoomOut = true;
        button.GetComponent<Button>().enabled = true;
    }
}
