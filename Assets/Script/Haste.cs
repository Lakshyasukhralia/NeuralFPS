using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haste : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {

        GameObject hit = collision.gameObject;
        PlayerController player = hit.GetComponent<PlayerController>();

        player.Haste();

        Destroy(gameObject);

    }
}
