using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class FunctionOnMouseDown : MonoBehaviour {

    public UnityEvent functionToCall;

	void OnMouseDown()
    {
        functionToCall.Invoke();
    }
}
