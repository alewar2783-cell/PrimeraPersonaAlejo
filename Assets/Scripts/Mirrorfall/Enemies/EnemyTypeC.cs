using UnityEngine;
using Mirrorfall.Interfaces;

namespace Mirrorfall.Enemies
{
    public class EnemyTypeC : EnemyAI
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;

        protected override void Start()
        {
            base.Start();
            // Project Specs: Speed 10 (Low), Dmg 15, HP 50
            agent.speed = 10f; 
            damage = 15;
            health = 50;
            attackRange = 15f; // Ranged need more distance
        }

        // Override HandleChase to maintain distance
        protected override void HandleChase()
        {
            float distance = Vector3.Distance(transform.position, player.position);
            
            // If too close, maybe back away? For now, just stop at attack range
            if (distance <= attackRange)
            {
                agent.ResetPath();
                SetState(State.Attack);
            }
            else
            {
                agent.SetDestination(player.position);
            }
        }

        protected override void PerformAttack()
        {
            if (projectilePrefab && firePoint)
            {
                Debug.Log($"{name} fires projectile!");
                GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                // Assumption: Projectile has its own script to move and deal damage
                // For simplicity, we can let the projectile handle collision or impulse here
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb) rb.AddForce(firePoint.forward * 20f, ForceMode.Impulse);
                
                // Add a simple Cleanup
                Destroy(bullet, 5f);
            }
            else
            {
                Debug.LogWarning("EnemyTypeC missing projectile prefab or fire point!");
            }
        }
    }
}
