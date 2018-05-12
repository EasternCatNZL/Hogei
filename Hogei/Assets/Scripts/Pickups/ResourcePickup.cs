using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : GameEntity {

    [Header("Resource Settings")]
    public ResourceManager.Resources Drop;
    public int ResourceGained = 1;
    public string PlayerTag = "";
    [Header("Visuals")]
    public float RotationSpeed = 90f;
    public GameObject PickupVFX = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(PlayerTag))
        {
            ResourceManager.GetResourceManager().AddResource(Drop, ResourceGained);
            if (PickupVFX) Instantiate(PickupVFX, transform.position, PickupVFX.transform.rotation);
            Destroy(this);
        }
    }
}
