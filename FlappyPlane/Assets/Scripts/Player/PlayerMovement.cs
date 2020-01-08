using UnityEngine;
using Utils;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        private float _speed = 10.0f;
        public Joystick joystickPrefab;
        private Joystick _joystick;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _joystick = Instantiate(joystickPrefab, joystickPrefab.transform.position, joystickPrefab.transform.rotation);
            _joystick.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }

        private void Update()
        {
            Vector3 verticalDir = GetVerticalDir();
            Vector3 horizontalDir = GetHorizontalDir();

            _rigidBody.AddForce(verticalDir * _speed);
            _rigidBody.AddForce(horizontalDir * _speed);
        }

        public Vector3 GetVerticalDir()
        {
            float verticalInput = _joystick.Vertical;

            return verticalInput * Vector3.forward;
        }

        public Vector3 GetHorizontalDir()
        {
            float horizontalInput = _joystick.Horizontal;

            return horizontalInput * Vector3.right;
        }
    }
}