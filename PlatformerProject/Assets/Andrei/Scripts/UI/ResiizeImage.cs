using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResiizeImage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro; 
    [SerializeField] RectTransform imageRectTransform;
    [SerializeField] float coef;

    void Update()
    {
        ResizeImage();
    }

    void ResizeImage()
    {
        if (textMeshPro != null && imageRectTransform != null)
        {
            // Get the width of the TextMeshPro object
            float textWidth = textMeshPro.rectTransform.rect.width;

            // Calculate the new width for the image
            float newImageWidth = textWidth * coef;

            // Set the width of the Image
            imageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newImageWidth);
        }
        else
        {
            Debug.LogError("TextMeshPro or Image RectTransform reference is missing.");
        }
    }
}
