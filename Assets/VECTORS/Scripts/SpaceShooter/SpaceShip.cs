using System;
using System.Xml.Serialization;
using UnityEngine;

namespace MathPresentation.SpaceShooter
{
    public class SpaceShip : MonoBehaviour, IDamageable
    {
        public Action onDeath, 
            onKill;

        private Vector3 startPostion;

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