using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthBarNotched : MonoBehaviour {

    public EntityHealth TargetHealth;
    public GameObject Notch;
    public float NotchPadding = 1.2f;

    public GameObject[] Notches;
    private int NumNotches;
    private int LastHealth = 0;

    private static HealthBarNotched Singleton;
   

    // Use this for initialization
    void Start () {
        if(!Singleton)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log(Time.time + "HEALTH BAR ALREAD EXISTS DESTROYING SELF");
        }
        DontDestroyOnLoad(gameObject);
        CreateNotches();
    }

    void OnEnable()
    {
        EntityHealth.OnPlayerHealthUpdate += UpdateNotches;
    }

    void OnDisable()
    {
        EntityHealth.OnPlayerHealthUpdate -= UpdateNotches;
    }

    private void CreateNotches()
    {
        if (TargetHealth && (int)TargetHealth.MaxHealth > NumNotches)
        {
            NumNotches = (int)TargetHealth.MaxHealth;
            LastHealth = NumNotches;
            Notches = new GameObject[NumNotches];
            float xPos = 0f;
            Color NotchColor = Color.red;
            for (int i = 0; i < NumNotches; ++i)
            {
                GameObject temp = Instantiate(Notch, this.transform);
                temp.transform.localPosition = new Vector3(-xPos, 0f, 0f);
                temp.GetComponent<SpriteRenderer>().color = NotchColor;
                Notches[i] = temp;
                xPos += NotchPadding;
                if (i + 1 > (NumNotches / 3) * 2)
                {
                    NotchColor = Color.yellow;
                }
                else if (i + 1 > NumNotches / 3)
                {
                    NotchColor = new Color(255f / 255f, 128f / 255f, 0f);
                }
            }
        }
    }

    public void UpdateNotches()
    {
        bool Shake = false;
        if ((int)TargetHealth.MaxHealth > NumNotches)
        {
            CreateNotches();
        }
        int CurrentHealth = (int)TargetHealth.CurrentHealth;
        if(CurrentHealth < LastHealth)
        {
            Shake = true;
            LastHealth = CurrentHealth;
        }
        //print(CurrentHealth);
        for (int i = 0; i < NumNotches; ++i)
        {
            if(i < CurrentHealth)
            {
                if(Shake)
                {
                    Notches[i].transform.DOComplete();
                    Notches[i].transform.DOShakePosition(0.1f);
                }
                Notches[i].SetActive(true);
            }
            else
            {
                Notches[i].SetActive(false);
            }
            
        }
    }

    public void DisableSprites()
    {
        foreach(GameObject Notch in Notches)
        {
            Notch.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void EnableSprites()
    {
        foreach (GameObject Notch in Notches)
        {
            Notch.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
