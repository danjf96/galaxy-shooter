using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //StartCoroutine(EnemySpawnRoutine());
        //StartCoroutine(PowerupSpawnRoutine());
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    //create a coroutine to spawn the Enemy every 5 seconds
    public IEnumerator EnemySpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            float randomX = Random.Range(-7.78f, 7.78f);
            Instantiate(enemyShipPrefab, new Vector3(randomX, 6.37f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public IEnumerator PowerupSpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            int randomPowerup = Random.Range(0, 3);
            float randomX = Random.Range(-7.78f, 7.78f);
            Instantiate(powerups[randomPowerup], new Vector3(randomX, 6.37f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

    }

}
