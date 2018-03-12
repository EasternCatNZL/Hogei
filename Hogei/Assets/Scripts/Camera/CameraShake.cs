using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    //public GameObject World;

    public Transform camTransform;

    public float ShakeDuration = 0.0f;
    private float OriginalShakeDuration;
    public float ShakeAmount = 1.0f;
    public float DecreaseFactor = 1.0f;

    public bool ShakeTrue = false;

    private Vector3 OriginalPos;

    // Use this for initialization
    void Start()
    {
        OriginalShakeDuration = ShakeDuration;
    }

    void OnEnable()
    {
        OriginalPos = camTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ShakeTrue = true;
            print("pressed");
        }
        ShakeCamera();
    
    }

    public void ShakeCamera()
    {
        //World.transform.DOShakePosition(3.0f, 1.0f);
        //Camera.main.transform.DOShakePosition(3.0f);
        
        if(ShakeTrue == true)
        {
            if(ShakeDuration >= 0.0f)
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, OriginalPos + Random.insideUnitSphere * ShakeAmount, Time.deltaTime * 3);
                ShakeDuration -= Time.deltaTime * DecreaseFactor;
            }
            else
            {
                ShakeDuration = OriginalShakeDuration;
                camTransform.localPosition = OriginalPos;
                ShakeTrue = false;
            }
        }
    }
}
