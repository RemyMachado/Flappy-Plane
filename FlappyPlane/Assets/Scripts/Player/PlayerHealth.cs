using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public event Action OnDeath;
        private bool _isAlive = true;
        public ParticleSystem[] deathParticlesPrefabs; // particles effects to apply in order
        public AudioClip deathSound;

        private void Update()
        {
            if (_isAlive)
            {
                CheckDeath();
            }
        }

        private void CheckDeath()
        {
            if (transform.position.y < -20)
            {
                _isAlive = false;
                GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(deathSound, 1.0f);
                for (uint i = 0; i < deathParticlesPrefabs.Length; ++i)
                {
                    Instantiate(deathParticlesPrefabs[i], transform.position, transform.rotation);
                }

                OnDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}