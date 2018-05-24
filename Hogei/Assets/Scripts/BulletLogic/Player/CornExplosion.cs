using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CornExplosion : MonoBehaviour {

    //public float radius = 0.0f;
    //public float power = 0.0f;
    //public float upwardsForce = 0.0f;

    public float scaleSize = 0.0f;
    public float scaleDuration = 0.0f;
    public float shakeIntensity = 5f;

    public float Damage = 1.0f;
    public float timer = 0.3f;

    public GameObject ExplosionVFX;

    private List<GameObject> ObjectsInRange;

    //private bool hasExploded = false;
    //private Vector3 ExplosionPos;     
        
	// Use this for initialization
	void Start () {
        //ExplosionPos = transform.position;
        ObjectsInRange = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        Scale();
        timer -= Time.deltaTime;
        //print(timer);
        if(timer <= 0.0f)
        {
            //Explode();            
            OnDestroy();
        }         

	}

    void Explode()
    {
        //hasExploded = true;
        /*
        Vector3 explosionPos = transform.position;
        Collider[] Col = Physics.OverlapSphere(explosionPos, radius);
        
        foreach (Collider hit in Col)
        {
            GameObject temp = hit.gameObject;
            temp.GetComponent<EntityHealth>().DecreaseHealth(1);
        }
        
        
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
        */
    }

    void Scale()
    {
        Sequence CornScaleUp = DOTween.Sequence();
        CornScaleUp.Insert(0,transform.DOScale(scaleSize, scaleDuration));
        CornScaleUp.Insert(0, transform.DOShakeRotation(scaleDuration, shakeIntensity));
        CornScaleUp.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Player"))
        {
            ObjectsInRange.Add(other.gameObject);
            print(other.gameObject.name);
        }
        /*
        if (hasExploded == true && other.GetComponent<EntityHealth>())
        {
            //other.attachedRigidbody.AddExplosionForce(power, ExplosionPos, radius, upwardsForce);
            other.gameObject.GetComponent<EntityHealth>().DecreaseHealth(1);
            Destroy(gameObject);
        }*/
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Player"))
        {
            ObjectsInRange.Remove(other.gameObject);
        }
    }
    void OnDestroy()
    {
        foreach (GameObject obj in ObjectsInRange)
        {
            if (obj == null) continue;
            if (obj.GetComponent<EntityHealth>())
            {
                obj.GetComponent<EntityHealth>().DecreaseHealth(Damage);
            }
        }
        GameObject _VFX = Instantiate(ExplosionVFX, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-90f, 0f, 0f)));
        _VFX.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }

}
