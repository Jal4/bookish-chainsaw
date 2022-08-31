using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusPointsBarScript : MonoBehaviour
{
    public Slider slider;

    public void SetMaxFP(float maxFP)
    {
        slider.maxValue = maxFP;
        slider.value = maxFP;
    }
    public void SetCurrentFP(float currentFP)
    {
        slider.value = currentFP;
    }
}
