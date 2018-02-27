using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : MonoBehaviour {

    [Tooltip("Percentage slowed by")]
    [Range(0f,1f)]
    public float SlowPercentage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {     
        if(other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<Movement>().SetSpeedModifier(1f - SlowPercentage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<Movement>().SetSpeedModifier(1f);
        }
    }
}
