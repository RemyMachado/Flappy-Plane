using UnityEngine;

namespace Controllers
{
    public class EnemySpeedIncrease : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        private float _maxSpeed = 20.0f;
        private Rigidbody _rigidBody;
        private GameObject _player;
        private PlayerController _playerController;
        private Renderer _renderer;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _playerController = _player.GetComponent<PlayerController>();
        }

        private void FixedUpdate()
        {
            if (_playerController)
            {
                float regulatedSpeed = Mathf.Min(speed, _maxSpeed);
                Vector3 distanceFromPlayer = _playerController.GetDistanceFrom(transform.position).normalized;

                _rigidBody.AddForce(distanceFromPlayer * regulatedSpeed);
                speed += Time.deltaTime;
                _renderer.material.color = Color.Lerp(Color.white, Color.red, regulatedSpeed / _maxSpeed);
            }
        }
    }
}