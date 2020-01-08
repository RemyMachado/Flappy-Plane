using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Abilities
{
    public class AbilityIncreaseCollisionForce : Ability
    {
        public ParticleSystem collisionParticlesPrefab;
        private Quantity _quantity;
        private Cooldown _cooldown;
        private PlayerCollision _playerCollision;
        private PlayerHealth _playerHealth;
        private PowerUpIndicator _powerUpIndicator;
        private float _additionalForce = 5.0f;
        private float _buffDuration = 10.0f;
        private uint _activeBuffCount;
        public AudioClip usageSound;
        public AudioClip strongHitSound;
        public AudioClip mediumHitSound;
        public AudioClip weakHitSound;
        public AudioClip unusableSound;

        private void Awake()
        {
            _quantity = GetComponent<Quantity>();
            _cooldown = GetComponent<Cooldown>();
            _playerCollision = GetComponentInParent<PlayerCollision>();
            _playerHealth = GetComponentInParent<PlayerHealth>();
            _powerUpIndicator = GetComponent<PowerUpIndicator>();
        }

        protected override void Start()
        {
            base.Start();
            _playerHealth.OnDeath += _powerUpIndicator.Hide;
            _playerCollision.OnCollisionEnemy += PlayParticles;
        }


        public override void UseAbility()
        {
            if (!IsUsable())
            {
                GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(unusableSound, 0.5f);
                return;
            }

            ConsumeAbility();
            StartBuff();
            StartCoroutine(DebuffCountdownRoutine());
        }

        private bool IsUsable()
        {
            return (_quantity.IsUnlimited || !_quantity.IsEmpty()) && !_cooldown.IsCoolingDown();
        }

        private void ConsumeAbility()
        {
            _quantity.SubtractQuantity(1);
            _cooldown.Begin();
            GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(usageSound, 1.0f);
        }

        private void StartBuff()
        {
            _playerCollision.collisionForce += _additionalForce;
            _powerUpIndicator.Show();
            ++_activeBuffCount;
        }

        IEnumerator DebuffCountdownRoutine()
        {
            yield return new WaitForSeconds(_buffDuration);

            --_activeBuffCount;
            _playerCollision.collisionForce -= _additionalForce;
            if (_activeBuffCount == 0)
            {
                _powerUpIndicator.Hide();
            }
        }

        private float ComputeBuffedImpactVelocity(float velocityMagnitude)
        {
            return (velocityMagnitude + _additionalForce * _activeBuffCount) / 10;
        }

        private void PlayParticles(Collision other)
        {
            ContactPoint hit = other.GetContact(0);
            float impactVelocity = other.relativeVelocity.magnitude;

            // Grow particles scale depending on impact intensity
            if (_activeBuffCount > 0 && collisionParticlesPrefab)
            {
                float buffedImpactVelocity = ComputeBuffedImpactVelocity(impactVelocity);
                Vector3 particlesScale = new Vector3(buffedImpactVelocity, buffedImpactVelocity, buffedImpactVelocity);

                ParticleSystem ps = Instantiate(collisionParticlesPrefab, hit.point, transform.rotation);
                ps.transform.localScale = particlesScale;
                GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(strongHitSound, 0.5f);
            }
            else
            {
                if (impactVelocity < 10)
                {
                    GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>()
                        .PlayOneShot(weakHitSound, impactVelocity / 10);
                }
                else
                {
                    GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>()
                        .PlayOneShot(mediumHitSound, 1.0f);
                }
            }
        }
    }
}