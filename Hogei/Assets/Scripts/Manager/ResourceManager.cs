using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public enum Resources
    {
        LAMB,
        CHICKEN,
        CACTUS,
    };

    private int[] ResourceAmounts;

    static ResourceManager Instance = null;

	// Use this for initialization
	void Start () {
        if (!Instance)
        {
            Instance = this.GetComponent<ResourceManager>();
        }
        else
        {
            Destroy(this);
        }
        ResourceAmounts = new int[System.Enum.GetNames(typeof(Resources)).Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public ResourceManager GetResourceManager()
    {
        if(!Instance)
        {
            GameObject newResMgt = new GameObject();
            newResMgt.AddComponent<ResourceManager>();
            Instance = newResMgt.GetComponent<ResourceManager>();
        }
        return Instance;
    }

    public void AddResource(Resources _Res, int _Amount)
    {
        ResourceAmounts[(int)_Res] += _Amount;
    }
}
