// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyPatrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        
        if (points.Length == 0)
            return;
        anim.SetBool("walk", true);
       
        agent.destination = points[destPoint].position;

        
        destPoint = (destPoint + 1) % points.Length;
        anim.SetBool("walk", true);
    }


    void Update()
    {
       
        
        if (!agent.pathPending && agent.remainingDistance < 3f)
        {
            GotoNextPoint();
        }
    }


}
