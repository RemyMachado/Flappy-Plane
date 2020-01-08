using UnityEngine;

namespace Abilities
{
    public class PowerUpIndicator : MonoBehaviour
    {
        public GameObject powerUpIndicatorPrefab;
        private GameObject _powerUpIndicator;
        private GameObject _player;
        private bool _isVisible;

        private void Awake()
        {
            _powerUpIndicator = Instantiate(powerUpIndicatorPrefab, powerUpIndicatorPrefab.transform.position,
                powerUpIndicatorPrefab.transform.rotation);
            Hide();
        }

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void FixedUpdate()
        {
            if (_isVisible)
            {
                _powerUpIndicator.transform.position =
                    _player.transform.position + new Vector3(0, -0.5f, 0);
            }
        }

        public void Show()
        {
            _powerUpIndicator.SetActive(true);
            _isVisible = true;
        }

        public void Hide()
        {
            _powerUpIndicator.SetActive(false);
            _isVisible = false;
        }
    }
}