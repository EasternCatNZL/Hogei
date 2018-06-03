using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {

    public Transform progressBar;

    public EntityHealth EntityHealth;

    public List<Sprite> BorderSprites; 

    public void SetPercentage(float _Percentage)
    {
        progressBar.localScale = new Vector3(_Percentage, progressBar.localScale.y , progressBar.localScale.z);
    }

    private void Update()
    {
        if(EntityHealth)
        {
            float percentage = EntityHealth.CurrentHealth/ EntityHealth.MaxHealth;
            SetPercentage(percentage);
            float _Index = Mathf.Ceil(percentage / (1f / BorderSprites.Count)) - 1;
            print(percentage + " " + _Index);         
            GetComponent<SpriteRenderer>().sprite = BorderSprites[(int)_Index];

        }
    }

    public void DisableSprites()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void EnableSprites()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

}
