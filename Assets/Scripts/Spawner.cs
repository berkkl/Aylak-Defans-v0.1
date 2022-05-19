using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private TMP_Text waveNumberText;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private int amount = 10;
    [SerializeField] float delayBtwSpawns;
    [SerializeField] float delayBtwWaves = 1f;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private EnemyHealthController _enemyHealthController0;
    private EnemyHealthController _enemyHealthController1;
    private int waveAmount = 1;
    public float DelayBtwSpawns { get; set; }
    private int enemySpawned;
    public int enemyRemaining;
    // Start is called before the first frame update
    void Start()
    {
        _enemyHealthController0 = enemyPrefabs[0].GetComponent<EnemyHealthController>();
        _enemyHealthController1 = enemyPrefabs[1].GetComponent<EnemyHealthController>();
        
        enemyRemaining = amount;
        DelayBtwSpawns = delayBtwSpawns;
        waveAmount = 1;
        _enemyHealthController0.maximumHealth = 30;
        _enemyHealthController1.maximumHealth = 20;
    }

    // Update is called once per frame
    void Update()
    {
        waveNumberText.text = "Wave #" + waveAmount;
        delayBtwSpawns -= Time.deltaTime;
        if (delayBtwSpawns <= 0)
        {
            delayBtwSpawns = DelayBtwSpawns;
            if (enemySpawned < amount)
            {
                enemySpawned++;
                SpawnEnemy();
            }

        }
    }

    private void SpawnEnemy()
    {
        float rnd = Random.RandomRange(0, 10);
        
        if (rnd <= 7) // 0 is the basic zombie (%70)
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[0], spawnPosition.transform.position, Quaternion.identity);
        }
        else // 1 is the runner zombie (%30)
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[1], spawnPosition.transform.position, Quaternion.identity);
        }

    }

    public IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        enemyRemaining = amount;
        enemySpawned = 0;
        delayBtwSpawns = 0f;
        
    }

    public void SpawnWave()
    {
        if(enemyRemaining < 1)
        {
            waveAmount++;
            _enemyHealthController0.maximumHealth += (_enemyHealthController0.maximumHealth * 50) / 100;
            _enemyHealthController1.maximumHealth += (_enemyHealthController1.maximumHealth * 50) / 100;
            amount += 2;
            ProjectileController.instance.minDamageDone += waveAmount;
            ProjectileController.instance.maxDamageDone += waveAmount;
            StartCoroutine(NextWave());
        }
    }
}

