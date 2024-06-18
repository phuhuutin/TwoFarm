using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolSelector : MonoBehaviour
{
    public Image[] toolSlots;   // Array of tool slot images
    public Image highlightIndicator;  // Highlight indicator image
    private int selectedToolIndex = 0;


    void Start()
    {
        // Initialize the highlight indicator position
        StartCoroutine(DelayedStart());

 
    }

    public void SelectTool(int index)
    {
        selectedToolIndex = index;
        UpdateHighlightPosition();
    }

    void UpdateHighlightPosition()
    {
        // Position the highlightIndicator based on the selectedToolIndex
        highlightIndicator.transform.position = toolSlots[selectedToolIndex].transform.position;
    }

    void ScreenSizeChanged(ScreenOrientation newOrientation)
    {
        // Handle screen size change by updating the highlight position
        UpdateHighlightPosition();
    }

    IEnumerator DelayedStart()
{
    yield return null; // Wait for one frame

    // Initialize the highlight indicator position
    UpdateHighlightPosition();
}
 
}
