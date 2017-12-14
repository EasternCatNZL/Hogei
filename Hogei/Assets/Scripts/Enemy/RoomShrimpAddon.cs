using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomShrimpAddon : MonoBehaviour {

    public GameObject myIndicator;

	// Use this for initialization
	void Start () {
        GameObject indicatorClone = Instantiate(myIndicator, transform.position, transform.rotation);
        indicatorClone.GetComponent<EnemyOutOfScreenTracker>().SetTarget(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
