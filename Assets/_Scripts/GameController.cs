using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public float startWait;
    public float spawnWait;
    public float waveWait;
    public float hazardCount;

   

    public Text scoreText;
    int score;
    public GameObject gameOverPanel;
    bool IsGameOver = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
        gameOverPanel.SetActive(false);
        UpdateText();
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
            for (float i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
    private void Update()
    {
        if (IsGameOver)
        {
            return;
        }
        hazardCount += Time.deltaTime;
        if (hazardCount >= spawnWait)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Instantiate(hazard, spawnPosition, Quaternion.identity);

            hazardCount = 0;
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        IsGameOver = true;
    }

    public void RestartButtonClick()
    {
        SceneManager.LoadScene("scene1");
        //SceneManager.LoadScene(0);
    }
}

