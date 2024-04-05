using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class PlayerUIManager : MonoBehaviour
{
    // References
    [SerializeField]
    private PlayerCombatController combatController;

    // Weapons
    [SerializeField]
    private GameObject weaponPanel;
    [SerializeField]
    private Color panelCol;
    [SerializeField]
    private GameObject waterPanel;
    [SerializeField]
    private GameObject firePanel;
    [SerializeField]
    private GameObject acidPanel;
    public List<Color> uiColours;
    [SerializeField]
    private List<Image> uiImages;

    // Fuels
    public Slider waterFuelSlider;
    public Slider fireFuelSlider;
    public Slider acidFuelSlider;

    // Health
    public Image[] healthPackImages;

    // Geiger
    public Pickup[] pickups;
    [SerializeField]
    private Animator geigerAnimator;

    private void Awake()
    {
        GetUIImagesAndColours();
    }

    void Start()
    {
        combatController = GetComponent<PlayerCombatController>();
        pickups = FindObjectsOfType<Pickup>();
    }

    private void GetUIImagesAndColours()
    {
        Image[] images = weaponPanel.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.name != "WeaponPanel")
            {
                uiImages.Add(image);
                uiColours.Add(image.color);
            }
        }
    }

    void Update()
    {
        ManageWeaponSelectionUI(); // TODO: Only call when switching weapons
        ManageFuelUI();
        ManageHealthPackUI();
        ManageGeiger();
    }

    private void ManageGeiger()
    {
        foreach (Pickup pickup in pickups)
        {
            if (pickup.distFromPlayer < pickup.audioMaxDist + 1)
            {
                Light2D light2D = pickup.GetComponent<Light2D>();

                if (light2D.intensity > 0f && pickup.distFromPlayer < pickup.audioMaxDist)
                {
                    geigerAnimator.SetBool("Activate", true);
                }
                else
                {
                    geigerAnimator.SetBool("Activate", false);
                }
            }
        }
    }

    private void ManageHealthPackUI()
    {
        foreach (var pack in healthPackImages)
        {
            pack.enabled = false;
        }

        for (int i = 0; i < combatController.healthPacks; i++)
        {
            healthPackImages[i].enabled = true;
        }
    }

    private void ManageFuelUI()
    {
        waterFuelSlider.value = combatController.weaponFuels[0];
        fireFuelSlider.value = combatController.weaponFuels[1];
        acidFuelSlider.value = combatController.weaponFuels[2];
    }

    private void ManageWeaponSelectionUI()
    {
        switch (combatController.currentWeapon)
        {
            case PlayerCombatController.Weapons.PowerWasher:
                // Restore colours
                uiImages[0].color = uiColours[0];
                uiImages[1].color = uiColours[1];

                // Set new colours
                //// Fire
                uiImages[2].color = new Color(uiImages[2].color.r, uiImages[2].color.g, uiImages[2].color.b, 0.5f); 
                uiImages[3].color = new Color(uiImages[3].color.r, uiImages[3].color.g, uiImages[3].color.b, 0.5f);
                //// Acid
                uiImages[4].color = new Color(uiImages[4].color.r, uiImages[4].color.g, uiImages[4].color.b, 0.5f); 
                uiImages[5].color = new Color(uiImages[5].color.r, uiImages[5].color.g, uiImages[5].color.b, 0.5f);
                break;
            case PlayerCombatController.Weapons.FlameThrower:
                // Restore colours
                uiImages[2].color = uiColours[2];
                uiImages[3].color = uiColours[3];

                // Set new colours
                //// Water
                uiImages[0].color = new Color(uiImages[0].color.r, uiImages[0].color.g, uiImages[0].color.b, 0.5f);
                uiImages[1].color = new Color(uiImages[1].color.r, uiImages[1].color.g, uiImages[1].color.b, 0.5f);
                //// Acid
                uiImages[4].color = new Color(uiImages[4].color.r, uiImages[4].color.g, uiImages[4].color.b, 0.5f);
                uiImages[5].color = new Color(uiImages[5].color.r, uiImages[5].color.g, uiImages[5].color.b, 0.5f);
                break;
            case PlayerCombatController.Weapons.AcidBlaster:
                // Restore colours
                uiImages[4].color = uiColours[4];
                uiImages[5].color = uiColours[5];

                // Set new colours
                //// Water
                uiImages[0].color = new Color(uiImages[0].color.r, uiImages[0].color.g, uiImages[0].color.b, 0.5f);
                uiImages[1].color = new Color(uiImages[1].color.r, uiImages[1].color.g, uiImages[1].color.b, 0.5f);
                //// Fire
                uiImages[2].color = new Color(uiImages[2].color.r, uiImages[2].color.g, uiImages[2].color.b, 0.5f);
                uiImages[3].color = new Color(uiImages[3].color.r, uiImages[3].color.g, uiImages[3].color.b, 0.5f);
                break;
            default:
                break;
        }
    }
}
