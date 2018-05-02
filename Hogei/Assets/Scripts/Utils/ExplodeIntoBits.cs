using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeIntoBits : MonoBehaviour {

    public float UpwardsForce = 10f;
    public List<GameObject> Bits;

	public void Explode()
    {
        foreach(GameObject Obj in Bits)
        {
            GameObject temp = Instantiate(Obj, transform.position, Obj.transform.rotation);
            temp.GetComponent<Rigidbody>().AddForce(transform.up * UpwardsForce, ForceMode.Impulse);
        }
    }

    public void Explode(bool _DestroyGameObject)
    {
        foreach (GameObject Obj in Bits)
        {
            GameObject temp = Instantiate(Obj, transform.position, Obj.transform.rotation);
            temp.GetComponent<Rigidbody>().AddForce(transform.up * UpwardsForce, ForceMode.Impulse);
        }if (_DestroyGameObject) Destroy(gameObject);
    }
}
