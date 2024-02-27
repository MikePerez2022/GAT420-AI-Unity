using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Define a class named AIUIMeter that extends MonoBehaviour
public class AIUIMeter : MonoBehaviour
{
	// Serialize field for the label text component
	[SerializeField] TMP_Text label;
	// Serialize field for the slider component
	[SerializeField] Slider slider;
	// Serialize field for the image component
	[SerializeField] Image image;

	// Property to set the position of the UI element
	public Vector3 position
	{
		set
		{
			// Draw a debug line for visualization
			Debug.DrawLine(value, value + Vector3.up * 3);

			// Convert world position to viewport point
			Vector2 viewportPoint = Camera.main.WorldToViewportPoint(value);

			// Set the anchors of the RectTransform to match the converted viewport point
			GetComponent<RectTransform>().anchorMin = viewportPoint;
			GetComponent<RectTransform>().anchorMax = viewportPoint;
		}
	}

	// Property to set the value of the slider
	public float value
	{
		set
		{
			// Set the value of the slider
			slider.value = value;
		}
	}

	// Property to set the text of the label
	public string text
	{
		set
		{
			// Set the text of the label
			label.text = value;
		}
	}

	// Property to set the visibility of the UI element
	public bool visible
	{
		set
		{
			// Set the game object's active state based on the input value
			gameObject.SetActive(value);
		}
	}

	// Property to set the alpha (transparency) of the image
	public float alpha
	{
		set
		{
			// Get the current color of the image
			Color color = image.color;

			// Set the alpha value of the color
			color.a = value;

			// Update the image color with the new alpha value
			image.color = color;
		}
	}
}