using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CactusTrigger : MonoBehaviour {

    [Header("Tags")]
    public string targetTag = "Player";

    //script refs
    CactusRandomSpray cactus;

    private bool isTriggered = false; //checks to see if trigger has been triggered

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
        if (other.gameObject.CompareTag(targetTag) && !isTriggered)
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
