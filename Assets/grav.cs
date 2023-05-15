using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grav : MonoBehaviour
{
     Rigidbody rb;

    private void FixedUpdate()
    {

        rb.useGravity = false;
        
    }
}
