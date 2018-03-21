using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitAttackBehavior : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("The bullet object that is fired")]
    public GameObject bulletObject;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 10.0f;
    [Tooltip("Number of bubbles")]
    public int numberOfBullets = 3;

    [Header("Angle control")]
    [Tooltip("Angle shot can be fired out at")]
    public float angleOut = 30.0f;

    //script refs
    public HermitMoveBehavior hermit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Shoot out bubble
    private void FireBubble()
    {
        //for number of bullets
        for(int i = 0; i > numberOfBullets; i++)
        {
            //get a random angle
            float randomAngle = Random.Range(-angleOut, angleOut);
            //create a bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the rotation
            Quaternion newRotation = Quaternion.Euler(0.0f, randomAngle, 0.0f);
            bulletClone.transform.rotation = newRotation;

        }
    }
}
