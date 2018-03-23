using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitBubbleCluster : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet objects belonging to this object")]
    public GameObject bulletObject;
    [Tooltip("Number of bullets")]
    public int numBullets = 4;
    [Tooltip("Speed leeway for pop")]
    public float popSpeed = 0.2f;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 5.0f;

    //control vars
    private Rigidbody myRigid;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Release();
	}

    //Pop bullets
    private void Pop()
    {
        //for all bullets
        for(int i = 0; i < numBullets; i++)
        {
            //clone a bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
            //set random angle of current bullet
            bulletClone.transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
            //fire the bullet
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        }
        Destroy(gameObject);
    }

    //Release logic
    private void Release()
    {
        //when speed is close to zero
        if(myRigid.velocity.magnitude <= popSpeed)
        {
            Pop();
        }
    }
}
