using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfinateSpawner : MonoBehaviour
{

    public GameObject Enemy;
    public float SpawnTime = 1.0f;
    public float ScaleUpTime = 0.5f;
    [Header("Launch Settings")]
    public float LaunchForce;
    private float timer = 0.0f;
    private bool CanSpawn = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CanSpawn == true)
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
            CanSpawn = true;
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

    public void Activate() { CanSpawn = true; }
    public void Deactivate() { CanSpawn = false; }
}
