using UnityEngine;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        private Rigidbody _rigidBody;
        private GameObject _player;
        private PlayerController _playerController;


        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
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
                Vector3 distanceFromPlayer = _playerController.GetDistanceFrom(transform.position).normalized;

                _rigidBody.AddForce(distanceFromPlayer * speed);
            }
        }
    }
}