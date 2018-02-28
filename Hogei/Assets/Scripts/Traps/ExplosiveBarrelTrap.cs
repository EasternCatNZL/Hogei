using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelTrap : MonoBehaviour {

    List<GameObject> ObjectsInRange;

	// Use this for initialization
	void Start () {
        ObjectsInRange = new List<GameObject>();
	}

    private void OnTriggerEnter(Collider other)
    {
        ObjectsInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectsInRange.Remove(other.gameObject);
    }

    private void OnDestroy()
    {
        foreach (GameObject obj in ObjectsInRange)
        {
            if (obj.GetComponent<EntityHealth>())
            {
                obj.GetComponent<EntityHealth>().DecreaseHealth(1);
            }
        }
    }
}
