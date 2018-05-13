using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour {

    [Header("Health phase changes")]
    [Tooltip("Boss start health")]
    public float bossStartHealth = 300.0f;
    [Tooltip("Phase two start health")]
    public float phaseTwoStartHealth = 200.0f;
    [Tooltip("Phase three start health")]
    public float phaseThreeStartHealth = 100.0f;

    [Header("Script refs")]
    public EntityHealth health;

    //control vars
    protected bool inPhaseOne = true; //checks in phase one
    protected bool inPhaseTwo = false; //checks in phase two
    protected bool inPhaseThree = false; //checks in phase three

 //   // Use this for initialization
 //   void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}
