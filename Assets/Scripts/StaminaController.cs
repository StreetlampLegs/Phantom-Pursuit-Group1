using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;



public class StaminaController : MonoBehaviour
{
    [Header("Stamina Main Parameters")]

    public float playerStamina 100.0f;

    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float jumpCost = 20;
    [HideInInspector] public bool has Regenerated = true;
    [HideInInspector] public bool weAreSprinting = false;

    [Header("Stamina Regen Parameters")]
    [Range(0, 50)][SerializeField] private float staminaDrain = 0.5f;
    [Range(0, 50)][SerializeField] private float staminaRegen = 0.5f;

    [Header("Stamina Speed Parameters")]
    [SerializeField] private int slowedRunSpeed = 4;
    [SerializeField] private int normalRunSpeed = 8;

    [Header("Stamina UI Elements")]
    [SerializeField] private Image stamina ProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;



    private FirstPersonController playerController;

    private void Start()
    {
        playerController = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if (!weAreSprinting)
        {
            if (playerStamina <= maxStamina - 0.01)
            {
                playerStamina += staminaRegen * Time.deltaTime; //UpdateStamina
                UpdateStamina(1);
                if (playerStamina >= maxStamina)
                {
                    //set to normal speed
                    sliderCanvasGroup.alpha = 0;
                    hasRegenerated = true;

                }
            }
        }

    }


    public void Sprinting()
    {
        if (hasRegnerated)
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);

            if (playerStamina <= 0)
            {
                hasRegernated = false;
                sliderCanvasGroup.alpha = 0;
            }
        }
    }

    void UpdateStamina(int value)
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;
        if (value == 0)
        {
            sliderCanvasGroup.alpha = 0;
        }
        else
        {
            sliderCanvasGroup.alpha = 1;
        }
        
    }