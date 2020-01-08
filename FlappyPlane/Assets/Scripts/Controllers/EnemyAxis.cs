using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class EnemyAxis : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private Rigidbody _rigidBody;
    private GameObject _player;
    private PlayerController _playerController;
    private bool _isOnXAxis;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _isOnXAxis = Random.value < 0.5;
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
            Vector3 distanceFromPlayer = _playerController.GetDistanceFrom(transform.position);

            if (_isOnXAxis)
            {
                distanceFromPlayer.z = 0;
            }
            else // isOnZAxis
            {
                distanceFromPlayer.x = 0;
            }

            distanceFromPlayer = distanceFromPlayer.normalized;

            _rigidBody.AddForce(distanceFromPlayer * speed);
        }
    }
}