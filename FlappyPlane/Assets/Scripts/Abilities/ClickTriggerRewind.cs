using UnityEngine;
using UnityEngine.UI;

namespace Abilities
{
    public class ClickTriggerRewind : MonoBehaviour
    {
        private Button _button;
        private GameObject _playerAbilities;

        private AbilityRewind _abilityRewind;

        // Start is called before the first frame update
        void Start()
        {
            _playerAbilities = GameObject.Find("Abilities");
            _abilityRewind = _playerAbilities.GetComponentInChildren<AbilityRewind>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_abilityRewind.UseAbility);
        }
    }
}
