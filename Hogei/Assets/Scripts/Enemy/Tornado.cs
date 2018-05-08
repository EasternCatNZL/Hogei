using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Tornado : MonoBehaviour {

    public float Knockback = 1.0f;
    public Transform[] Points;
    private int destPoints;
    private NavMeshAgent Agent;

    // Use this for initialization
    void Start () {
        Agent = GetComponent<NavMeshAgent>();
        Agent.autoBraking = false;
        GoToNextPoint();
		
	}
	
	// Update is called once per frame
	void Update () {

        if(!Agent.pathPending && Agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }	
	}

    void GoToNextPoint()
    {
        if(Points.Length == 0)
        {
            return;
        }

        Agent.destination = Points[destPoints].position;
        destPoints = (destPoints + 1) % Points.Length;
    }

    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Vector3 HitPos = col.contacts[0].point;
            Vector3 Pos = col.transform.position;
            Vector3 Temp = Pos - HitPos;
            Temp.Scale(new Vector3(1, 0, 1));

            col.rigidbody.DOJump(col.transform.position - Temp * -Knockback, 1.0f, 0, 0.5f, false);
        }
    }
    
}
