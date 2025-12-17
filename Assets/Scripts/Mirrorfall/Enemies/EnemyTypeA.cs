using UnityEngine;
using Mirrorfall.Interfaces;

namespace Mirrorfall.Enemies
{
    public class EnemyTypeA : EnemyAI
    {
        protected override void Start()
        {
            base.Start();
            // Project Specs: Speed 20 (high), Dmg 15, HP 100
            agent.speed = 20f; // Note: 20 is very fast for Unity units, might need tuning in Inspector.
            damage = 15;
            health = 100;
        }

        protected override void PerformAttack()
        {
            Debug.Log($"{name} attacks Player with Melee!");
            IDamageable playerHealth = player.GetComponent<IDamageable>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
