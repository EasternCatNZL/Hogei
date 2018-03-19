using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CornExplosion : MonoBehaviour {

    public float radius = 0.0f;
    public float power = 0.0f;
    public float upwardsForce = 0.0f;

    public float scaleSize = 0.0f;
    public float scaleDuration = 0.0f;

    public float timer = 5.0f;
        
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Scale();
        timer -= Time.deltaTime;
        print(timer);
        if(timer <= 0.0f)
        {
            Explode();
            Destroy(gameObject);
        }         

	}

    void Explode()
    {
        
        Vector3 explosionPos = transform.position;
        Collider[] Col = Physics.OverlapSphere(explosionPos, radius);
        /*
        foreach (Collider hit in Col)
        {
            GameObject temp = hit.gameObject;
            temp.GetComponent<EntityHealth>().DecreaseHealth(1);
        }
        */
        
        foreach (Collider hit in Col)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();            
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, upwardsForce);
                print("Bang!");
                Destroy(gameObject);
            }
            
        }
    }

    void Scale()
    {
        transform.DOScale(scaleSize, scaleDuration);
    }
    
}
