using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfinateSpawner : EnemyBehavior
{

    public GameObject Enemy;
    public float SpawnTime = 1.0f;
    public float ScaleUpTime = 0.5f;
    [Header("Launch Settings")]
    [Tooltip("Force to launch the spawned enemy out with. Set to -1 to add no force")]
    public float LaunchForce = 5f;
    private float timer = 0.0f;


    // Use this for initialization
    void Start()
    {
        if (LaunchForce == 0) LaunchForce = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                SpawnEnemy();
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
        Chick.transform.DOScale(1f, ScaleUpTime);
        Chick.GetComponent<Rigidbody>().AddForce(transform.forward * LaunchForce, ForceMode.Impulse);
        timer = SpawnTime;
    }

    public override void Activate() { isActive = true; }
    public void SetLaunchForce(float _LaunchForce) { LaunchForce = _LaunchForce; }
    public override void Deactivate() { isActive = false; }
}
