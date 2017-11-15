using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {

    public Transform progressBar;

    private EntityHealth PlayerHealth;

    public void SetPercentage(float _Percentage)
    {
        progressBar.localScale = new Vector3(progressBar.localScale.x, 2 * _Percentage, progressBar.localScale.z);
    }

    private void Update()
    {
        if (!PlayerHealth)
        {
            PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityHealth>();
        }
        else
        {
            float percentage = PlayerHealth.CurrentHealth/PlayerHealth.MaxHealth;
            SetPercentage(percentage);
        }
    }
}
