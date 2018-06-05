using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panning : MonoBehaviour {

	public Renderer renderer;
	public Material instancedMaterial;

	public float xDirection = 0F;
	public float yDirection = 0F;

	// Use this for initialization
	void Start () {
		renderer= gameObject.GetComponent<Renderer>();
		instancedMaterial = renderer.material;
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 panSpeed = new Vector2(xDirection, yDirection);
		instancedMaterial.SetVector("_PanSpeed", panSpeed);
		
	}
}
