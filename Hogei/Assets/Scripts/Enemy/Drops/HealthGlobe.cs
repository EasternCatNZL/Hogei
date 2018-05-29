using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthGlobe : MonoBehaviour {

    GameObject Player;
    public float HealthIncrease = 1.0f;
    public float rotationSpeed = 100.0f;
    public GameObject pickupVFX;
    [Header("Vacuum Settings")]
    public float travelSpeed = 5f;
    [Tooltip("Min Range to pickup; x\nMaximum Range to vacuum; y")]
    public Vector2 PickupRangeMinMax = new Vector2(5f, 10f);
    private Rigidbody myRigid;

    // Use this for initialization
    void Start () {
        Player = PlayerManager.GetInstance().Player;
        transform.DOJump(transform.position, 0.8f, 1, 0.5f);
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        if (Player) CheckPickupRange();
	}

    void CollectItem()
    {
        Player.GetComponent<EntityHealth>().IncreaseHealth(HealthIncrease);
        Instantiate(pickupVFX, Player.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void CheckPickupRange()
    {
        float DistToPlayer = Mathf.Abs(Vector3.Distance(transform.position, Player.transform.position));
        if (DistToPlayer < PickupRangeMinMax.y)
        {
            if (DistToPlayer < PickupRangeMinMax.x)
            {
                CollectItem();
            }
            SteerToTarget(Player);
        }
    }

    private void SteerToTarget(GameObject _Target)
    {
        //steer towards target
        //get vector towards target
        Vector3 desireVelocity = _Target.transform.position - transform.position;
        float distance = desireVelocity.magnitude;
        desireVelocity = Vector3.Normalize(desireVelocity) * travelSpeed;
        //get steering force
        Vector3 steeringForce = desireVelocity - myRigid.velocity;
        //steeringForce /= adjustForce;
        //adjust velocity
        myRigid.velocity = Vector3.ClampMagnitude(myRigid.velocity + (steeringForce * Time.deltaTime), travelSpeed);
        transform.rotation = Quaternion.LookRotation(myRigid.velocity);
    }
}
