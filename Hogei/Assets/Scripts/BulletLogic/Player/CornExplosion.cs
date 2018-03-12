using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CornExplosion : MonoBehaviour {

    public float radius = 2.0f;
    public float power = 5.0f;
    public float upwardsForce = 5.0f;

    public float scaleSize = 3.0f;
    public float scaleDuration = 2.0f;
        
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Scale();
        if(Input.GetKeyDown(KeyCode.H))
        {
            Explode();
        }

	}

    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] Col = Physics.OverlapSphere(explosionPos, radius);
        foreach(Collider hit in Col)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, upwardsForce);
                Destroy(gameObject);
            }
        }
    }

    void Scale()
    {
        transform.DOScale(scaleSize, scaleDuration);
    }
    
}
