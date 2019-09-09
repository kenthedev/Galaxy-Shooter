using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject _EnemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameManager _gameManager;
	// Use this for initialization
	void Start () {
        //0 tripleshot, 1 speed, 2 shield
        //_powerups[]
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
	}

    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

	IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(_EnemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);

        }
    }
    IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
       
    }

    //create a coroutine to spawn the enemy every 5 seconds

	// Update is called once per frame
	void Update () {
		
	}
}
