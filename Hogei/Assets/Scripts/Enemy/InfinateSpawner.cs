using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinateSpawner : MonoBehaviour
{

    public GameObject Enemy;
    public float SpawnTime = 1.0f;
    private float timer = 0.0f;
    private bool CanSpawn = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(CanSpawn == true)
        {
            timer -= Time.deltaTime;            
            if(timer <= 0.0f)
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
        if (other.gameObject.tag.Equals("Player"))
        {
            CanSpawn = false;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
        timer = SpawnTime;
    }
}
