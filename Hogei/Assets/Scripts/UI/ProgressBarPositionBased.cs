using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProgressBarPositionBased : MonoBehaviour {

    public Transform ProgressBar = null;

    public float StartY = 0f;
    public float EndY = 0f;

    private float Diff = 0f;

    public bool DebugBar = false;
    private float DebugPercent = 1f;
    private EntityHealth PlayerHealth = null;

    private void Start()
    {
        Diff = StartY - EndY;
    }

    public void SetPercentage(float _Percent)
    {
        Mathf.Clamp(_Percent, 0f, 1f);
        Vector3 newPos = new Vector3(0f, StartY - Diff * (1f - _Percent), 0f);
        ProgressBar.DOLocalMove(newPos, 0.1f);
    }

    private void Update()
    {
        if(DebugBar)
        {
            DebugDecrease();
            DebugBar = false;
        }
        if (!PlayerHealth)
        {
            PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityHealth>();
        }
        else
        {
            float percent = PlayerHealth.CurrentHealth / PlayerHealth.MaxHealth;
            SetPercentage(percent);
        }
    }

    private void DebugDecrease()
    {
        DebugPercent -= 0.1f;
        SetPercentage(DebugPercent);
        
    }
}
