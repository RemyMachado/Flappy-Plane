using UnityEngine;
using UnityEngine.UI;

namespace Abilities
{
    public class ClickTriggerNuke : MonoBehaviour
    {
        private Button _button;
        private GameObject _playerAbilities;

        private AbilityNuke _abilityNuke;
        // Start is called before the first frame update
        void Start()
        {
            _playerAbilities = GameObject.Find("Abilities");
            _abilityNuke = _playerAbilities.GetComponentInChildren<AbilityNuke>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_abilityNuke.UseAbility);
        }
    }
}
