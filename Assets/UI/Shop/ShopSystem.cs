using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject shopWindow; // assign the panel (shop window) in Unity editor
    public Button shopButton; // assign the button in Unity editor
    public TurnSystem turnSystem; // assign the TurnSystem object in Unity editor

    void Start()
    {
        shopButton.onClick.AddListener(ToggleShopWindow);
    }

    void Update()
    {
        if (turnSystem.currentPhase == Phase.Editing)
        {
            shopButton.gameObject.SetActive(true);
            shopButton.interactable = true;
        }
        else
        {
            // Close the shop and hide the button
            shopWindow.SetActive(false);
            shopButton.gameObject.SetActive(false);
        }
    }

    void ToggleShopWindow()
    {
        // Toggle the shop window
        shopWindow.SetActive(!shopWindow.activeSelf);
    }
}
