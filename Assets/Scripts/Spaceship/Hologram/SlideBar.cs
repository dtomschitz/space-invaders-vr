using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>SlideBar</c> is used as an base class for slide bars e.g. the 
/// health and force field bar.
/// </summary>
public class SlideBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    /// <summary>
    /// Sets the maximum fill amount to the given value. The method can be used
    /// to reset the bar.
    /// </summary>
    protected void SetMaxFillAmount(float amount)
    {
        slider.maxValue = amount;
        slider.value = amount;
        fill.color = gradient.Evaluate(1f);
    }

    /// <summary>
    /// Sets the fill amount to the given value.
    /// <param name="amount">
    /// The amount which should get represented from the bar.
    /// </param>
    /// </summary>
    protected void SetFillAmount(float amount)
    {
        slider.value = amount;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}