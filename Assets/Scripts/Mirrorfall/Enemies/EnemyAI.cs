using UnityEngine;
using UnityEngine.AI;
using Mirrorfall.Core;
using Mirrorfall.Interfaces;

namespace Mirrorfall.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyAI : MonoBehaviour, IDamageable
    {
        [Header("Stats")]
        [SerializeField] protected int health = 100;
        [SerializeField] protected int damage = 15;
        [SerializeField] protected float detectionRange = 10f;
        [SerializeField] protected float attackRange = 2f;
        [SerializeField] protected float attackCooldown = 1.5f;

        protected NavMeshAgent agent;
        protected Transform player;
        protected float lastAttackTime;

        protected enum State { Idle, Patrol, Chase, Attack }
        protected State currentState;

        protected virtual void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            // Find player - Assuming Player tag is "Player"
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj) player = playerObj.transform;
            
            // Register with GameManager
            MirrorfallGameManager.Instance.RegisterEnemy();
            
            SetState(State.Idle);
        }

        protected virtual void Update()
        {
            if (MirrorfallGameManager.Instance.CurrentState != MirrorfallGameManager.GameState.Gameplay) return;
            if (player == null) return;

            switch (currentState)
            {
                case State.Idle:
                    HandleIdle();
                    break;
                case State.Patrol: // Simplified to Idle for now, can be expanded
                    HandleIdle();
                    break;
                case State.Chase:
                    HandleChase();
                    break;
                case State.Attack:
                    HandleAttack();
                    break;
            }
        }

        protected virtual void HandleIdle()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= detectionRange)
            {
                SetState(State.Chase);
            }
        }

        protected virtual void HandleChase()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            agent.SetDestination(player.position);

            if (distance <= attackRange)
            {
                SetState(State.Attack);
            }
            if (distance > detectionRange * 1.5f) // Lose aggro
            {
                SetState(State.Idle);
            }
        }

        protected virtual void HandleAttack()
        {
            agent.SetDestination(transform.position); // Stock moving
            transform.LookAt(player);

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > attackRange)
            {
                SetState(State.Chase);
                return;
            }

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                PerformAttack();
                lastAttackTime = Time.time;
            }
        }

        protected abstract void PerformAttack();

        protected void SetState(State newState)
        {
            currentState = newState;
        }

        public virtual void TakeDamage(int amount)
        {
            health -= amount;
            // Aggro on hit
            if (currentState == State.Idle) SetState(State.Chase);

            if (health <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            MirrorfallGameManager.Instance.EnemyDefeated();
            Destroy(gameObject);
        }
    }
}
