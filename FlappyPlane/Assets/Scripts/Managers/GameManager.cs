using Controllers;
using Player;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameObject gameOverMenu;
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            gameOverMenu.SetActive(false);
        }

        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
            _playerHealth.OnDeath += EndGame;
        }

        private void EndGame()
        {
            gameOverMenu.SetActive(true);
        }
    }
}