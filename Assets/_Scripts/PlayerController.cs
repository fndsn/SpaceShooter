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
    public Transform[] shotSpawns;
    public float fireRate;
    float nextFire;
    private AudioSource playerShoot;
    public SimpleTouchAreaButton areaButton;
    public SimpleTouchPad TouchPad;
    private Quaternion calibrationQuaternion;


    // Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    playerShoot = GetComponent<AudioSource>();
    //}

    //private void Update()
    //{
    //    if (Input.GetButton("Fire1") && Time.time > nextFire)
    //    {
    //        nextFire = Time.time + fireRate;
    //        foreach (var shotSpawn in shotSpawns)
    //            Instantiate(bolt, shotSpawn.position, shotSpawn.rotation);
    //        playerShoot.Play();
    //    }
    //}
void Start()
    {
        CalibrateAccelerometer();
        rb = GetComponent<Rigidbody>();
        playerShoot = GetComponent<AudioSource>();
    }

    void Update()
    {
                if (areaButton.CanFire() && Time.time > nextFire)
        {      
                      nextFire = Time.time + fireRate;
                      foreach (var shotSpawn in shotSpawns)
                      Instantiate(bolt, shotSpawn.position, shotSpawn.rotation);
                      playerShoot.Play();
            

        }
    }

    //Used to calibrate the Iput.acceleration input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Vector3 accelerationRaw = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(accelerationRaw);
        //Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);
        //rb.velocity = movement * speed;

        Vector2 direction = TouchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);


        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.minX, boundary.maxX), rb.position.y,
            Mathf.Clamp(rb.position.z, boundary.minZ, boundary.maxZ));

        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);
    }


    
   
[System.Serializable]
    public class Boundary
    {
        public float minX, maxX, minZ, maxZ;
    }
}
