using System;
using Abilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PickUps
{
    public class PickUp : MonoBehaviour
    {
//        public string targetTag;
        public uint quantity = 1;
//        private Quantity _targetQuantity;
        private float _rotationSpeed = 3.0f;
        private Vector3 _rotationDirection;
        public AudioClip pickUpSound;
        private MeshRenderer _meshRenderer;

        void Start()
        {
//            _targetQuantity = GameObject.FindWithTag(targetTag).GetComponent<Quantity>();
            _rotationDirection = Random.insideUnitSphere;
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            transform.Rotate(_rotationDirection * _rotationSpeed); //rotates 50 degrees per second around z axis
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(pickUpSound, 0.5f);
//                _targetQuantity.AddQuantity(quantity);
                _meshRenderer.enabled = false;
                PlayerStats.money += 10;
//                Destroy(gameObject);
            }
        }
    }
}