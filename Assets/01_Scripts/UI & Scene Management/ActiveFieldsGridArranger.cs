using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActiveFieldsGridVariableSizeArranger : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public RectTransform container;       // The empty GameObject holding all input fields
    public RectTransform containerParent; // The panel holding the container

    public float verticalSpacing = 5f;    // space between rows
    public float horizontalSpacing = 10f; // space between columns

    // Call this whenever the active fields change
    public void RearrangeActiveFields()
    {
        // Get all child input fields that are active (enabled and gameObject active)
        List<RectTransform> activeFields = new List<RectTransform>();
        foreach (RectTransform child in container)
        {
            if (child.gameObject.activeSelf)
            {
                activeFields.Add(child);
            }
        }

        if (activeFields.Count == 0)
        {
            // No active fields, no layout needed
            return;
        }

        int rows = 4; // fixed rows per your design
        int columns = Mathf.CeilToInt(activeFields.Count / (float)rows);

        // Position fields in columns (left to right), top to bottom inside container
        float[] columnWidths = new float[columns];
        float[] rowHeights = new float[rows];

        // First, calculate max widths and heights per column and row
        for (int i = 0; i < activeFields.Count; i++)
        {
            RectTransform field = activeFields[i];
            int col = i / rows;    // column index
            int row = i % rows;    // row index

            Vector2 size = field.sizeDelta;

            if (size.x > columnWidths[col])
                columnWidths[col] = size.x;

            if (size.y > rowHeights[row])
                rowHeights[row] = size.y;
        }

        // Now position each field relative to container top-left (pivot 0,1)
        // Y: start at 0 top, go down by row heights + spacing
        // X: start at 0 left, add column widths + spacing
        float[] columnXPositions = new float[columns];
        columnXPositions[0] = 0;
        for (int c = 1; c < columns; c++)
        {
            columnXPositions[c] = columnXPositions[c - 1] + columnWidths[c - 1] + horizontalSpacing;
        }

        float[] rowYPositions = new float[rows];
        rowYPositions[0] = 0;
        for (int r = 1; r < rows; r++)
        {
            rowYPositions[r] = rowYPositions[r - 1] - (rowHeights[r - 1] + verticalSpacing);
        }

        // Apply positions
        for (int i = 0; i < activeFields.Count; i++)
        {
            RectTransform field = activeFields[i];
            int col = i / rows;
            int row = i % rows;

            float posX = columnXPositions[col];
            float posY = rowYPositions[row];

            field.anchoredPosition = new Vector2(posX, posY);
        }

        // Calculate total width and height of container
        float totalWidth = 0f;
        for (int c = 0; c < columns; c++)
        {
            totalWidth += columnWidths[c];
            if (c > 0)
                totalWidth += horizontalSpacing;
        }

        float totalHeight = 0f;
        for (int r = 0; r < rows; r++)
        {
            totalHeight += rowHeights[r];
            if (r > 0)
                totalHeight += verticalSpacing;
        }

        // Resize container to fit content
        container.sizeDelta = new Vector2(totalWidth, totalHeight);

        // Now position the container inside its parent (the panel)
        // Rules:
        // - If 1 column: pinned top-left (0,1)
        // - If multiple columns: shift left so content fits inside panel but container never moves right of 0

        float newX = 0f;
        if (columns > 1)
        {
            float maxShift = containerParent.rect.width - totalWidth;
            newX = Mathf.Min(0, maxShift);
        }

        // Keep y anchoredPosition unchanged (usually 1 or 0 depending on setup)
        container.anchoredPosition = new Vector2(newX, container.anchoredPosition.y);
    }
}
