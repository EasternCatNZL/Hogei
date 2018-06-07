using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
public class FunctionOnMouseDown : MonoBehaviour
{

    [Header("Glitter and Sparkles")]
    public bool ScaleUpOnHover = false;
    public float ScaleDuration = 0.3f;
    public float ScaleMultiplier = 1.3f;
    private Vector3 OriginalScale;

    public UnityEvent functionToCall;

    void Start()
    {
        if (ScaleUpOnHover)
        {
            OriginalScale = transform.localScale;
        }
    }


    //Unity function called when collider is clicked down on
    void OnMouseDown()
    {
        functionToCall.Invoke();
    }

    void OnMouseEnter()
    {
        if (ScaleUpOnHover)
        {
            transform.DOScale(OriginalScale * ScaleMultiplier, ScaleDuration).SetEase(Ease.OutBounce);
        }
    }

    void OnMouseExit()
    {
        if (ScaleUpOnHover)
        {
            transform.DOScale(OriginalScale, ScaleDuration);
        }
    }
}
