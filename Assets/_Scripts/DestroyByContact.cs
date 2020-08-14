using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosionEffect, playerExplosionEffect;
    public int scoreValue;
    GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("'GameController' bulunamadı.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            return;
        }
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        gameController.AddScore(scoreValue);



        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosionEffect, other.transform.position, Quaternion.identity);
            //gameController.GameOver();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
