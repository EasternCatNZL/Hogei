using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Calls the given function when triggered
public class FunctionTrigger : MonoBehaviour {

    public UnityEvent functionToCall;

    private void OnTriggerEnter(Collider other)
    {
        functionToCall.Invoke();
    }
}
