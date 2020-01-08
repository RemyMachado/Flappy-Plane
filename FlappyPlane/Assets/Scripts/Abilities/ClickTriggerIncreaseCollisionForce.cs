using UnityEngine;
using UnityEngine.UI;

namespace Abilities
{
    public class ClickTriggerIncreaseCollisionForce : MonoBehaviour
    {
        private Button _button;
        private GameObject _playerAbilities;

        private AbilityIncreaseCollisionForce _abilityIncreaseCollisionForce;

        // Start is called before the first frame update
        void Start()
        {
            _playerAbilities = GameObject.Find("Abilities");
            _abilityIncreaseCollisionForce = _playerAbilities.GetComponentInChildren<AbilityIncreaseCollisionForce>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_abilityIncreaseCollisionForce.UseAbility);
        }
    }
}