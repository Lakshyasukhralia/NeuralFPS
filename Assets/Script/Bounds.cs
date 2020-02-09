using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {

        GameObject hit = collision.gameObject;
        Health health = hit.GetComponent<Health>();

      
            health.Death();
        

    }
}
