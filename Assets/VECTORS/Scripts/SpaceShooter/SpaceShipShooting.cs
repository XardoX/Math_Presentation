using UnityEngine;
using UnityEngine.InputSystem;

namespace MathPresentation.SpaceShooter
{
    public class SpaceShipShooting : MonoBehaviour
    {
        [Header("Shooting Settings")]
        [SerializeField]
        private Bullet bulletPrefab;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private float bulletSpeed = 20f;

        [SerializeField]
        private float fireRate = 0.5f;

        private float lastShotTime = 0f;

        private bool isFiring = false;

        private void Update()
        {
            if (isFiring && Time.time >= lastShotTime + fireRate)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }

        private void Shoot()
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            bullet.Shoot(bulletSpeed);
        }

        private void OnFire(InputValue value)
        {
            isFiring = value.isPressed;
        }
    }
}