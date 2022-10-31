using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private Image totalStaminaBar;
    [SerializeField] private Image currentStaminaBar;

    private void Update()
    {
        totalStaminaBar.fillAmount = playerStamina.maxStamina / 10;
        currentStaminaBar.fillAmount = playerStamina.currentStamina / 10;
    }
}
