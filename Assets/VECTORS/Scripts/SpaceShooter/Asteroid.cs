using UnityEngine;
using System;

namespace MathPresentation.SpaceShooter
{
    public class Asteroid : MonoBehaviour
    {
        public event Action OnAsteroidDestroyed;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                DestroyAsteroid();
            }
        }

        private void DestroyAsteroid()
        {
            OnAsteroidDestroyed?.Invoke();

            Destroy(gameObject);
        }
    }
}