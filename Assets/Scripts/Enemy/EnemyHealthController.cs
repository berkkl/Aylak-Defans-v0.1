using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{

    //[SerializeField] private GameObject healthBarPrefab;
    //[SerializeField] private Transform barPosition;
    
    public AudioSource enemyDieSound;
    [SerializeField] private Spawner spawner;
    [SerializeField] public float maximumHealth;
    private float _maximumHealth = 30f;
    [SerializeField] private float _currentHealth;
    private ScoreManager sm;
    private EnemyController em;
    private bool _isEnemyAlive = true;
    private float poisonAmount = 10f;
    public float CurrentHealth { get; set; }

    //public HealthBarDefiner healthBarDefiner;

    private bool isPoisoned = false;

    public SpriteRenderer healthBar;

    //[SerializeField] private Image _healthBar;

    

    private void Start()
    {
        em = GetComponent<EnemyController>();
        sm = FindObjectOfType<ScoreManager>();
        if (spawner == null)
        {
            spawner = FindObjectOfType<Spawner>();
        }
        CurrentHealth = maximumHealth;
        _currentHealth = maximumHealth;
        healthBar.GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (CurrentHealth / maximumHealth < 0)
        {
            //Debug.Log("eroor");
            //Debug.Log(CurrentHealth + "Current <=");
            
        }
        
        if (isPoisoned == true && CurrentHealth > 0)
        {
            CurrentHealth -= Time.deltaTime * poisonAmount;
            healthBar.size = new Vector2(CurrentHealth / maximumHealth, 1f);
        }


        //healthBarDefiner.healthRatio = CurrentHealth / maximumHealth ;
        /*if(CurrentHealth/maximumHealth > 0.5f)
        {
            healthBar.transform.localScale = new Vector3(CurrentHealth / maximumHealth, 1f, 1f);
        }*/


        /*if (_healthBar != null)
        {
            _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount,
                CurrentHealth / maxHealth, Time.deltaTime);    
        }*/
    }

    public void DealDamage(int damageReceived)
    {
        enemyDieSound.Play();
        CurrentHealth -= damageReceived;
        _currentHealth = CurrentHealth;
        healthBar.size = new Vector2(CurrentHealth / maximumHealth, 1f);
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            _isEnemyAlive = false;
            //enemyDieSound.Play();
            Die();
            
            
        }
    }

    
    public void Poison(float poisonDuration)
    {
        isPoisoned = true;
        poisonDuration -= Time.deltaTime;
        if (poisonDuration < 0)
        {
            isPoisoned = false;
        }

    }

    private void Die()
    {
        sm.money += em.enemyWorth;
        spawner.enemyRemaining--;
        spawner.SpawnWave();
        //Debug.Log(sm.money);
        Destroy(gameObject);  

        
    }
}