using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Abilities
{
    public class AbilityDisplayGUI : MonoBehaviour
    {
        /* GUI */
        public Vector2 position = new Vector2(0, 0);
        public GameObject abilityGUIPrefab;
        private GameObject _abilityGUIObj;
        private CanvasGroup _canvasGroup;
        private TextMeshProUGUI _quantityText;
        private TextMeshProUGUI _cooldownText;
        private Image _filledMask;

        /* Scripts */
        private Quantity _quantity;
        private Cooldown _cooldown;

        private void Awake()
        {
            _quantity = GetComponent<Quantity>();
            _cooldown = GetComponent<Cooldown>();

            InitializeGUI();
        }

        private void InitializeGUI()
        {
            InstantiateGUI();
            LinkGUIComponents();
            SubscribeGUI();
            SynchronizeGUI();
        }

        private void InstantiateGUI()
        {
            /* attach abilityGUI to Canvas */
            _abilityGUIObj = Instantiate(abilityGUIPrefab, position, abilityGUIPrefab.transform.rotation);
            _abilityGUIObj.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }

        private void LinkGUIComponents()
        {
            _canvasGroup = _abilityGUIObj.GetComponent<CanvasGroup>();
            GameObject cooldownObj = _canvasGroup.transform.Find("Cooldown").gameObject;
            _quantityText = _canvasGroup.GetComponentInChildren<TextMeshProUGUI>();
            _cooldownText = cooldownObj.GetComponentInChildren<TextMeshProUGUI>();
            _filledMask = cooldownObj.GetComponentInChildren<Image>();
        }

        private void SubscribeGUI()
        {
            _cooldown.OnTimeLeftChange += UpdateCooldownText;
            _cooldown.OnTimeLeftPercentageChange += UpdateMask;
            _cooldown.OnReset += ResetCooldown;
            _quantity.OnQuantityChange += UpdateQuantityText;
            _quantity.OnQuantityChange += UpdateAvailability;
            _quantity.OnIsUnlimitedChange += UpdateQuantityText;
            _quantity.OnIsUnlimitedChange += UpdateAvailability;
        }

        private void SynchronizeGUI()
        {
            /* Trigger events */
            _quantity.SetQuantity(_quantity.Value);
            _quantity.SetIsUnlimited(_quantity.IsUnlimited);
        }

        private void Start()
        {
            ResetCooldown();
        }

        private void UpdateCooldownText(float remainingTime)
        {
            if (_cooldownText)
            {
                if (remainingTime <= 0.0f)
                {
                    _cooldownText.text = "";
                }
                else if (remainingTime <= 1.0f)
                {
                    _cooldownText.text = remainingTime.ToString("n1");
                }
                else
                {
                    _cooldownText.text = ((int) (remainingTime + 0.5f)).ToString();
                }
            }
        }

        private void UpdateMask(float percentage)
        {
            _filledMask.fillAmount = 1 - percentage;
        }

        private void ResetCooldown()
        {
            UpdateMask(0);
            UpdateCooldownText(0);
        }

        private void UpdateQuantityText(bool isUnlimited)
        {
            _quantityText.enabled = !isUnlimited;
        }

        private void UpdateQuantityText(uint quantity)
        {
            _quantityText.text = "x" + quantity;
        }

        private void UpdateAvailability(bool isUnlimited)
        {
            if (isUnlimited)
            {
                _canvasGroup.alpha = 1;
            }
        }

        private void UpdateAvailability(uint quantity)
        {
            if (_quantity.IsEmpty())
            {
                _canvasGroup.alpha = 0.5f;
            }
            else if (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha = 1;
            }
        }
    }
}