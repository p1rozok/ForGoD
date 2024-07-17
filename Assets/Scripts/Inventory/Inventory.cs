
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    private UIManager uiManager;

    private void Start()
    {
        // ������� � ��������� UIManager
        uiManager = GameObject.FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager �� ������!");
        }
    }

    public void ToggleInventory()
    {
        if (uiManager != null)
        {
            uiManager.ToggleInventory();
        }
        else
        {
            Debug.LogError("UIManager �� �������� � Inventory.");
        }
    }
}
