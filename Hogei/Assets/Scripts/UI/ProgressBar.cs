using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {

    public bool EnemyHealthBar = false;
    public Transform progressBar;
    public Transform backgroundBar;

    public EntityHealth EntityHealth;

    public List<Sprite> BorderSprites; 

    private static ProgressBar Singleton;

    void Start()
    {
        if (!EnemyHealthBar)
        {
            if (Singleton == null)
            {
                Singleton = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetPercentage(float _Percentage)
    {
        progressBar.localScale = new Vector3(_Percentage/* - 0.01f*/, progressBar.localScale.y , progressBar.localScale.z) ;
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
            if (BorderSprites.Count > 0)
            {
                float _Index = Mathf.Ceil(percentage / (1f / BorderSprites.Count)) - 1;
                //print(percentage + " " + _Index);         
                if (_Index < 0) _Index = 0;
                else if (_Index >= BorderSprites.Count) _Index = BorderSprites.Count - 1;
                GetComponent<SpriteRenderer>().sprite = BorderSprites[(int)_Index];
            }

        }
    }

    public void DisableSprites()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        progressBar.GetComponent<SpriteRenderer>().enabled = false;
        backgroundBar.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void EnableSprites()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        progressBar.GetComponent<SpriteRenderer>().enabled = true;
        backgroundBar.GetComponent<SpriteRenderer>().enabled = true;
    }

}
