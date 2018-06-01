using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour {

    public bool WaitForTrigger = false;
    public List<GameObject> enemyList = new List<GameObject>();
    private Rigidbody Anchor;

    // Use this for initialization
    void Start () {
        Anchor = GetComponentInChildren<Rigidbody>();
        if(!WaitForTrigger && enemyList.Count <= 0)
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
        if (!WaitForTrigger)
        {
            bool EnemiesCleared = true;
            for (int i = 0; i < enemyList.Count; ++i)
            {
                if (enemyList[i] != null && enemyList[i].activeSelf == true)
                {
                    EnemiesCleared = false;
                    break;
                }
            }
            if (EnemiesCleared)
            {
                DropAnchor();
            }
        }
    }

    public void DropAnchor()
    {
        Anchor.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
}
