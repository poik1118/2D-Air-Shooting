using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject[]     enemyObjs;
    public Transform[]      spawnPoints;

    public float            maxSpawnDelay;
    public float            curSpawnDelay;

    public GameObject       player;
    public Text             scoreText;
    public Image[]          hpImage;
    public Image[]          boomImage;
    public GameObject       gameOverSet;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();

            maxSpawnDelay = Random.Range(1f, 3f);
            curSpawnDelay = 0;
        }

        PlayerState playerLogic = player.GetComponent<PlayerState>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, 3);
        int randomPoint = Random.Range(0, 9);

        GameObject enemy = Instantiate(enemyObjs[randomEnemy], spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);

        Rigidbody2D rg = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

        if(randomPoint == 5 || randomPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rg.velocity = new Vector2(enemyLogic.speed*(-1), -1);
        }
        else if (randomPoint == 7 || randomPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rg.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {
            rg.velocity = new Vector2(0, enemyLogic.speed*(-1));
        }
    }

    public void UpdateHPIcon(int HP)
    {
        for (int index = 0; index < 3; index++)
        {
            hpImage[index].color = new Color(1, 1, 1, 0);
        }
        for (int index = 0; index < HP; index++)
        {
            hpImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateBoomIcon(int boom)
    {
        for (int index = 0; index < 2; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }
        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    public void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 10f;
        player.SetActive(true);

        PlayerState playerLogic = player.GetComponent<PlayerState>();
        playerLogic.isHit = false;
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("Ingame");
    }

}