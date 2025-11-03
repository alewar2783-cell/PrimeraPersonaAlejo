using UnityEngine;
using UnityEngine.AI;

public class AnimacionZombie : MonoBehaviour
{
    public Animator _animator;
    private NavMeshAgent ZombienNavMeshAgent;
    public float detectionRadius = 15f;
    public float stopDistance = 2f; 

    [SerializeField] Transform player;
    [SerializeField] float chaseInterval = 0.5f;

    void Start()
    {
        _animator = GetComponent<Animator>();

        ZombienNavMeshAgent = GetComponent<NavMeshAgent>(); 

        player = GameObject.FindGameObjectWithTag("Player").transform;

        ZombienNavMeshAgent.stoppingDistance = stopDistance;

        InvokeRepeating(nameof(SetDestination), 1f, chaseInterval);


        _animator.SetBool("Corriendo", true);
        

        SetDestination();
    }

    void Update()
    {
      if (player == null) return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius)
        {
            ZombienNavMeshAgent.isStopped = false;
            _animator.SetBool("Corriendo", true);
        }
        else
        {
            ZombienNavMeshAgent.isStopped = true;
            _animator.SetBool("Corriendo", false);
        }
    }

    private void SetDestination()
    { 
        ZombienNavMeshAgent.SetDestination(player.position); 
        Debug.Log("Persiguiendo al jugador");
    }
}
