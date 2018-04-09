using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChainLighting : Weapon
{

    public float InitalRange = 10f;
    public int NumberChains = 3;
    public float ChainRange = 5f;
    public int EnemyLayerNum = 10;
    [Tooltip("Time between ticks")]
    public float TickTime = 0.5f;
    [Tooltip("Damage done per tick")]
    public int Damage = 1;

    private RaycastHit InitalHitInfo;
    public Vector3[] ChainPositions;
    private LineRenderer LineRend;
    public List<GameObject> ChainedEnemies;
    private Vector3 LastChainPos;
    private float LastTick;

    // Use this for initialization
    void Start()
    {
        Type = WeaponTypes.Lighting;
        LastTick = Time.time;
        ChainPositions = new Vector3[NumberChains + 2];
        ChainedEnemies = new List<GameObject>();
        if (GetComponent<LineRenderer>())
        {
            LineRend = GetComponent<LineRenderer>();
            LineRend.positionCount = 2 + NumberChains;
        }
        else
        {
            Debug.Log(System.DateTime.Now + " : There is no line renderer on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            foreach(GameObject Enemy in ChainedEnemies)
            {
                if (Enemy)
                {
                    Enemy.GetComponent<EntityHealth>().SetStatusEffect(EntityHealth.StatusEffects.CHAINLIGHTING, false);
                }
            }
            LineRend.enabled = true;
            ChainedEnemies.Clear();
            UseWeapon();
            DrawLighting();
            DamageEnemies();
        }
        else
        {
            LineRend.enabled = false;
        }

    }

    public override void UseWeapon()
    {
        int Enemylayer = 1 << EnemyLayerNum;
        Vector3 Direction = MouseTarget.GetWorldMousePos() - transform.position;
        Direction.y = 0f;
        Ray InitalRay = new Ray(transform.position, Direction);
        Debug.DrawRay(InitalRay.origin, InitalRay.direction, Color.cyan);
        if (Physics.Raycast(InitalRay, out InitalHitInfo, InitalRange, Enemylayer))
        {
            GameObject objectHit = InitalHitInfo.collider.gameObject;
            LineRend.positionCount = 2 + NumberChains;
            ChainPositions[0] = transform.position;
            ChainPositions[1] = objectHit.transform.position;
            ChainedEnemies.Add(objectHit);
            ChainToEnemy(objectHit.transform.position, NumberChains);
        }
        else
        {
            LastChainPos = InitalRay.origin + InitalRay.direction * InitalRange;
            LineRend.positionCount = 2;
            ChainPositions[0] = transform.position;
            ChainPositions[1] = LastChainPos;
        }
    }

    void ChainToEnemy(Vector3 _ChainOrigin, int _ChainsLeft)
    {
        if (_ChainsLeft <= 0)
        {
            return;
        }
        GameObject ClosestEnemy = null;
        float ShortestDistance = 10000f;
        foreach (GameObject Enemy in SceneHandler.GetSceneHandler().GetActiveList())
        {
            float NewDistanceSqr = (Enemy.transform.position - _ChainOrigin).sqrMagnitude;
            if (!Enemy.GetComponent<EntityHealth>().GetStatusEffect(EntityHealth.StatusEffects.CHAINLIGHTING) && NewDistanceSqr < ShortestDistance && NewDistanceSqr < ChainRange * ChainRange)
            {
                ClosestEnemy = Enemy;
                ShortestDistance = NewDistanceSqr;
                continue;
            }
        }
        if (ClosestEnemy)
        {
            ClosestEnemy.GetComponent<EntityHealth>().SetStatusEffect(EntityHealth.StatusEffects.CHAINLIGHTING, true);
            //Add enemy position to ChainPositions
            ChainPositions[NumberChains - _ChainsLeft + 2] = ClosestEnemy.transform.position;
            ChainedEnemies.Add(ClosestEnemy);
            //Chain again
            ChainToEnemy(ClosestEnemy.transform.position, _ChainsLeft - 1);
        }
        else
        {
            print("No more Enemies to chain too");
            LastChainPos = _ChainOrigin;
            for (int i = NumberChains - _ChainsLeft + 2; i < NumberChains + 2; ++i)
            {
                ChainPositions[i] = LastChainPos;
            }
        }
    }

    void DrawLighting()
    {
        LineRend.SetPositions(ChainPositions);
    }

    void DamageEnemies()
    {
        if (Time.time - LastTick > TickTime)
        {
            foreach (GameObject Enemy in ChainedEnemies)
            {
                Enemy.GetComponent<EntityHealth>().DecreaseHealth(Damage);
            }
            LastTick = Time.time;
        }

    }

    public override void ApplyUpgrade(SoupUpgrade _Upgrade)
    {
        throw new NotImplementedException();
    }
}
