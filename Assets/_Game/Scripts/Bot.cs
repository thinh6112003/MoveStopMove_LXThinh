using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private float radius = 20;
    Vector3 nextPosition;
    NavMeshAgent myAgent;
    private IState currentState;
    private string currentAnimName;
    [SerializeField] private Animator anim;
    void Start()
    {
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
    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public void ChangeAnimRun()
    {
        ChangeAnim("run");
    }
    public void moveRandom()
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
}
