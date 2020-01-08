using System;
using Player;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        private KeyTrigger _keyTrigger;

        protected virtual void Start()
        {
            _keyTrigger = GetComponent<KeyTrigger>();
            _keyTrigger.OnKeyTriggered += UseAbility;
        }

        public abstract void UseAbility();
    }
}