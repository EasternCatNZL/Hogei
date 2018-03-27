using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour {

    [Header("Cloud Options")]
    [Tooltip("Heights the cloud can vary between for height")]
    public Vector2 CloudHeightRange = Vector2.zero;
    public float CloudAmount = 10;
    public float CloudAreaSize = 10f;
    public Vector3 CloudDirection = Vector3.forward;
    public Vector2 CloudSpeedRange = Vector2.zero;
    public Vector2 CloudScaleRange = Vector2.zero;
    public GameObject[] CloudVariations;
    [Header("Material Options")]
    public float EmissionsValue = 1.5f;

    private List<GameObject> Clouds;
	private bool SphereArea = false;
	private float BoxWidth = 0f;
	private float BoxLength = 0f;

	// Use this for initialization
	void Start () {
        Clouds = new List<GameObject>();
        if(CloudVariations.Length > 0)
        {
            Init();
        }
        else
        {
            Debug.Log("There are no cloud prefabs for the cloud manager to spawn");
        }

	}

    public void Init()
    {
        //transform.position = new Vector3(0f, CloudHeightCenter, 0f);
		if (GetComponent<SphereCollider> ()) {
			SphereCollider SphereC = GetComponent<SphereCollider> ();
			SphereC.radius = CloudAreaSize;
			SphereArea = true;
		} else if (GetComponent<BoxCollider> ()) {
			BoxCollider BoxC = GetComponent<BoxCollider> ();
			BoxWidth = BoxC.size.x;
			BoxLength = BoxC.size.z;
		}
		else
		{
			Debug.Log ("No Collider on " + gameObject.name);
		}
        ClearClouds();
        for(int i = 0; i < CloudAmount; ++i)
        {
			Vector3 CloudPosition = Vector3.zero;
            //If the cloud are spawning in a sphere
			if (SphereArea) {
				CloudPosition = Random.insideUnitSphere * CloudAreaSize;
                //Debug.Log("SP " + CloudPosition.ToString());
			}
            //If the clouds are spawning in a box
            else {
                CloudDirection = transform.forward;
				CloudPosition = new Vector3(Random.Range(0f,BoxWidth) - BoxWidth/2, 0f, Random.Range(0f,BoxLength) - BoxLength/2);
				//Debug.Log ("BX " + CloudPosition.ToString ());
			}
            CloudPosition.y = transform.position.y + Random.Range(CloudHeightRange.x, CloudHeightRange.y) - CloudHeightRange.y/2;
            //Create a new cloud
            GameObject newCloud = Instantiate(CloudVariations[Random.Range(0, CloudVariations.Length)],Vector3.zero, Quaternion.identity);
            //Set cloud parent
            newCloud.transform.parent = gameObject.transform;
            //Set cloud position
            newCloud.transform.localPosition = CloudPosition;
            //Rotate to point in the cloud direction
            newCloud.transform.Rotate(new Vector3(0f, -Vector3.Angle(newCloud.transform.right, CloudDirection)));
            //Change cloud scale
            float CloudScale = Random.Range(CloudScaleRange.x, CloudScaleRange.y);
            newCloud.transform.localScale = new Vector3(CloudScale, CloudScale, CloudScale);
            //Set cloud velocity
            newCloud.GetComponent<Rigidbody>().velocity = CloudDirection * Random.Range(CloudSpeedRange.x, CloudSpeedRange.y);

            //Change the clouds material settings
            newCloud.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            newCloud.GetComponent<MeshRenderer>().material.SetFloat("_Emission", EmissionsValue);

            //Add cloud to cloud list
            Clouds.Add(newCloud);
        }
    }

    void ClearClouds()
    {
        for(int i = Clouds.Count - 1; i >= 0; --i)
        {
            GameObject.Destroy(Clouds[i]);
        }
        Clouds.Clear();
    }

    private void OnTriggerExit(Collider collision)
    {
        GameObject Cloud = collision.gameObject;
        if(SphereArea) Cloud.transform.position -= CloudDirection * CloudAreaSize * 1.5f;
        else Cloud.transform.position -= CloudDirection * BoxLength;
    }
}
