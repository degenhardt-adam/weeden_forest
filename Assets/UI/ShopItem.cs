using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public static ShopItem selected;
    public static GameObject outline;
    public Sprite indicatorSprite;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Add a click listener to the button component
        GetComponent<Button>().onClick.AddListener(Select);

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

    void Select()
    {
        if (selected != null)
        {
            // Hide the outline of the previously selected item
            outline.SetActive(false);
        }

        // Show the outline of the newly selected item
        outline.transform.position = transform.position;
        outline.GetComponent<RectTransform>().sizeDelta = rectTransform.sizeDelta + new Vector2(10, 10); // adjust as needed
        outline.SetActive(true);

        // Update the selected item
        selected = this;
    }
}
