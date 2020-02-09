using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maxHealth=100;
    private NetworkStartPosition[] spawnPoints;
    [SyncVar (hook="OnChangeHealth")] public int currentHealth=maxHealth;
	public RectTransform healthbar;

    void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }


    public void Takedamage(int amount)
	{
		if (!isServer) {
			return;
		}
		currentHealth -= amount;
        
        if (currentHealth <= 0) {
			currentHealth = maxHealth;
			RpcRespawn ();
		}
			
	}

    public void Death()
    {
        currentHealth = maxHealth;
        RpcRespawn();
        
    }

    public void Medical()
    {
        if (!isServer)
        {
            return;
        }
        currentHealth = maxHealth;
        healthbar.sizeDelta = new Vector2(200, healthbar.sizeDelta.y);
    }

    void OnChangeHealth(int health)
	{
		healthbar.sizeDelta = new Vector2 (health * 2, healthbar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer) {
            Vector3 spawnPoint = Vector3.zero;

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }
            transform.position = spawnPoint;
        }
	}
}
