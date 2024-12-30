using MyBox;
using PlasticGui.WorkspaceWindow.Locks;
using UnityEngine;

namespace MathPresentation.SpaceShooter
{
    public class Bullet : MonoBehaviour, IDamageable
    {
        [Header("Bullet Settings")]
        [SerializeField]
        private float lifetime = 3f;

        [SerializeField]
        private int damage = 1;

        [SerializeField]
        private float ignoreCollisionDuration = 0.1f;

        [Header("References")]
        [SerializeField]
        private Rigidbody2D rb;

        private bool canDamage = false;

        public void TakeDamage(int damage)
        {
            Destroy(gameObject);
        }

        public void Shoot(float speed)
        {
            rb.linearVelocity = transform.up * speed;
            Invoke(nameof(EnableDamage), ignoreCollisionDuration);
        }

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        private void EnableDamage()
        {
            canDamage = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (canDamage == false) return;
            
            
            var damageable = collision.gameObject.GetComponentInParent<IDamageable>();
            damageable?.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
