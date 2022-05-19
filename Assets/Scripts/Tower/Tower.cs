using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float towerRange = 3f;
    public int Cost;
    public LayerMask enemyMarker;

    private float _checkCounter;
    public float checkTime = .25f; //enemy check interval(with .25, it check 4 times in a second)
    
    public EnemyController CurrentEnemyTarget { get; set; }
    
    public Collider2D[] colliderInRange;
    public List<EnemyController> enemiesInRange = new List<EnemyController>();

    [HideInInspector] public bool enemiesUpdated;
    
    private bool _gameStarted;


    //TODO: Add cost
    //TODO: Add rangeModel(Game Object)


    private void Start()
    {
        _gameStarted = true;
    }

    private void Update()
    {
        enemiesUpdated = false;
        _checkCounter -= Time.deltaTime;
        if (_checkCounter <= 0)
        {
            _checkCounter = checkTime;

            colliderInRange = Physics2D.OverlapCircleAll(transform.position, towerRange, enemyMarker);
            
            enemiesInRange.Clear();
            foreach (Collider2D col in colliderInRange)
            {
                if(col.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy))
                {
                    enemiesInRange.Add(col.GetComponent<EnemyController>());    
                }
            }
            enemiesUpdated = true;
        }
    }
}


