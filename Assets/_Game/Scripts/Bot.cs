using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private float radius = 20;
    public Vector3 nextPosition;
    public NavMeshAgent myAgent;
    private IState currentState;
    public Character target;
    [SerializeField]public LayerMask characterLayer;
    protected override void Start()
    {
        base.Start();
        myAgent = GetComponent<NavMeshAgent>();
        nextPosition = transform.position;
        ChangeState(new PatrolState());
    }
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void ChangeState(IState newState)
    {
        if(currentState!= null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if(currentState!= null)
        {
            currentState.OnEnter(this);
        }
    }
    public void ChangeAnimRun()
    {
        ChangeAnim("run");
    }
    public void MoveRandom()
    {
        if (Vector3.Distance(nextPosition, transform.position) <= 1.5f)
        {
            nextPosition = RandomPoint(transform.position, radius);
            myAgent.SetDestination(nextPosition);
        }
    }
    public static Vector3 RandomPoint(Vector3 startPoint, float radius)
    {
        Vector3 dir = Random.insideUnitSphere * radius;
        dir += startPoint;
        NavMeshHit hit;
        Vector3 finalPos = Vector3.zero;
        if (NavMesh.SamplePosition(dir, out hit, radius, 1))
        {
            finalPos = hit.position;
        }
        return finalPos;
    }
    public void Findtarget()
    {
        Collider[] results = new Collider[10];
        Physics.OverlapSphereNonAlloc(transform.position, 6, results, characterLayer);
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] != null && results[i].name.Equals(gameObject.name))
            {
                target = results[i].GetComponent<Character>();
            }
        }
    }
}
