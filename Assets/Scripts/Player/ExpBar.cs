using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;

    public void SetDefaultExp(int exp)
    {
        slider.maxValue = exp;
        slider.value = exp;
    }
    public void SetExp(int exp)
    {
        slider.value = exp;
    }
}
