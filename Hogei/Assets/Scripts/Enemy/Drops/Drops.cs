using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {

    public Vector3 SpawnOffset = Vector3.zero;
    public Vector3 RotationOffset = Vector3.zero; 
    public GameObject[] itemDrop;


    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        		
	}

    public void DropItem()
    {
        Vector3 DropPosition = Vector3.zero;
        int Count = 0;
        foreach (GameObject item in itemDrop)
        {
            Count++;
            DropPosition = new Vector3(SpawnOffset.x + Mathf.Sin((360 / itemDrop.Length) * Mathf.Deg2Rad * Count), SpawnOffset.y + 0f, SpawnOffset.z + Mathf.Cos((360 / itemDrop.Length) * Mathf.Deg2Rad * Count));
            DropPosition += transform.position;
            Instantiate(item, DropPosition, Quaternion.Euler(RotationOffset));

        }
    }

    /*
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            DropItem();
        }
    }
    */

    public void OnDeathDrop()
    {
        if (GetComponent<EntityHealth>().CurrentHealth <= 0.0f)
        {
            
            Vector3 DropPosition = Vector3.zero;
            int Count = 0;
            foreach (GameObject item in itemDrop)
            {
                Count++;
                DropPosition = new Vector3(SpawnOffset.x + Mathf.Sin((360 / itemDrop.Length) * Mathf.Deg2Rad * Count), SpawnOffset.y + 0f, SpawnOffset.z + Mathf.Cos((360 / itemDrop.Length) * Mathf.Deg2Rad * Count));
                DropPosition += transform.position;
                Instantiate(item, DropPosition, Quaternion.Euler(RotationOffset));
            }
        }
    }       
}
