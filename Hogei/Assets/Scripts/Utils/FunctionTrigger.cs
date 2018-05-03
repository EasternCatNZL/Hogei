using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Calls the given function when triggered
public class FunctionTrigger : MonoBehaviour {

    public bool CheckTag;
    public string TagCheck;
    public UnityEvent functionToCall;

    private void OnTriggerEnter(Collider other)
    {
        if(CheckTag)
        {
            if(other.CompareTag(TagCheck))
            {
                functionToCall.Invoke();
            }
        }
        else
        {
            functionToCall.Invoke();
        }
    }
}
