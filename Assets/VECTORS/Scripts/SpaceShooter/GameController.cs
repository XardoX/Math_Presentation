using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MathPresentation.SpaceShooter
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private SpaceShip spaceShip;
        private float gameTimeRemaining;

        private int score;
        private bool isGameOver;
        private bool isGameStarted;

        private void Start()
        {
            score = 0;
            isGameOver = false;
            isGameStarted = false;

            Time.timeScale = 0f;
        }

        private void Update()
        {
        }

       

        public void AddScore(int points)
        {
            if (!isGameOver)
            {
                score += points;
            }
        }

        public void GameOver()
        {
            if (isGameOver) return;

            isGameOver = true;
            Time.timeScale = 0f;
        }

        public void StartGame()
        {
            isGameStarted = true;
            Time.timeScale = 1f;
        }

        private void RestartGame()
        {

        }
    }
}