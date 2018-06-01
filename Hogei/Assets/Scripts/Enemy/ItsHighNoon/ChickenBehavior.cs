using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehavior : EnemyBehavior
{

    [Header("Movement Settings")]
    [Tooltip("Trigger the jumps on the animation")]
    public bool AnimationTriggered = true;
    [Tooltip("Time between jumps when not jumps aren't animation triggered")]
    public float TimeBetweenJumps = 1f;
    [Tooltip("Speed object travels at")]
    public float jumpForce = 3.0f;

    [Header("Tags")]
    public string targetTag = "Player";
    public string bulletTag = "Bullet";

    [Header("Attack Settings")]
    public float timeBetweenAttacks = 1f;
    public int Damage = 1;

    [Header("Animation Settings")]
    public Vector2 IdleSpeedVariance = Vector2.zero;

    //object refs
    private GameObject target;

    //Animator Ref
    private Animator myAnim;

    //control refs
    Rigidbody myRigid;

    private GameObject HitTarget;
    private float LastAttackTime;
    private float LastJumpTime;

    // Use this for initialization
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        myAnim.speed = Random.Range(IdleSpeedVariance.x, IdleSpeedVariance.y);
        jumpForce += (Random.Range(0f, 1f) - 0.5f);
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
            myAnim.speed = 1;
            myAnim.SetBool("Walking", true);
            //Look at target
            transform.LookAt(target.transform.position);
            //remove rotations on x and z
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
            //move in that direction
            //myRigid.velocity = transform.forward * moveSpeed;
            //transform.position += (transform.forward * moveSpeed) * Time.deltaTime;
            if (!AnimationTriggered)//Trigger the jump if the timer equals zero
            {
                if (Time.time - LastJumpTime > TimeBetweenJumps)
                {
                    JumpEvent();
                    LastJumpTime = Time.time;
                }
            }
        }
        else
        {
            myAnim.speed = Random.Range(IdleSpeedVariance.x, IdleSpeedVariance.y);
            myAnim.SetBool("Walking", false);
            myRigid.velocity = Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        if(SpawnerParent) SpawnerParent.DecrementEnemyCount();
    }

    //On death logic
    public override void AmDead()
    {

    }

    public void JumpEvent()
    {
        myRigid.AddForce((transform.forward + transform.up) * jumpForce, ForceMode.Impulse);
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
                myAnim.SetTrigger("Attack");
                //HitTarget = collision.gameObject;
                collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(Damage);
                LastAttackTime = Time.time;
            }
        }
    }

    public void AttackHit()
    {
        if(HitTarget) HitTarget.GetComponent<EntityHealth>().DecreaseHealth(Damage);
    }
}
