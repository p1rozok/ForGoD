
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel;

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
        else
        {
            Debug.LogError("Inventory panel not assigned in UIManager.");
        }
    }
}
