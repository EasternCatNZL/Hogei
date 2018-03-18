using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CornShake : MonoBehaviour {
  
    public float ShakeLength = 1f;
    public float ShakeIntensity = 10f;
    private bool Used = false;

    private void OnTriggerEnter(Collider other)
    {
        transform.DOComplete();
        transform.DOShakeRotation(ShakeLength, ShakeIntensity);
        //Add item drop
    }
}
