using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OnOff: MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    void Start()
    {
        textDisplay.enabled = false;
    }
    public void ToggleText()
    {
        if (textDisplay.enabled == true)
        {
            textDisplay.enabled = false;
        }
        else
        {
            textDisplay.enabled = true;
        }
    }
}
