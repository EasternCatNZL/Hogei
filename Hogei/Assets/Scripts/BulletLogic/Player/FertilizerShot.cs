using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class FertilizerShot : Weapon {

    private Vector3 Target;
    Vector3 MousePosition;
    //public float JumpPower = 1.0f;
    //public float Duration = 1.0f;
    public float Angle = 45.0f;
    public float TimeBetweenShots = 0.0f;
    public float LastShotTime = 0.0f;

    // Use this for initialization
    void Start () {
        Type = WeaponTypes.Fert;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Mouse0))
        {
            GetTarget();
        }
        /*
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            UseWeapon();
        }*/
	}

    public override void UseWeapon()
    {
        /*
        //Vector3 spawnOffset = new Vector3(0.0f, 0.5f, 0.0f);
        GameObject Bullet = Instantiate(bulletObject, transform.position, transform.rotation);
        Vector3 pos = Bullet.transform.position;
        Vector3 _target = Target;

        float dist = Vector3.Distance(pos, _target);

        float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * Angle * 2)));
        float Vy, Vz;

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * Angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * Angle);

        Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        Vector3 globalVelocity = transform.TransformVector(localVelocity);

        Bullet.GetComponent<Rigidbody>().velocity = globalVelocity;
        */
        if (Time.time > LastShotTime + TimeBetweenShots)
        {
            LastShotTime = Time.time;
            Vector3 spawnOffset = new Vector3(0.0f, 0.5f, 0.5f);
            GameObject Bullet = Instantiate(bulletObject, transform.position, transform.rotation);
            Vector3 pos = Bullet.transform.position;
            Vector3 _target = Target;

            float dist = Vector3.Distance(pos, _target);

            float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * Angle * 2)));
            float Vy, Vz;

            Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * Angle);
            Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * Angle);

            Vector3 localVelocity = new Vector3(0f, Vy, Vz);

            Vector3 globalVelocity = transform.TransformVector(localVelocity);

            Bullet.GetComponent<Rigidbody>().velocity = globalVelocity;
        }
        
        /*
        Vector3 _target = Target;
        Vector3 bolah = new Vector3(-1.0f, 0.0f, 0.0f);
        print(_target);
        GameObject Bullet = Instantiate(bulletObject, transform.position + bolah, transform.rotation);
        Bullet.GetComponent<Rigidbody>().DOJump(_target, JumpPower, 0, Duration, false);
        */
    }

    void GetTarget()
    {
        MousePosition = MouseTarget.GetWorldMousePos();
        Target = MousePosition;
    }

    public override void ApplyUpgrade(SoupUpgrade _Upgrade)
    {
        throw new NotImplementedException();
    }

}
