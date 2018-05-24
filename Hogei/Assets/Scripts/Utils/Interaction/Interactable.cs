using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public UnityEvent OnClick;

	// Use this for initialization
	void Start () {
        gameObject.tag = "Interactable";
	}

    public virtual void MouseEnter()
    {

    }

    public virtual void MouseExit()
    {

    }

    public virtual void MouseClick()
    {
        OnClick.Invoke();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
