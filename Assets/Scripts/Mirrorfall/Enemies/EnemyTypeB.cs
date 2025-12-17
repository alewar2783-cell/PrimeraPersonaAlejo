using UnityEngine;
using Mirrorfall.Interfaces;

namespace Mirrorfall.Enemies
{
    public class EnemyTypeB : EnemyAI
    {
        protected override void Start()
        {
            base.Start();
            // Project Specs: Speed 15 (Normal), Dmg 25, HP 150
            agent.speed = 15f; 
            damage = 25;
            health = 150;
        }

        protected override void PerformAttack()
        {
            Debug.Log($"{name} hits HARD with Heavy Melee!");
            IDamageable playerHealth = player.GetComponent<IDamageable>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
