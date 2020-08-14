using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boundry"))
        {
            Destroy(gameObject);
        }
    }
}
