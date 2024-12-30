using Extensions;
using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MathPresentation.SpaceShooter
{
    public class SpaceShipMovement : MonoBehaviour
    {
        [Foldout("Movement Settings", true)]
        [SerializeField]
        private float moveSpeed = 10f,
            maxMoveSpeed = 10f,
            rotationSpeed = 100f,
            boostMultiplier = 2f;

        [Foldout("Physics Settings", true)]
        [SerializeField]
        private float bounceFactor = 0.8f,
            dampingFactor = 0.95f;

        [SerializeField]
        [Foldout("Settings")]
        private bool canMove = true,
            canBoost = true,
            damping = true;


        private Vector3 velocity;

        private Vector2 moveInput;

        private bool isBoosting = false;

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
        public float BoostMultiplier { get => boostMultiplier; set => boostMultiplier = value; }
        public float BounceFactor { get => bounceFactor; set => bounceFactor = value; }
        public float DampingFactor { get => dampingFactor; set => dampingFactor = value; }
        public bool CanMove { get => canMove; set => canMove = value; }
        public bool CanBoost { get => canBoost; set => canBoost = value; }
        public bool Damping { get => damping; set => damping = value; }

        private void Update()
        {
            if (canMove == false) return;

            HandleMovement();
            HandleRotation();
            HandleBoost();
            ApplyDamping();
        }

        private void HandleMovement()
        {
            velocity += moveInput.y * transform.up * moveSpeed * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxMoveSpeed);

            transform.position += velocity * Time.deltaTime;
        }

        private void HandleRotation()
        {
            transform.Rotate(0, 0, -moveInput.x * rotationSpeed * Time.deltaTime);
        }

        private void HandleBoost()
        {
            if (isBoosting && canBoost)
            {
                velocity = velocity.normalized * moveSpeed * boostMultiplier;
            }
        }

        private void ApplyDamping()
        {
            if (moveInput == Vector2.zero && !isBoosting && damping)
            {
                velocity *= dampingFactor;
                if (velocity.magnitude < 0.01f)
                {
                    velocity = Vector3.zero;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Vector2 normal = collision.contacts[0].normal;
            //velocity = Vector2.Reflect(velocity, normal) * bounceFactor;
        }

        private void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
        }

        private void OnBoost(InputValue value)
        {
            isBoosting = value.isPressed;
        }
    }
}
