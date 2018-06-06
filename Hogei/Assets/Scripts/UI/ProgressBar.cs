using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {

    public Transform progressBar;

    public EntityHealth EntityHealth;

    public List<Sprite> BorderSprites; 

    private static ProgressBar Singleton;

    void Start()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPercentage(float _Percentage)
    {
        progressBar.localScale = new Vector3(_Percentage - 0.01f, progressBar.localScale.y , progressBar.localScale.z) ;
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        UpdateHealthBar();
    }

    //update health bar
    private void UpdateHealthBar()
    {
        if (EntityHealth)
        {
            float percentage = EntityHealth.CurrentHealth / EntityHealth.MaxHealth;
            SetPercentage(percentage);
            float _Index = Mathf.Ceil(percentage / (1f / BorderSprites.Count)) - 1;
            //print(percentage + " " + _Index);         
            GetComponent<SpriteRenderer>().sprite = BorderSprites[(int)_Index];

        }
    }

    public void DisableSprites()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        progressBar.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void EnableSprites()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        progressBar.GetComponent<SpriteRenderer>().enabled = true;
    }

}
