using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupIngredient : MonoBehaviour {

    public bool Spin = true;

    public AudioClip PickupSounds;

    public enum IngredientType
    {
        Lamb,
        HornedLamb,
        Whiskey,
        Cactus,
        Chicken
    }

    public IngredientType Type;
    public bool IsWeaponEffect = false;
    public Weapon.WeaponModifier WeaponMod;
    public float rotationSpeed = 90f;
    [Header("Vacuum Settings")]
    private GameObject PlayerRef;
    public float travelSpeed = 5f;
    [Tooltip("Min Range to pickup; x\nMaximum Range to vacuum; y")]
    public Vector2 PickupRangeMinMax = new Vector2(5f, 10f);
    private Rigidbody myRigid;

    public IngredientBowl myBowl; //ref to the bowl object this ingredient came from

    void Start()
    {
        PlayerRef = PlayerManager.GetInstance().Player;
        myRigid = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if(Spin) transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        if (PlayerRef) CheckPickupRange();
    }

    private void CheckPickupRange()
    {
        float DistToPlayer = Mathf.Abs(Vector3.Distance(transform.position, PlayerRef.transform.position));
        if (DistToPlayer < PickupRangeMinMax.y)
        {
            if(DistToPlayer < PickupRangeMinMax.x)
            {
                CollectItem();
            }
            SteerToTarget(PlayerRef);
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
        //transform.rotation = Quaternion.LookRotation(myRigid.velocity);
    }

    public Weapon.WeaponModifier GetModifier() { return WeaponMod; }

    public static int GetIngredientTypeCount() { return System.Enum.GetNames(typeof(IngredientType)).Length; }

    private void CollectItem()
    {
        PlayerManager.GetInstance().AddIngredientInventory(this);
        if (PickupSounds) MusicManager.GetInstance().PlaySoundAtLocation(PickupSounds, transform.position);
        Destroy(gameObject);
    }

    //send back to bowl
    public void SendBackToBowl()
    {
        if (myBowl)
        {
            //add one back to ingredient bowl
            myBowl.IngredientAmount++;
            //destroy the object
            Destroy(gameObject);
        }
    }



    void OnCollisionEnter(Collision _Col)
    {
        //if(_Col.gameObject.CompareTag("HotPot"))
        //{
        //    SendBackToBowl();
        //}
    }
}
