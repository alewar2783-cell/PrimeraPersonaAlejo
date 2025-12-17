using UnityEngine;
using Mirrorfall.Interfaces;

namespace Mirrorfall.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int damageAmount = 15;
        [SerializeField] private float lifeTime = 3f;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Try to get IDamageable from the object we hit
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }

            // Destroy bullet on impact
            Destroy(gameObject);
        }
    }
}
