using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHitTracker : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.position = MouseTarget.GetWorldMousePos();
	}
}
