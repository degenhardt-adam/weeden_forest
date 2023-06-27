using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public static GameObject outline;
    public GameObject devicePrefab;
    public Sprite indicatorSprite;
    public int powerCost;
    public TMP_Text powerCostText;
    private RectTransform rectTransform;
    private GridSystem gridSystem;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Add a click listener to the button component
        GetComponent<Button>().onClick.AddListener(Select);

        powerCostText.text = $"Power Cost: {powerCost}";

        // Create outline if it doesn't exist
        if (outline == null)
        {
            outline = new GameObject("Outline");
            outline.AddComponent<Image>();
            outline.GetComponent<Image>().sprite = indicatorSprite;
            outline.transform.SetParent(transform.parent); // parent to the shop panel
            outline.SetActive(false);
        }
    }

    private void Start()
    {
        gridSystem = GridSystem.instance;
    }

    void Select()
    {
        // Show the outline of the newly selected item
        outline.SetActive(true);
        outline.transform.position = transform.position;
        outline.GetComponent<RectTransform>().sizeDelta = rectTransform.sizeDelta + new Vector2(10, 10); // adjust as needed

        gridSystem.selectedShopItem = this;
    }
}
