using UnityEngine;

namespace Controllers
{
    public class EnemyGrow : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        private Rigidbody _rigidBody;
        private GameObject _player;
        private PlayerController _playerController;
        private readonly Vector3 _enemyUpScaling = new Vector3(0.01f, 0.01f, 0.01f);
        private readonly float _enemyMassGrowing = 0.1f;
        private float _upScalingInterval = 0.1f;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _playerController = _player.GetComponent<PlayerController>();
            InvokeRepeating(nameof(Grow), 1.0f, _upScalingInterval);
        }

        private void FixedUpdate()
        {
            if (_playerController)
            {
                Vector3 distanceFromPlayer = _playerController.GetDistanceFrom(transform.position).normalized;

                _rigidBody.AddForce(distanceFromPlayer * speed, ForceMode.Acceleration);
            }
        }

        private void Grow()
        {
            transform.localScale += _enemyUpScaling;
            _rigidBody.mass += _enemyMassGrowing;
        }
    }
}