using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupManager : MonoBehaviour {

    public enum GunMods
    {
        SPEEDUP,
        DAMAGEUP
    };

    public GameObject TheSoup;
    [Tooltip("The angle the soup rotates each second")]
    public float SoupRotationSpeed = 90f;
    private List<GameObject> SoupIngredients;

	// Use this for initialization
	void Start () {
        SoupIngredients = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {       
        TheSoup.transform.Rotate(new Vector3(0f, 0f, SoupRotationSpeed * Time.deltaTime));
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject Obj = other.gameObject;
        SoupIngredients.Add(Obj);
    }
}
