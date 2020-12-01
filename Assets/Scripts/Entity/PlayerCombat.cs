using System;
using UnityEngine;

public class PlayerCombat : EntityCombat
{
    [Header("Shield")]
    public const int maxShieldPower = 100;
    public float shieldRegenerationAmount;
    public float shieldRegenerationSpeed;

    public event Action OnShieldPowerUsed;
    public event Action OnShieldPowerAdded;

    public float CurrentShieldPower { get; protected set; }


    void Update()
    {
        if (!IsBlocking)
        {
            AddShieldPower(shieldRegenerationAmount * Time.deltaTime / shieldRegenerationSpeed);
        }
    }

    /// <summary>
    /// This method adds a set ammount of shield power to the player and calls the
    /// <see cref="OnShieldPowerAdded"/> event.
    /// </summary>
    /// <param name="amount">The ammount of shield power the player received.</param>
    public void AddShieldPower(float amount)
    {
        CurrentShieldPower += amount;
        CurrentShieldPower = Mathf.Clamp(CurrentShieldPower, 0f, maxShieldPower);
        OnShieldPowerAdded?.Invoke();
    }

    /// <summary>
    /// This method reduces a set ammount of shield power from the player and calls the
    /// <see cref="OnShieldPowerUsed"/> event.
    /// </summary>
    /// <param name="amount">The ammount of shield power the player lost.</param>
    public void UseShieldPower(float amount = 1f)
    {
        CurrentShieldPower -= amount;
        OnShieldPowerUsed?.Invoke();
    }

    /// <summary>
    /// This method calculates the normalized shield power.
    /// </summary>
    /// <returns>The normalized shield power.</returns>
    public float ShieldPowerNormalized
    {
        get { return CurrentShieldPower / maxShieldPower; }
    }


}

