using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFlash : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    public float blinkInterval = 0.5f;

    void Start()
    {
        InvokeRepeating(nameof(ToggleText), blinkInterval, blinkInterval);
    }

    void ToggleText()
    {
        textObject.enabled = !textObject.enabled;
    }
}
