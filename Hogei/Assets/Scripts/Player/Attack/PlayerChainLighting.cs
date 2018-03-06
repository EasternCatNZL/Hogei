using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChainLighting : MonoBehaviour {

    public float InitalRange = 10f;
    public int NumberChains = 3;
    public float ChainRange = 5f;

    private RaycastHit InitalHitInfo;
    private Vector3[] ChainPositions;

	// Use this for initialization
	void Start () {
        ChainPositions = new Vector3[NumberChains];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FireChainLighting()
    {
        Vector3 Direction = MouseTarget.GetWorldMousePos() - transform.position;
        Ray InitalRay = new Ray(transform.position, Direction);
        Debug.DrawRay(InitalRay.origin, InitalRay.direction, Color.cyan);
        if(Physics.Raycast(InitalRay, out InitalHitInfo, InitalRange))
        {
            GameObject objectHit = InitalHitInfo.collider.gameObject;
        }
    }

    void ChainToEnemy(Vector3 _ChainOrigin, int _ChainsLeft)
    {
        if(_ChainsLeft <= 0)
        {
            return;
        }
        GameObject ClosestEnemy = null;
        float ShortestDistance = 0f;
        foreach(GameObject Enemy in SceneHandler.GetSceneHandler().GetActiveList())
        {
            float NewDistanceSqr = (Enemy.transform.position - _ChainOrigin).sqrMagnitude;
            if (ClosestEnemy == null && NewDistanceSqr < ChainRange * ChainRange)
            {
                ClosestEnemy = Enemy;
                ShortestDistance = (ClosestEnemy.transform.position - _ChainOrigin).sqrMagnitude;
                continue;
            }
            
            if (NewDistanceSqr < ShortestDistance)
            {
                ClosestEnemy = Enemy;
                ShortestDistance = NewDistanceSqr;
                continue;
            }
        }
        ChainToEnemy(ClosestEnemy.transform.position, _ChainsLeft - 1);
    }
}
