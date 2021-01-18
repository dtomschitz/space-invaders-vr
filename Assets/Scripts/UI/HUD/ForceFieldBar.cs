public class ForceFieldBar : ShrinkBar
{
    void Start()
    {
        ForceField.instance.OnShieldPowerAdded += OnShieldPowerAdded;
        ForceField.instance.OnShieldPowerUsed += OnShieldPowerUsed;
    }

    /// <summary>
    /// Gets called if the force field got healed and updates the health bar accordingly.
    /// </summary>
    /// <param name="amount">The amount of health points the player received.</param>
    void OnShieldPowerAdded(float amount, float currentNormalizedPower)
    {
        SetFillAmount(currentNormalizedPower);
    }

    /// <summary>
    /// Gets called when the force field took damage and updates the force field bar accordingly..
    /// </summary>
    /// <param name="amount">The amount of damage the player took.</param>
    void OnShieldPowerUsed(float amount, float currentNormalizedPower)
    {
        SetFillAmount(currentNormalizedPower);
    }
}
