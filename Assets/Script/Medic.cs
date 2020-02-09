using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {

        GameObject hit = collision.gameObject;
        Health health = hit.GetComponent<Health>();

        health.Medical();

        Destroy(gameObject);

    }
}
