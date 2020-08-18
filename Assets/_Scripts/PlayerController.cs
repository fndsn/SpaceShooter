using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject bolt;
    public Transform [] shotSpawns;
    public float fireRate;
    float nextFire;
    private AudioSource playerShoot;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerShoot = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            foreach (var shotSpawn in shotSpawns)
            Instantiate(bolt, shotSpawn.position, shotSpawn.rotation);
            playerShoot.Play();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.velocity = movement * speed;

        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.minX, boundary.maxX), rb.position.y, Mathf.Clamp(rb.position.z, boundary.minZ, boundary.maxZ));

        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);
    }
}

[System.Serializable]
public class Boundary
{
    public float minX, maxX, minZ, maxZ;
}
