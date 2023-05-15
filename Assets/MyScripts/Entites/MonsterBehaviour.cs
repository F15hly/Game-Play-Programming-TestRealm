using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    public GameObject levelManger;
    levelMangment levelMangment;
    MonsterBehaviour monsterBehaviour;

    MeshRenderer meshRenderer;
    Color startColour, damageColour = Color.red;

    Rigidbody rb;
    public GameObject damageBox;

    [SerializeField] public float rollSpeed = 12;
    private bool isMoving;

    public float monsterHP = 2;
    public bool isPunching;
    public bool knockback = false;

    public GameObject player;
    public Transform[] childTransform;
    public GameObject[] child;
    private bool parentDestroyed;
    public float scaleSize;

    GameObject fist;
    public GameObject sword;

    public float rotX;
    public float rotY;
    public float rotZ;

    public bool setOriginalRot = false;

    public Transform coin;

    public float test;
    private void Awake()
    {
        levelMangment = levelManger.GetComponent<levelMangment>();
        meshRenderer = GetComponent<MeshRenderer>();
        startColour = meshRenderer.material.color;
        damageColour.a = 0.5f;
        fist = GameObject.FindGameObjectWithTag("fist");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == fist.GetComponent<Collider>())
        {
            if (isPunching)
            {
                meshRenderer.material.color = damageColour;
                levelMangment.isPunched = false;
                monsterHP -= 1;
                isPunching = false;
                StartCoroutine(Hurtin());
            }
        }
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis)
    {
        isMoving = true;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(0.65f);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        if (!knockback)
        {
            for (int i = 0; i < (90 / rollSpeed); i++)
            {
                transform.RotateAround(anchor, axis, rollSpeed);
                if(knockback)
                {
                    break;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        isMoving = false;
    }
    private void Move()
    {
        if (isMoving) return;
        float distanceX;
        float distanceY;
        distanceX = (player.transform.position.x - transform.position.x) * (player.transform.position.x - transform.position.x);
        distanceY = (player.transform.position.z - transform.position.z) * (player.transform.position.z - transform.position.z);



        void Assemble (Vector3 dir)
        {
            var anchor = transform.localPosition + (Vector3.down + dir) * scaleSize;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis));
        }

        if (player.transform.position.x < transform.position.x && distanceX > distanceY)
        {
            Assemble(Vector3.left);
        }
        if (player.transform.position.x > transform.position.x && distanceX > distanceY)
        {
            Assemble(Vector3.right);
        }
        if (player.transform.position.z < transform.position.z && distanceY > distanceX)
        {
            Assemble(Vector3.back);
        }
        if (player.transform.position.z > transform.position.z && distanceY > distanceX)
        {
            Assemble(Vector3.forward);
        }

    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(0);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        setOriginalRot = false;
        parentDestroyed = false;
    }
    IEnumerator Hurtin()
    {
        knockback = true;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(5, fist.transform.position, 5f, 2F, ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.5f);
        meshRenderer.material.color = startColour;
        yield return new WaitForSeconds(4.5f);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        knockback = false;

    }
    private void Death()
    {
        if (monsterHP <= 0)
        {
            foreach (Transform item in childTransform)
            {
                item.SetParent(null);
                item.gameObject.AddComponent<Rigidbody>();
                item.gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
                item.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                item.gameObject.GetComponent<Rigidbody>().mass = 500;
                item.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                item.gameObject.GetComponent<PatrolState>().enabled = true;
                item.gameObject.GetComponent<MonsterBehaviour>().parentDestroyed = true;
                item.gameObject.GetComponent<Rigidbody>().AddExplosionForce(5f, gameObject.transform.position, 5f, 2f, ForceMode.VelocityChange);
                MonsterBehaviour monsterBehaviour = item.gameObject.GetComponent<MonsterBehaviour>();
                monsterBehaviour.setOriginalRot = true;
            }
            if(coin != null)
            {
                coin.gameObject.SetActive(true);
                coin.SetParent(null);
            }
            if (coin == null) ;
            {
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Death();
    }
    void RotationCall()
    {
        if (transform.rotation.eulerAngles.y <= 45 && transform.rotation.eulerAngles.y >= 360 - 45)
        {
            rotY = 0;
        }
        if (transform.rotation.eulerAngles.z <= 45 && transform.rotation.eulerAngles.z >= 360 - 45)
        {
            rotZ = 0;
        }
        if (transform.rotation.eulerAngles.x <= 45 && transform.rotation.eulerAngles.x >= 360 - 45)
        {
            rotX = 0;
        }

        if (transform.rotation.eulerAngles.y > 45 && transform.rotation.eulerAngles.y < 90 + 45)
        {
            rotY = 90;
        }
        if (transform.rotation.eulerAngles.z > 45 && transform.rotation.eulerAngles.z < 90 + 45)
        {
            rotZ = 90;
        }
        if (transform.rotation.eulerAngles.x > 45 && transform.rotation.eulerAngles.x < 90 + 45)
        {
            rotX = 90;
        }

        if (transform.rotation.eulerAngles.y >= 90 + 45 && transform.rotation.eulerAngles.y <= 180 + 45)
        {                                                        
            rotY = 180;                                          
        }                                                        
        if (transform.rotation.eulerAngles.z >= 90 + 45 && transform.rotation.eulerAngles.z <= 180 + 45)
        {
            rotZ = 180;
        }
        if (transform.rotation.eulerAngles.x >= 90 + 45 && transform.rotation.eulerAngles.x <= 180 + 45)
        {
            rotX = 180;
        }

        if (transform.rotation.eulerAngles.y > 180 + 45 && transform.rotation.eulerAngles.y < 270 + 45)
        {
            rotY = 270;
        }
        if (transform.rotation.eulerAngles.z > 180 + 45 && transform.rotation.eulerAngles.z < 270 + 45)
        {
            rotZ = 270;
        }
        if (transform.rotation.eulerAngles.x > 180 + 45 && transform.rotation.eulerAngles.x < 270 + 45)
        {
            rotX = 270;
        }

        Quaternion baseRot = Quaternion.Euler(rotX, rotY, rotZ);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, baseRot, 50 * Time.deltaTime);
    }
    private void Update()
    {
        test = transform.rotation.eulerAngles.x;
        Move();
        if(knockback)
        {
            RotationCall();
        }
        isPunching = levelMangment.isPunched;
        if (parentDestroyed)
        {
            damageBox.GetComponent<hurtBox>().enabled = true;
            setOriginalRot = true;
            StartCoroutine(waiting());
        }
        if(setOriginalRot)
        {
            RotationCall();
        }
        if(isPunching)
        {
            StopCoroutine(waiting());
        }
    }
}
