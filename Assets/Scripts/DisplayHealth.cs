using TMPro;
using UnityEngine;

public class DisplayHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text resourcesText;

    public PlayerController playerController;
    public ResourceManager resourceManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthText = GameObject.Find("Canvas/Health").GetComponent<TMP_Text>();
        resourcesText = GameObject.Find("Canvas/Resources").GetComponent<TMP_Text>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        resourceManager = GameObject.Find("Resource Manager").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + playerController.health.ToString();
        resourcesText.text = "Scrap: " + resourceManager.totalScrap + " Biomass: " + resourceManager.totalBiomass + " Weapon Parts: " + resourceManager.totalWeaponParts;
    }
}
