using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
    public Rigidbody player;
    public Transform Gun;
    public bool onGround=true;
    public Camera cam;
    public float speed = 15.0f; 
	public static int action = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
            cam.enabled = false; 
            return;
		}



        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Gun.Rotate(Vector3.left * 2.5f, Space.Self);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Gun.Rotate(Vector3.right * 2.5f, Space.Self);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdFire();
        }

		if (action == 1 && onGround == true || Input.GetKeyDown(KeyCode.Space) && onGround == true)
        {
            Jump();
        }

    }

    void FixedUpdate()
    {
        /*Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 1))
            print("There is something in front of the object!");*/

        
        float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * speed;

        transform.Rotate (0,x,0);
		transform.Translate (0,0,z);

      
    }

    public void Haste()
    {
        if (!isServer)
        {
            return;
        }
        StartCoroutine(MyHaste());
    }

    IEnumerator MyHaste()
    {
        speed = 25.0f;
        yield return new WaitForSeconds(10.0f);
        speed = 15.0f;
    }


    private void OnCollisionEnter(Collision other)
    {
        GameObject hit = other.gameObject;
        Health health = hit.GetComponent<Health>();

        if (other.gameObject.CompareTag("Ground"))
                {
            onGround = true;
        }
    }

    public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}

   

    [Command]
	void CmdFire()
	{
		GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 45.0f;
        NetworkServer.Spawn(bullet);
		Destroy (bullet, 2);
	}
    


    void Jump()
    {
        player.velocity = new Vector3(0, 8, 0);
        onGround = false;
    }


}
