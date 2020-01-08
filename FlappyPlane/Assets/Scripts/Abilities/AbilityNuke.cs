using System;
using Player;
using UnityEngine;

namespace Abilities
{
    public class AbilityNuke : Ability
    {
        private Quantity _quantity;
        private Cooldown _cooldown;
        private float _explosionRadius = 100.0f;
        private float _explosionForce = 50.0f;
        private float _explosionUpwardsModifier = 1.5f;
        private ForceMode _explosionForceMode = ForceMode.Impulse;
        public ParticleSystem explosionParticlePrefab;
        public AudioClip explosionSound;
        public AudioClip unusableSound;

        private void Awake()
        {
            _quantity = GetComponent<Quantity>();
            _cooldown = GetComponent<Cooldown>();
        }

        public override void UseAbility()
        {
            if (!IsUsable())
            {
                GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(unusableSound, 0.5f);
                return;
            }

            ConsumeAbility();

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, _explosionRadius);

            foreach (Collider nearbyCollider in colliders)
            {
                if (nearbyCollider.CompareTag("Player"))
                {
                    continue;
                }

                Rigidbody nearbyRigidBody = nearbyCollider.attachedRigidbody;

                if (nearbyRigidBody != null)
                    nearbyRigidBody.AddExplosionForce(_explosionForce, explosionPos, _explosionRadius,
                        _explosionUpwardsModifier, _explosionForceMode);
            }
        }

        private bool IsUsable()
        {
            return (_quantity.IsUnlimited || !_quantity.IsEmpty()) && !_cooldown.IsCoolingDown();
        }

        private void ConsumeAbility()
        {
            _quantity.SubtractQuantity(1);
            _cooldown.Begin();
            Instantiate(explosionParticlePrefab, transform.position, transform.rotation);
            GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(explosionSound, 1.0f);
        }
    }
}