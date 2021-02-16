using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>SlideBar</c> is used as an base class for slide bars e.g. the 
/// health or force field bar.
/// </summary>
public class SlideBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    protected void SetMaxFillAmount(float amount)
    {
        slider.maxValue = amount;
        slider.value = amount;
        fill.color = gradient.Evaluate(1f);
    }

    protected void SetFillAmount(float amount)
    {
        slider.value = amount;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}