using System.Collections;
using UnityEngine;

namespace MathPresentation.SpaceShooter
{
    public class AsteroidController : MonoBehaviour
    {
        [Header("Asteroid Settings")]
        [SerializeField]
        private GameObject asteroidPrefab;

        [SerializeField]
        private int maxAsteroids = 10;

        [SerializeField]
        private float spawnRate = 2f;

        [Header("Spawn Area Settings")]
        [SerializeField]
        private Vector2 spawnAreaSize = new Vector2(20f, 20f);

        [SerializeField]
        private Vector2 spawnAreaOffset = Vector2.zero;

        [Header("Asteroid Properties")]
        [SerializeField]
        private float minAsteroidSpeed = 2f;
        [SerializeField]
        private float maxAsteroidSpeed = 5f;
        [SerializeField]
        private float asteroidLifetime = 10f;

        private int currentAsteroidCount = 0;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;

            StartCoroutine(SpawnAsteroids());
        }

        private IEnumerator SpawnAsteroids()
        {
            while (true)
            {
                if (currentAsteroidCount < maxAsteroids)
                {
                    SpawnAsteroid();
                }

                yield return new WaitForSeconds(spawnRate);
            }
        }

        private void SpawnAsteroid()
        {
            if (currentAsteroidCount >= maxAsteroids)
                return;

            var spawnPosition = GetRandomPositionOutsideCamera();

            var asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            var rb = asteroid.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                var directionToScreen = GetRandomDirectionTowardScreen(spawnPosition);
                var randomSpeed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);
                rb.linearVelocity = directionToScreen * randomSpeed;
            }

            Destroy(asteroid, asteroidLifetime);

            currentAsteroidCount++;

            asteroid.GetComponent<Asteroid>().OnAsteroidDestroyed += HandleAsteroidDestroyed;
        }

        private Vector2 GetRandomPositionOutsideCamera()
        {
            var cameraPosition = mainCamera.transform.position;
            var screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            float x, y;

            int side = Random.Range(0, 4);
            switch (side)
            {
                case 0: // Top
                    x = Random.Range(cameraPosition.x - screenBounds.x, cameraPosition.x + screenBounds.x);
                    y = cameraPosition.y + screenBounds.y + 1f;
                    break;
                case 1: // Bottom
                    x = Random.Range(cameraPosition.x - screenBounds.x, cameraPosition.x + screenBounds.x);
                    y = cameraPosition.y - screenBounds.y - 1f;
                    break;
                case 2: // Left
                    x = cameraPosition.x - screenBounds.x - 1f;
                    y = Random.Range(cameraPosition.y - screenBounds.y, cameraPosition.y + screenBounds.y);
                    break;
                default: // Right
                    x = cameraPosition.x + screenBounds.x + 1f;
                    y = Random.Range(cameraPosition.y - screenBounds.y, cameraPosition.y + screenBounds.y);
                    break;
            }
            return new Vector2(x, y);
        }

        private Vector2 GetRandomDirectionTowardScreen(Vector2 spawnPosition)
        {
            var screenMin = mainCamera.ScreenToWorldPoint(Vector3.zero);
            var screenMax = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            var targetPoint = new Vector2(
                Random.Range(screenMin.x, screenMax.x),
                Random.Range(screenMin.y, screenMax.y)
            );

            return (targetPoint - spawnPosition).normalized;
        }


        private void HandleAsteroidDestroyed()
        {
            currentAsteroidCount--;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(spawnAreaOffset, spawnAreaSize);

            Gizmos.color = Color.yellow;
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Gizmos.DrawWireCube(cameraPosition, screenBounds * 2f);
        }
    }
}