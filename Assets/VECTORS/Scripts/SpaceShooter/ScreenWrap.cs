using UnityEngine;

namespace MathPresentation.SpaceShooter
{
    public class ScreenWrap : MonoBehaviour
    {
        [SerializeField]
        private float objectWidth,
            objectHeight;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            objectWidth = objectWidth / Screen.width;
            objectHeight = objectHeight / Screen.height;
        }

        private void Update()
        {
            var viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            var newPosition = transform.position;
            bool shouldTeleport = false;

            if (viewportPosition.x < 0 - objectWidth)
            {
                newPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(1 + objectWidth, 0, 0)).x;
                shouldTeleport = true;
            }
            else if (viewportPosition.x > 1 + objectWidth)
            {
                newPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(0 - objectWidth, 0, 0)).x;
                shouldTeleport = true;
            }

            if (viewportPosition.y < 0 - objectHeight)
            {
                newPosition.y = mainCamera.ViewportToWorldPoint(new Vector3(0, 1 + objectHeight, 0)).y;
                shouldTeleport = true;
            }
            else if (viewportPosition.y > 1 + objectHeight)
            {
                newPosition.y = mainCamera.ViewportToWorldPoint(new Vector3(0, 0 - objectHeight, 0)).y;
                shouldTeleport = true;
            }

            if (shouldTeleport)
            {
                transform.position = newPosition;
            }
        }
    }
}