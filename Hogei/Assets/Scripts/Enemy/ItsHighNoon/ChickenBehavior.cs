using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehavior : EnemyBehavior
{

    [Header("Speed vars")]
    [Tooltip("Speed object travels at")]
    public float moveSpeed = 3.0f;

    [Header("Tags")]
    public string targetTag = "Player";
    public string bulletTag = "Bullet";

    [Header("Attack Settings")]
    public float timeBetweenAttacks = 1f;
    public int Damage = 1;

    //object refs
    public GameObject target;

    //Animator Ref
    private Animator myAnim;

    //control refs
    Rigidbody myRigid;

    private float LastAttackTime;

    // Use this for initialization
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            MoveAtTarget();
        }

    }

    public override void Activate()
    {
        base.Activate();
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public void SetUp(GameObject thing)
    {
        isActive = true;
        target = thing;
    }

    //Move towards target, disregards terrain restrictions
    private void MoveAtTarget()
    {
        if (target)
        {
            myAnim.SetBool("Walking", true);
            //Look at target
            transform.LookAt(target.transform.position);
            //remove rotations on x and z
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
            //move in that direction
            //myRigid.velocity = transform.forward * moveSpeed;
            //transform.position += (transform.forward * moveSpeed) * Time.deltaTime;
        }
        else
        {
            myAnim.SetBool("Walking", false);
            myRigid.velocity = Vector3.zero;
        }
    }

    //On death logic
    public override void AmDead()
    {

    }

    public void JumpEvent()
    {
        print("I Jump");
        myRigid.AddForce((transform.forward + transform.up) * moveSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if not active
        if (!isActive)
        {
            //check is bullet
            if (collision.gameObject.CompareTag(bulletTag))
            {
                //activate
                isActive = true;
                //set target
                target = GameObject.FindGameObjectWithTag(targetTag);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {      
        if (isActive)
        {
            if (Time.time - LastAttackTime >= timeBetweenAttacks && collision.gameObject.CompareTag(targetTag))
            {
                collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(Damage);
                LastAttackTime = Time.time;
            }
        }
    }
}
