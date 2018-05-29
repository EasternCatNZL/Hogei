using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CactusTrigger : EnemyTrigger {

    //script refs
    

    // Use this for initialization
    void Start () {
        Enemy = GetComponentInChildren<EnemyBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //trigger enter
    void OnTriggerEnter(Collider other)
    {
        //check other
        if ((doTriggerPlayer && other.gameObject.CompareTag(targetTag)) || (doTriggerBullet && other.gameObject.CompareTag(bulletTag)) && !isTriggered)
        {
            //check object hasnt been destroyed before being triggered
            if (Enemy)
            {
                isTriggered = true;
                Enemy.isActive = true;
                Enemy.transform.DOJump(Enemy.transform.position, 1f, 1, 0.5f);
            }
            //Setup();
            //change has setup to true
            
        }
    }
}
