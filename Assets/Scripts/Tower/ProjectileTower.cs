using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : MonoBehaviour
{
    public AudioSource fireSound;
    private Tower _theTower;

    public GameObject projectile;
    public Transform firePoint;
    public float timeBetweenShots = 1f;
    private float _timeBetweenShotsDefault;
    public Transform target;
    public GameObject missle;
    
    private void Start()
    {
        _theTower = GetComponent<Tower>();
    }

    private void Update()
    {
        if (_theTower.enemiesInRange.Count > 0)
        {
            FindClosestEnemy();
            RotateTowardsTarget();
            FireWeapon();
        }
    }

    public void FireWeapon()
    {
        _timeBetweenShotsDefault -= Time.deltaTime;
        if (_timeBetweenShotsDefault <= 0 && target != null)
        {
            _timeBetweenShotsDefault = timeBetweenShots;
            
           
            fireSound.Play();   
            
            missle = Instantiate(projectile, firePoint.position, firePoint.rotation);
            missle.GetComponent<ProjectileController>().followTarget = target;
        }
    }
    
    public void FindClosestEnemy()
    {
        if (_theTower.enemiesInRange.Count > 0)
        {
            float minDistance = _theTower.towerRange + 1f;

            foreach (EnemyController enemy in _theTower.enemiesInRange)
            {
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = enemy.transform;
                    }
                }
            }
        }
    }
    
    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position - transform.position;
            if (targetPos == null)
            {
                Destroy(gameObject);
            }
            float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
            transform.Rotate(0f, 0f, angle);
        }
        
        
        
    }
}
