public class PowerBar : ShrinkBar
{
    void Start()
    {
        Shield shield = Shield.instance;
        shield.OnShieldPowerAdded += OnShieldPowerAdded;
        shield.OnShieldPowerUsed += OnShieldPowerUsed;
    }

    void OnShieldPowerAdded(float amount, float currentNormalizedShieldPower)
    {
        SetFillAmount(currentNormalizedShieldPower);
    }

    void OnShieldPowerUsed(float amount, float currentNormalizedShieldPower)
    {
        SetFillAmount(currentNormalizedShieldPower);
    }
}
