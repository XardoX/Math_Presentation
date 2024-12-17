using MyBox;
using System;
using Unity.VisualScripting;
using UnityEngine;
namespace MathPresentation.SpaceShooter
{
    [Serializable, Inspectable]
    public class SpaceShipMovement : MonoBehaviour
    {
        [Foldout("Movement Settings", true)]
        [SerializeField]
        [Inspectable]
        private float moveSpeed = 10f,
            rotationSpeed = 100f,
            boostMultiplier = 2f;

        [Foldout("Physics Settings", true)]
        [SerializeField]
        private float bounceFactor = 0.8f;

        private Vector3 velocity;
        private bool isBoosting = false;

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleBoost();
        }

        private void HandleMovement()
        {

            if (Input.GetKey(KeyCode.W))
            {
                velocity += transform.up * moveSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S))
            {
                velocity -= transform.up * moveSpeed * Time.deltaTime;
            }


            transform.position += velocity * Time.deltaTime;
        }

        private void HandleRotation()
        {
            float rotationInput = 0;


            if (Input.GetKey(KeyCode.A))
            {
                rotationInput = 1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rotationInput = -1;
            }

            transform.Rotate(0, 0, rotationInput * rotationSpeed * Time.deltaTime);
        }

        private void HandleBoost()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isBoosting = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isBoosting = false;
            }

            if (isBoosting)
            {
                velocity = velocity.normalized * moveSpeed * boostMultiplier;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector2 normal = collision.contacts[0].normal;
            velocity = Vector2.Reflect(velocity, normal) * bounceFactor;
        }
    }
}
