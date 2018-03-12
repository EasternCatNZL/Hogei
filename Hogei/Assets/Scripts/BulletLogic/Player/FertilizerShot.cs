using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerShot : MonoBehaviour {

    private Vector3 Bullseye;
    public GameObject bulletObject;
    Vector3 MousePosition;

    public float Angle = 45.0f;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetTarget();
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Launch();
        }
	}

    void Launch()
    {
        GameObject Bullet = Instantiate(bulletObject, transform.position, transform.rotation);
        Vector3 pos = Bullet.transform.position;
        Vector3 target = Bullseye;

        float dist = Vector3.Distance(pos, target);

        float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * Angle * 2)));
        float Vy, Vz;

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * Angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * Angle);

        Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        Vector3 globalVelocity = transform.TransformVector(localVelocity);

        Bullet.GetComponent<Rigidbody>().velocity = globalVelocity;
    }

    void GetTarget()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Bullseye = MousePosition;
    }

}
