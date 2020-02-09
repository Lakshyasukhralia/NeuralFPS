using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {

    public GameObject hastePrefab;
    public Transform hasteSpawn;

   
    public override void OnStartServer()
    {
        GameObject haste = (GameObject)Instantiate(hastePrefab, hasteSpawn.position, hasteSpawn.rotation);
        NetworkServer.Spawn(haste);
    }
}
