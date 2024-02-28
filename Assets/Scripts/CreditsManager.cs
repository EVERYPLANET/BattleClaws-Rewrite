using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CreditsManager : MonoBehaviour
{
    // Define a list of strings in the Unity Inspector
    public List<string> textList;

    // Reference to TextMeshProUGUI objects
    public TextMeshProUGUI textObject1;
    public TextMeshProUGUI textObject2;
    public TextMeshProUGUI textObject3;
    public TextMeshProUGUI textObject4;
    // Add more TextMeshProUGUI objects as needed

    // Function to assign strings to TextMeshProUGUI objects
    public void AssignTexts()
    {
        // Make sure the number of strings matches the number of TextMeshProUGUI objects
        if (textList.Count == 0 || textList.Count != GetTextObjectsCount())
        {
            Debug.LogError("Number of strings in the list does not match the number of TextMeshProUGUI objects.");
            return;
        }

        // Assign each string to the corresponding TextMeshProUGUI object
        for (int i = 0; i < textList.Count; i++)
        {
            GetTextObjectByIndex(i).text = textList[i];
        }
    }

    // Helper function to get the number of assigned TextMeshProUGUI objects
    private int GetTextObjectsCount()
    {
        int count = 0;

        if (textObject1 != null) count++;
        if (textObject2 != null) count++;
        if (textObject3 != null) count++;
        if (textObject4 != null) count++;
        // Add more TextMeshProUGUI objects as needed

        return count;
    }

    // Helper function to get TextMeshProUGUI object by index
    private TextMeshProUGUI GetTextObjectByIndex(int index)
    {
        switch (index)
        {
            case 0: return textObject1;
            case 1: return textObject2;
            case 2: return textObject3;
            case 3: return textObject4;
            // Add more cases as needed
            default:
                Debug.LogError("Invalid TextMeshProUGUI index.");
                return null;
        }
    }
}