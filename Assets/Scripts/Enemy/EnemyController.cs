using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public static Action<Enemy> OnEndReached;
    public CameraShake cameraShake;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Waypoint waypointRef;
    [SerializeField] private Spawner spawner;
    public float enemyWorth;
    public static EnemyController instance;
    public ParticleSystem particleSystem;
    public ParticleSystem impactParticle;
    private ScoreManager sm;
    private void Awake()
    {
        instance = this;
    }
    private int currentWaypointIndex;
    public EnemyHealthController EnemyHealth { get; set; }
    private void Start()
    {
        //particleSystem = getcom<ParticleSystem>();
        
        sm = FindObjectOfType<ScoreManager>();
        EnemyHealth = GetComponent<EnemyHealthController>();
        if (waypointRef == null)
        {
            waypointRef = FindObjectOfType<Waypoint>();
        }
        if (spawner == null)
        {
            spawner = FindObjectOfType<Spawner>();
        }
        currentWaypointIndex = 0;
    }
    private void Update()
    {
        //Debug.Log(particleSystem);
        Move();
        if (WaypointHasReached())
        {
            UpdateWaypointIndex();
        }
    }
    private void Move()
    {
        if (ScoreManager.instance.lives > 0)
        {
            Vector3 waypointPosition = waypointRef.GetWaypointPosition(currentWaypointIndex);
            this.transform.position = Vector2.MoveTowards(this.transform.position, waypointPosition,
                moveSpeed * Time.deltaTime);
        }
    }
    private void UpdateWaypointIndex()
    {
        float lastPointIndex = waypointRef.Points.Length - 1;
        if (currentWaypointIndex < lastPointIndex)
        {
            currentWaypointIndex++;
        }
        else // if we have reached the last waypoint!
        {
            EndPointReached();
        }
    }
    private bool WaypointHasReached()
    {
        float distanceToWaypoint = Vector3.Distance(this.transform.position,
            waypointRef.GetWaypointPosition(currentWaypointIndex));
        if (distanceToWaypoint <= 0.1f)
        {
            return true;
        }
        return false;
    }
    private void EndPointReached()
    {

        CameraShake.instance.flag = 1;
        //Debug.Log("tunaya girsin");
        sm.LoseLife();
        //Debug.Log(sm.lives);
        spawner.enemyRemaining--;
        spawner.SpawnWave();
        Destroy(gameObject);
    }
}
