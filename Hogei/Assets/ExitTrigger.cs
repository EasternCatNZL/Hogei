using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour {

    public List<GameObject> enemyList = new List<GameObject>();
    private Rigidbody Anchor;

    // Use this for initialization
    void Start () {
        Anchor = GetComponentInChildren<Rigidbody>();
        if(enemyList.Count <= 0)
        {
            DropAnchor();
        }
    }

    private void OnEnable()
    {
        EntityHealth.OnDeath += CheckExitClear;
    }

    private void OnDisable()
    {
        EntityHealth.OnDeath -= CheckExitClear;
    }

    public void CheckExitClear()
    {
        bool EnemiesCleared = true;
        for (int i = 0; i < enemyList.Count; ++i)
        {
            if (enemyList[i] != null && enemyList[i].activeSelf == true)
            {
                Debug.Log("Enemies are not cleared");
                EnemiesCleared = false;
                break;
            }
        }
        if (EnemiesCleared)
        {
            Debug.Log("Enemies cleared");
            DropAnchor();
        }
    }

    private void DropAnchor()
    {
        Anchor.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
}
