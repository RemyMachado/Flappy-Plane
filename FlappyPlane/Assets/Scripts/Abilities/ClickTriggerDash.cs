using UnityEngine;
using UnityEngine.UI;

namespace Abilities
{
    public class ClickTriggerDash : MonoBehaviour
    {
        private Button _button;
        private GameObject _playerAbilities;

        private AbilityDash _abilityDash;
        // Start is called before the first frame update
        void Start()
        {
            _playerAbilities = GameObject.Find("Abilities");
            _abilityDash = _playerAbilities.GetComponentInChildren<AbilityDash>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_abilityDash.UseAbility);
        }
    }
}
