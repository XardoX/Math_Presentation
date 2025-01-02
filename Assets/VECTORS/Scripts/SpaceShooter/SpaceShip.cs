using System;
using System.Xml.Serialization;
using UnityEngine;

namespace MathPresentation.SpaceShooter
{
    public class SpaceShip : MonoBehaviour, IDamageable
    {
        public Action onDeath, 
            onKill;

        [SerializeField]
        private SpaceShipMovement movement;

        [SerializeField]
        private SpaceShipShooting shooting;

        private Vector3 startPostion;

        public SpaceShipShooting Shooting => shooting;
        public SpaceShipMovement Movement => movement;

        public void TakeDamage(int damage)
        {
            Death();
        }

        public void ResetPlayer()
        {

        }

        private void Death()
        {
            onDeath?.Invoke();
        }
    }
}