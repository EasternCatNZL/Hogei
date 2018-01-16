using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelfectShieldBehaviour : MonoBehaviour {

    [Header("Bullet object")]
    [Tooltip("Player bullet that is fired back")]
    public GameObject bulletObject;
    [Tooltip("Bullet travel speed")]
    public float bulletTravelSpeed = 10.0f;

    [Header("Tags")]
    [Tooltip("Bullet tag")]
    public string bulletTag = "Bullet";

    [Header("Lifetime")]
    [Tooltip("The amount of time in secs object should exist")]
    public float lifeTime;
    //[Tooltip("The start time of the object")]
    private float startTime;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }

    //should only collide with enemy bullet layer
    private void OnCollisionEnter(Collision collision)
    {
        //check if other object is a bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //get the opposite of the forward vector of the object
            Vector3 reverseVector = -collision.gameObject.transform.forward;
            //spawn a bullet that moves in the opposite direction
            GameObject bulletClone = Instantiate(bulletObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            bulletClone.transform.rotation = Quaternion.LookRotation(reverseVector);
            //setup the bullet
            bulletClone.GetComponent<PlayerStraightBullet>().SetupVars(bulletTravelSpeed, 0, false);
            //remove it
            Destroy(collision.gameObject);
        }
    }

    //do even if clearer spawned ontop of bullets
    private void OnCollisionStay(Collision collision)
    {
        //check if other object is a bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            ////get the opposite of the forward vector of the object
            //Vector3 reverseVector = -collision.gameObject.transform.forward;
            ////spawn a bullet that moves in the opposite direction
            //GameObject bulletClone = Instantiate(bulletObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            //bulletClone.transform.rotation = Quaternion.LookRotation(reverseVector);
            //remove it
            Destroy(collision.gameObject);
        }
    }
}