using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class ProjectileController : MonoBehaviour
    {
        public static ProjectileController instance;
        public float projectileLifeTime = 1f;
        private float tempProjectileLifeTime;
        private void Awake()
        {
            instance = this;
        }

        public Transform followTarget;
        
        private EnemyHealthController _enemyHealthController;
        public float projectileSpeed = 10f;

        private Tower _theTower;
        private Collider2D _collider2D;

        private Tower _tower;
        public int minDamageDone;
        public int maxDamageDone;
        private Random rd = new Random();

        private ParticleSystem _poisonEffect;

        //public float selfDestroyTimer = 3f;
        //private float _selfDestroyTimerDefault;
        private void Start()
        {
            tempProjectileLifeTime = projectileLifeTime;
        }

        private List<EnemyController> _enemies;

        private void Update()
        {
            projectileLifeTime  -=Time.deltaTime;
            if (projectileLifeTime < 0)
            {
                projectileSpeed = tempProjectileLifeTime;
                Destroy(gameObject);
            }
            //Debug.Log("deneme");
            FollowerMissle();
            
        }

        public void FollowerMissle()
        {
            if (followTarget != null)
            {
                transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime * projectileSpeed);    
            }
            /*GameObject missle = Instantiate(ProjectileTower.instance.projectile, ProjectileTower.instance.firePoint.position, ProjectileTower.instance.firePoint.rotation);*/
            
            //transform.position += missle.transform.position * projectileSpeed * Time.deltaTime;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            int randomDamage = rd.Next(minDamageDone, maxDamageDone + 1);
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyHealthController>().DealDamage(randomDamage);
                Instantiate(EnemyController.instance.impactParticle, col.transform.position, transform.rotation);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("Poison") && col.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyHealthController>().Poison(5f);
                _poisonEffect = Instantiate(EnemyController.instance.particleSystem, col.transform.position, col.transform.rotation);
                _poisonEffect.transform.SetParent(col.transform);
                //col.GetComponentInChildren<ParticleSystem>().Play();
                Destroy(gameObject);
            }
        }
    }
