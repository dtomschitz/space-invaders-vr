using UnityEngine;
using UnityEngine.UI;

public class ShrinkBar : MonoBehaviour
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
        fill.color = gradient.Evaluate(amount);
    }
}