using UnityEngine;
using UnityEngine.Playables;

namespace MathPresentation.SpaceShooter.Timeline
{
    [System.Serializable]
    public class SpaceShipBehaviour : PlayableBehaviour
    {
        public float moveSpeed = 10f;
        public float rotationSpeed = 100f;
        public float boostMultiplier = 2f;
        public float bounceFactor = 0.8f;
        public float dampingFactor = 0.95f;
        public bool canMove = true;
        public bool canBoost = true;
        public bool damping = true;

        private SpaceShip spaceShip = null;
        public override void OnPlayableCreate(Playable playable)
        {
            base.OnPlayableCreate(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (spaceShip == null)
                spaceShip = playerData as SpaceShip;

            SpaceShipMovement spaceShipMovement = spaceShip.Movement;
            if (spaceShipMovement == null) return;

            float weight = info.weight;

            spaceShipMovement.MoveSpeed = Mathf.Lerp(spaceShipMovement.MoveSpeed, moveSpeed, weight);
            spaceShipMovement.RotationSpeed = Mathf.Lerp(spaceShipMovement.RotationSpeed, rotationSpeed, weight);
            spaceShipMovement.BoostMultiplier = Mathf.Lerp(spaceShipMovement.BoostMultiplier, boostMultiplier, weight);
            spaceShipMovement.BounceFactor = Mathf.Lerp(spaceShipMovement.BounceFactor, bounceFactor, weight);
            spaceShipMovement.DampingFactor = Mathf.Lerp(spaceShipMovement.DampingFactor, dampingFactor, weight);

            spaceShipMovement.CanMove = weight > 0.5f ? canMove : spaceShipMovement.CanMove;
            spaceShipMovement.CanBoost = weight > 0.5f ? canBoost : spaceShipMovement.CanBoost;
            spaceShipMovement.Damping = weight > 0.5f ? damping : spaceShipMovement.Damping;
        }

    }
}