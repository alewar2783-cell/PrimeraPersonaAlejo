using UnityEngine;
using Mirrorfall.Core;
using Mirrorfall.Interfaces;

namespace Mirrorfall.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            Debug.Log($"Player took {amount} damage. Current HP: {currentHealth}");

            if (currentHealth <= 0)
            {
                MirrorfallGameManager.Instance.PlayerDied();
            }
        }

        // Optional: Add healing
        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
    }
}
