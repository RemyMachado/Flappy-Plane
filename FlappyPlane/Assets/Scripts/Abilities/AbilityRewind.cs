using System;
using Player;
using UnityEngine;

namespace Abilities
{
    public class AbilityRewind : Ability
    {
        private Quantity _quantity;
        private Cooldown _cooldown;
        private RewindTransform _rewindTransform;
        private MeshRenderer _meshRenderer;
        public ParticleSystem usageParticlePrefab;
        private ParticleSystem _usageParticleInstance;
        public AudioClip rewindSound;
        public AudioClip unusableSound;

        private void Awake()
        {
            _quantity = GetComponent<Quantity>();
            _cooldown = GetComponent<Cooldown>();
            _rewindTransform = GetComponent<RewindTransform>();
            _meshRenderer = GetComponentInParent<MeshRenderer>();

            _rewindTransform.OnRewindFinish += SetMaxOpacity;
            _rewindTransform.OnRewindFinish += DestroyParticleSystem;
        }

        public override void UseAbility()
        {
            if (!IsUsable())
            {
                GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(unusableSound, 0.5f);
                return;
            }

            ConsumeAbility();

            SetOpacity(0.5f);
            _rewindTransform.StartRewind();
        }

        private bool IsUsable()
        {
            return (_quantity.IsUnlimited || !_quantity.IsEmpty()) && !_cooldown.IsCoolingDown();
        }

        private void DestroyParticleSystem()
        {
            Destroy(_usageParticleInstance);
        }
        
        private void ConsumeAbility()
        {
            _quantity.SubtractQuantity(1);
            _cooldown.Begin();
            _usageParticleInstance = Instantiate(usageParticlePrefab, Vector3.zero, transform.rotation);
            GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(rewindSound, 1.0f);
        }

        private void SetOpacity(float ratio)
        {
            Color colorCopy = _meshRenderer.material.color;
            colorCopy.a = ratio;

            _meshRenderer.material.color = colorCopy;
        }

        private void SetMaxOpacity()
        {
            SetOpacity(1.0f);
        }
    }
}