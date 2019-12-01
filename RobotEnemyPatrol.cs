using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class RobotEnemyPatrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    

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
        

        agent.destination = points[destPoint].position;


        destPoint = (destPoint + 1) % points.Length;
        
    }


    void Update()
    {


        if (!agent.pathPending && agent.remainingDistance < 3f)
        {
            GotoNextPoint();
        }
    }


}
