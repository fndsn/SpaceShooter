using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public float startWait;
    public float spawnWait;
    public float waveWait;
    public int hazardCount;

    public float spawnCounter;

    public Text scoreText;
    int score;
    public GameObject gameOverPanel;
    bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnWaves());
        gameOverPanel.SetActive(false);
        UpdateText();
    }

    private void Update()
    {
        if (isGameOver)
        {
            return;
        }
        spawnCounter += Time.deltaTime;
        if (spawnCounter >= spawnWait)
        {
            GameObject hazard = hazards[Random.Range(0, hazards.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Instantiate(hazard, spawnPosition, Quaternion.identity);

            spawnCounter = 0;
        }
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateText();
    }

  

    void UpdateText()
    {
        scoreText.text = "Score : " + score;
    }

    IEnumerator SpawnWaves()
    {
        // oyuncu hazırlık süresi
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        isGameOver = true;
    }

    public void RestartButtonClick()
    {
        SceneManager.LoadScene("Scene1");
        //SceneManager.LoadScene(0);
    }
}
