using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatObjects : MonoBehaviour {

    public Transform ParentToAttachTo;
    public List<GameObject> FloatingObjects;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject Obj = other.gameObject;
        Obj.GetComponent<Rigidbody>().useGravity = false;
        if (!FloatingObjects.Contains(other.gameObject))
        {
            FloatingObjects.Add(Obj);
        }
        if (ParentToAttachTo)
        {
            Obj.transform.parent = ParentToAttachTo.transform;
        }
        else Obj.transform.parent = gameObject.transform;
        Obj.GetComponent<Rigidbody>().DOMoveY(transform.position.y, 3f).SetEase(Ease.OutElastic, 2f);
        Obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject Obj = other.gameObject;
        Obj.GetComponent<Rigidbody>().useGravity = false;
        if(ParentToAttachTo)
        {
            Obj.transform.parent = ParentToAttachTo.transform;
        }
        else Obj.transform.parent = gameObject.transform;

        Obj.GetComponent<Rigidbody>().DOMoveY(transform.position.y, 3f).SetEase(Ease.OutElastic, 2f);
        Obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public List<GameObject> GetObjects()
    {
        return FloatingObjects;
    }
}
