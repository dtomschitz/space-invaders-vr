/// <summary>
/// Class <c>HealthBar</c> is used to visualize the force field power bar for 
/// the player.
/// </summary>
public class ForceFieldBar : SlideBar
{
    void Start()
    {
        ForceField.instance.OnForceFieldPowerAdded += OnShieldPowerAdded;
        ForceField.instance.OnForceFieldPowerUsed += OnShieldPowerUsed;
        GameState.instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnGameStateChanged(GameStateType newState)
    {
        // Reset the force field mana bar if the game state is set to PreInGame.
        if (newState == GameStateType.PreInGame)
        {
            SetFillAmount(ForceField.instance.ForceFieldPowerNormalized);
        }
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
