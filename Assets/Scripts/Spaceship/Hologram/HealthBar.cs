public class HealthBar : SlideBar
{
    void Start()
    {
        Player.instance.OnEntityHealed += OnEntityHealed;
        Player.instance.OnEntityDamaged += OnEntityDamaged;
        GameState.instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnGameStateChanged(GameStateType newState) 
    {
        if (newState == GameStateType.PreInGame)
        {
            SetFillAmount(Player.instance.HealthNormalized);
        }
    }

    /// <summary>
    /// Gets called if the player got healed and updates the health bar accordingly.
    /// </summary>
    /// <param name="amount">The amount of health points the player received.</param>
    void OnEntityHealed(float amount, float currentNormalizedHealth)
    {
        SetFillAmount(currentNormalizedHealth);
    }

    /// <summary>
    /// Gets called when the player took damage and updates the health bar accordingly..
    /// </summary>
    /// <param name="amount">The amount of damage the player took.</param>
    void OnEntityDamaged(float damage, float currentNormalizedHealth)
    {
        SetFillAmount(currentNormalizedHealth);
    }
}
