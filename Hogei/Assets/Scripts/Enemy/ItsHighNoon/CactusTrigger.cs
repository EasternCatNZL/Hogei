using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CactusTrigger : EnemyTrigger {

    //script refs
    CactusRandomSpray cactus;

    // Use this for initialization
    void Start () {
        cactus = GetComponentInChildren<CactusRandomSpray>();
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
            if (cactus)
            {
                isTriggered = true;
                cactus.isActive = true;
                cactus.transform.DOJump(cactus.transform.position, 1f, 1, 0.5f);
            }
            //Setup();
            //change has setup to true
            
        }
    }
}
