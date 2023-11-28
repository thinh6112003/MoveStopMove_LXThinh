using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Bot : Character
{
    [SerializeField]private Rigidbody rigidbody;
    [SerializeField]private LayerMask characterLayer;
    private float radius = 20;
    private int layerDefault= 1 << 0;
    private IState currentState;

    public TextMeshProUGUI textName;
    public GameObject indicator;
    public Vector3 nextPosition;
    public NavMeshAgent myAgent;
    protected override void Start()
    {
        int weaponTypeRandom = Random.Range(0, 2);
        weaponData = DataManager.Instance.GetWeaponData((WeaponType)weaponTypeRandom);
        myAgent = GetComponent<NavMeshAgent>();
        nextPosition = transform.position;
        ChangeState(new PatrolState());
        base.Start();
    }
    void Update()
    {
        if(indicator.active== true)
        {
            indicator.transform.rotation = Quaternion.Euler(90,0,0);
        }
        if (isDead) return;
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
        SetAnimation(AnimationType.RUN);
    }
    public void ChangeAnimIdle()
    {
        SetAnimation(AnimationType.IDLE);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(constr.CHARACTER))
        {
            botInRange.Add(other.gameObject);
        }
        if (other.CompareTag(constr.WEAPON) && other.GetComponent<Bullet>().shooter.name != gameObject.name)
        {
            SetAnimation(AnimationType.DEAD);
            myAgent.destination = gameObject.transform.position;
            myAgent.isStopped = true;
            rigidbody.velocity= Vector3.zero;
            Invoke(nameof(DestroyCharacter), 2);
            gameObject.layer = layerDefault;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(constr.CHARACTER))
        {
            for (int i = 0; i < botInRange.Count; i++)
            {
                if (botInRange[i]!= null&& botInRange[i].name == other.name)
                {
                    botInRange.RemoveAt(i);
                }
            }
        }
    }
    private void DestroyCharacter()
    {
        Destroy(gameObject);
    }
}
