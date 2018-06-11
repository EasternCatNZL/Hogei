using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Walking Chicken Coop
/// Uses several different attacks to elimminate the play
/// -Dive Attack: Lifts off into the air then crashes down into an aoe
/// -Chicken Swarm: Shoots live chickens at the player
/// -Slam Attack: Slams the ground and creates bullets
/// </summary>
public class CoopWalkerBehaviour : EnemyBehavior
{
    private GameObject PlayerRef;

    private GameObject Target;
    [Header("Attack Settings")]
    public float DelayBetweenAttacks = 5f;
    [Header("Slam Attack Settings")]
    public GameObject SlamBullet;
    public float SlamBulletSpeed;
    public int SlamNumBullets;
    public Transform RightFoot;
    public Transform LeftFoot;

    [Header("Chicken Spawner Settings")]
    public InfinateSpawner Spawner;

    [Header("Jump Settings")]
    public float JumpHeight = 10f;
    private float MinimumY;//The height the chicken coop should reach when jumping

    [Header("Dive Settings")]
    public int DiveNumBullets;
    public GameObject DiveMarker;

    [Header("VFX Settings")]
    public GameObject DustExplosion;
    public Transform DustExplosionSpawn;

    private bool Landed = false;
    private bool Flying = false;
    private Animator myAnim;
    private Rigidbody myRigid;
    private int CurrentAttack = 0;
    private int LastAttack = 0;



    private float LastAttackTime = 0;

    // Use this for initialization
    void Start()
    {
        PlayerRef = PlayerManager.GetInstance().Player;
        myAnim = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Landed && isActive)
        {
            if (Time.time - LastAttackTime > DelayBetweenAttacks)
            {
                ChooseAttack();
            }
            if (Mathf.Abs(PlayerRef.transform.position.y - transform.position.y) < 10f)
            {
               transform.LookAt(PlayerRef.transform);
            }
            else
            {
                isActive = false;
            }
        }
        if (CurrentAttack == 3)//Dive Attack
        {
            print(transform.position.y + " > " + MinimumY);
            if (transform.position.y > MinimumY)
            {
                myAnim.SetBool("Flying", true);
                myRigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                Dive();
                CurrentAttack = 0;
            }
        }
    }

    public override void Activate()
    {
        base.Activate();
        myRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    //Randomly chooses an attack
    void ChooseAttack()
    {
        //Esure a the same attacks aren't repeated
        CurrentAttack = Random.Range(1, 3);
        if (CurrentAttack == LastAttack && CurrentAttack != 3) CurrentAttack += 1;
        else if (CurrentAttack == LastAttack && CurrentAttack != 1) CurrentAttack -= 1;
        LastAttack = CurrentAttack;

        switch (CurrentAttack)
        {
            case 1:// Stomp Attack
                myAnim.SetTrigger("Stomp");
                LastAttackTime = Time.time;
                break;
            case 2://Chicken Spawn Attack
                myAnim.SetTrigger("JumpAround");
                LastAttackTime = Time.time;
                break;
            case 3://Dive Attack
                Landed = false;
                Flying = true;
                Jump();
                LastAttackTime = Time.time;
                break;
        }
    }

    private void SetDiveMarker()
    {
        if (DiveMarker)
        {
            GameObject _Marker = Instantiate(DiveMarker, PlayerRef.transform.position, Quaternion.identity);
            Destroy(_Marker, 2f);
        }
    }

    void Jump()
    {
        PlayDustExplosion(DustExplosionSpawn.position);

        myAnim.SetTrigger("Jump");
        myRigid.DOMoveY(transform.position.y + JumpHeight, 1f);
    }

    void Dive()
    {
        myAnim.SetTrigger("Crouch");
        Vector3 _NewPos = new Vector3(PlayerRef.transform.position.x, transform.position.y, PlayerRef.transform.position.z);
        transform.position = _NewPos;
        SetDiveMarker();
        myRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void SpawnChickens()
    {
        Spawner.BurstSpawnEnemy();
    }

    //Gets called by animation events
    void SlamEvent(int _FootIndex)//0 = RightFoot, 1 = LeftFoot
    {
        PlayDustExplosion(DustExplosionSpawn.position);
        if (_FootIndex == 0) CircleSpray(SlamNumBullets, RightFoot.position);
        else if (_FootIndex == 1) CircleSpray(SlamNumBullets, LeftFoot.position);
    }

    //Gets called by animation events
    void SpawnEvent()
    {
        PlayDustExplosion(DustExplosionSpawn.position);
        SpawnChickens();
    }

    void CircleSpray(int _NumBullets, Vector3 _SpawnPos)
    {
        //get a random starting angle
        float angle = Random.Range(0f, 360f);
        //Angle Change 
        float angleChange = 360 / _NumBullets;
        //reset the angle total
        float currentAngleTotal = 0.0f;
        //while current angle total not reached 360, keep spawning bullets
        while (currentAngleTotal < 360.0f)
        {
            //create a shot
            //get the current angle as a quaternion
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = new Vector3(0.0f, angle, 0.0f);
            //get a bullet from the bank
            GameObject bullet = Instantiate(SlamBullet, _SpawnPos, transform.rotation);
            //set the bullets position to this pos
            bullet.transform.position = _SpawnPos;
            //set the bullet's rotation to current rotation
            bullet.transform.rotation = currentRotation;
            //setup the bullet and fire
            bullet.GetComponent<RegularStraightBullet>().SetupVars(SlamBulletSpeed);

            //change the angle between shots
            angle += angleChange;
            //add the amount angle changed to current angle total
            currentAngleTotal += angleChange;
        }
    }

    void OnCollisionEnter(Collision _Col)
    {
        if(_Col.gameObject.CompareTag("Enviroment"))
        {
            Destroy(_Col.gameObject);
        }
        if (!Landed && _Col.gameObject.CompareTag("Dungeon"))
        {
            PlayDustExplosion(DustExplosionSpawn.position);
            CircleSpray(DiveNumBullets, transform.position + transform.up/2);
            myAnim.SetTrigger("LookAround");
            myAnim.SetTrigger("Stomp");
            myAnim.SetBool("Idle", true);
            myAnim.SetBool("Flying", false);
            MinimumY = transform.position.y + JumpHeight - JumpHeight * 0.1f;
            Landed = true;
            Flying = false;
        }
    }

    private void PlayDustExplosion(Vector3 _Location)
    {
        GameObject _VFX = Instantiate(DustExplosion, _Location, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
        _VFX.transform.localScale = new Vector3(2f, 2f, 2f);
    }
}
