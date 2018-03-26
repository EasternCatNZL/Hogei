using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SheepTriggerer : MonoBehaviour {

    [Header("Tags")]
    public string targetTag = "Player";
    public string bulletTag = "Bullet";

    public GameObject WarningObject;
    private float WarningBegan = 0.0f;
    public float WarningTime = 0.5f;
    private bool WarningActive = false;

    //script refs
    SheepBehaviour sheep;

    private bool isTriggered = false; //checks to see if trigger has been triggered

    // Use this for initialization
    void Start () {
        sheep = GetComponentInChildren<SheepBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        if(WarningActive == true)
        {
            WarningBegan = Time.time;
            if(Time.time > WarningTime)
            {               
                WarningActive = false;
            }
        }
        
	}

    //trigger enter
    void OnTriggerEnter(Collider other)
    {
        //check other
        if (other.gameObject.CompareTag(targetTag) && !isTriggered)
        {
            //Check object hasnt been killed before triggered
            if (sheep)
            {
                Warning();
                //Setup();
                //change has setup to true
                isTriggered = true;
                sheep.isTriggered = true;
                //set time charge begins to now
                sheep.timeChargeBegan = Time.time;
                //set target object
                sheep.target = other.gameObject;
            }
        }
    }

    void Warning()
    {
        WarningActive = true;
        Vector3 currentPos = transform.position;
        Vector3 extraHeight = new Vector3(0.0f, 2.0f, 0.0f);
        GameObject Object = Instantiate(WarningObject, currentPos + extraHeight, Quaternion.identity);
        Object.transform.DOScaleY(0.5f, 0.5f).SetEase(Ease.OutBack, 5f);
    }


}
