using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopyTextFadeIn : MonoBehaviour {

    public TextMesh text;
    public MeshRenderer mesh;
    public Material mat;
    public float fadeInDelay = 2.0f;
    public float alpha = 0;
    //public Color textColor;

    private float startTime = 0;
    

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
        text.font.material.color = new Color(1, 1, 1, alpha);
        mat.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > startTime + fadeInDelay)
        {
            alpha += 0.2f * Time.deltaTime;
            Mathf.Clamp(alpha, 0, 1.0f);

            mat.color = new Color(1, 1, 1, alpha);
        }
    }
}
