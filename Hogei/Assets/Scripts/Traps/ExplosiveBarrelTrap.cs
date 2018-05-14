using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelTrap : GameEntity
{
    public int ExplosionDamage = 5;
    public List<GameObject> ObjectsInRange;

	// Use this for initialization
	void Start () {
        ObjectsInRange = new List<GameObject>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Player"))
        {
            ObjectsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Player"))
        {
            ObjectsInRange.Remove(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject obj in ObjectsInRange)
        {
            if (obj.GetComponent<EntityHealth>())
            {
                obj.GetComponent<EntityHealth>().DecreaseHealth(ExplosionDamage);
            }
        }
    }
}
