using System;
using Player;
using UnityEngine;

namespace Abilities
{
    public class AbilityDash : Ability
    {
        private Quantity _quantity;
        private Cooldown _cooldown;
        private Rigidbody _playerRigidBody;
        private PlayerMovement _playerMovement;
        public ParticleSystem usageParticlePrefab;
        private ParticleSystem _usageParticleInstance;
        public AudioClip dashSound;
        public AudioClip unusableSound;

        private void Awake()
        {
            _quantity = GetComponent<Quantity>();
            _cooldown = GetComponent<Cooldown>();
            _playerRigidBody = GetComponentInParent<Rigidbody>();
            _playerMovement = GetComponentInParent<PlayerMovement>();
        }

        public override void UseAbility()
        {
            if (!IsUsable())
            {
                GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(unusableSound, 0.5f);
                return;
            }

            ConsumeAbility();

            Vector3 verticalDir = _playerMovement.GetVerticalDir();
            Vector3 horizontalDir = _playerMovement.GetHorizontalDir();

            _playerRigidBody.AddForce(verticalDir * 10.0f, ForceMode.Impulse);
            _playerRigidBody.AddForce(horizontalDir * 10.0f, ForceMode.Impulse);
        }

        private bool IsUsable()
        {
            return (_quantity.IsUnlimited || !_quantity.IsEmpty()) && !_cooldown.IsCoolingDown();
        }

        private void ConsumeAbility()
        {
            _quantity.SubtractQuantity(1);
            _cooldown.Begin();
            _usageParticleInstance = Instantiate(usageParticlePrefab,
                Vector3.Scale(transform.position, new Vector3(1, 0, 1)), transform.rotation);
            _usageParticleInstance.transform.localScale += new Vector3(3, 3, 3);
            GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(dashSound, 1.0f);
        }
    }
}