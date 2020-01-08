using System;
using UnityEngine;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        public event Action<Collision> OnCollisionEnemy;
        public float collisionForce = 1.0f;
        public AudioClip dieSound;
        public AudioSource audioSpeaker;
        public ParticleSystem explosion;
        private MeshRenderer _meshRenderer;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                OnCollisionEnemy?.Invoke(other);
                Debug.Log("Collision");

                --PlayerStats.lives;
                audioSpeaker.PlayOneShot(dieSound, 1.0f);
                _meshRenderer.enabled = false;
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}