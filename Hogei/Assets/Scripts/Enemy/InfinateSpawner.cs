using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfinateSpawner : EnemyBehavior
{

    [Header("Spawner Settings")]
    public GameObject Enemy;
    [Tooltip("The maximum number of enemies that can be spawned at anytime. 0 = Infinite")]
    public int SpawnMaximum = 10;
    public float SpawnTime = 1.0f;
    public float ScaleUpTime = 0.5f;
    [Tooltip("Spawns the enemies in burst of groups instead of single file.")]
    public bool BurstSpawn = false;
    public int BurstAmount = 10;
    [Tooltip("The angle the enemies can be launched in.")]
    public float SpawnAngle = 45;
    [Header("Launch Settings")]
    [Tooltip("Force to launch the spawned enemy out with. Set to -1 to add no force.")]
    public float LaunchForce = 5f;
    private float timer = 0.0f;
    private int EnemiesSpawned = 0;//The number of enemies currently spawned


    // Use this for initialization
    void Start()
    {
        if (LaunchForce == 0) LaunchForce = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && EnemiesSpawned < SpawnMaximum)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                if (BurstSpawn) BurstSpawnEnemy();
                else SpawnEnemy();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isActive = true;
            timer = SpawnTime;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag.Equals("Player"))
        //{
        //    CanSpawn = false;
        //}
    }

    void SpawnEnemy()
    {
        GameObject Chick = Instantiate(Enemy, transform.position, transform.rotation);
        Chick.transform.localScale = Vector3.zero;
        Chick.GetComponent<EnemyBehavior>().Activate();
        Chick.GetComponent<EnemyBehavior>().SpawnerParent = this;
        Chick.transform.DOScale(1f, ScaleUpTime);
        Chick.GetComponent<Rigidbody>().AddForce(transform.forward * LaunchForce, ForceMode.Impulse);
        timer = SpawnTime;
        EnemiesSpawned++;
    }

    public void BurstSpawnEnemy()
    {
        int SpawnAmount = BurstAmount;
        //Get the number of enemies to be spawned
        if (SpawnAmount > SpawnMaximum - EnemiesSpawned) SpawnAmount = SpawnMaximum - EnemiesSpawned;
        //Create the starting rotation
        Vector3 SpawnRotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - SpawnAngle / SpawnAmount - 1, transform.rotation.eulerAngles.z);
        for(int i = 0; i < SpawnAmount; ++i)
        {
            Debug.Log(SpawnRotation.ToString());
            GameObject Chick = Instantiate(Enemy, transform.position, Quaternion.Euler(SpawnRotation));
            Chick.transform.localScale = Vector3.zero;
            Chick.GetComponent<EnemyBehavior>().Activate();
            Chick.GetComponent<EnemyBehavior>().SpawnerParent = this;
            Chick.transform.DOScale(1f, ScaleUpTime);
            Chick.GetComponent<Rigidbody>().AddForce((Chick.transform.forward + Chick.transform.up) * LaunchForce, ForceMode.Impulse);
            timer = SpawnTime;
            EnemiesSpawned++;
            SpawnRotation = new Vector3(transform.rotation.eulerAngles.x, SpawnRotation.y + SpawnAngle / SpawnAmount - 1, transform.rotation.eulerAngles.z);
        }
        
    }

    public override void Activate() { isActive = true; }
    public void SetLaunchForce(float _LaunchForce) { LaunchForce = _LaunchForce; }
    public override void Deactivate() { isActive = false; }

    public void DecrementEnemyCount() { EnemiesSpawned--; }
}
