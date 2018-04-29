using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoverOverText : MonoBehaviour {

    public GameObject TextObject = null;
    public Font TextFont = null;
    public Material FontMaterial = null;
    public string Text = " ";
    public Vector3 TextOffset = Vector3.zero;
    [Tooltip("Size of text(Default = 0.05)")]
    public float TextSize = 0.05f;
    public bool FaceCamera = false;

    private float TextYScale = 0f;

	// Use this for initialization
	void Start () {
		if(TextObject == null)
        {
            CreateTextObject();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateTextObject()
    {
        //Creating the Gameobject
        TextObject = new GameObject(gameObject.name + "Text");
        TextObject.AddComponent<TextMesh>();
        //Transform setup
        TextObject.transform.parent = gameObject.transform;
        TextObject.transform.localPosition = TextOffset;
        TextObject.transform.rotation = gameObject.transform.rotation;
        TextObject.transform.DOScaleY(0f, 0f);
        //Text Mesh setup
        TextMesh _Text = TextObject.GetComponent<TextMesh>();
        if (TextFont) _Text.font = TextFont;
        _Text.fontSize = 80;
        _Text.characterSize = TextSize;
        _Text.alignment = TextAlignment.Center;
        _Text.anchor = TextAnchor.MiddleCenter;
        _Text.text = Text;
        //Set Material
        TextObject.GetComponent<MeshRenderer>().material = FontMaterial;
        //Add face camera script
        if (FaceCamera)
        {
            TextObject.AddComponent<FaceCamera>();
            TextObject.GetComponent<FaceCamera>().Invert = true;
        }
        //Save TextObject Y Scale
        TextYScale = TextObject.transform.localScale.y;
    }

    private void OnMouseEnter()
    {
        TextObject.transform.DOScaleY(TextYScale, 0.5f).SetEase(Ease.OutBounce);
    }

    private void OnMouseExit()
    {
        TextObject.transform.DOScaleY(0, 0.5f);
    }
}
