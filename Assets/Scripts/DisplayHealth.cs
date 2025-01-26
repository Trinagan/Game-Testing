using TMPro;
using UnityEngine;

public class DisplayHealth : MonoBehaviour
{
    public TMP_Text textComponent;

    public PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = "Health: " + playerController.health.ToString();
    }
}
