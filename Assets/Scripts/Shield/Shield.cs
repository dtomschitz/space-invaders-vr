using System;
using UnityEngine;

public class Shield : MonoBehaviour
{
    #region Singelton

    public static Shield instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    [Header("Shield Stats")]
    public const int maxShieldPower = 100;
    public float shieldRegenerationAmount;
    public float shieldRegenerationSpeed;

    [Header("Shield Button")]
    public ShieldButton shieldButton;

    public delegate void ShieldPowerUsed(float amount, float currentNormalizedShieldPower);
    public event ShieldPowerUsed OnShieldPowerUsed;

    public delegate void ShieldPowerAdded(float amount, float currentNormalizedShieldPower);
    public event ShieldPowerAdded OnShieldPowerAdded;


    public float CurrentShieldPower { get; protected set; }
    public bool IsShieldEnabled { get; protected set; }

    void Start()
    {
        shieldButton.OnButtonPress += OnShieldButtonPress;
    }

    void Update()
    {
        if (!IsShieldEnabled)
        {
            AddShieldPower(shieldRegenerationAmount * Time.deltaTime / shieldRegenerationSpeed);
        } else
        {
            // UseShieldPower()
        }
    }

    void OnShieldButtonPress()
    {
        IsShieldEnabled = !IsShieldEnabled;
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
        OnShieldPowerAdded?.Invoke(amount, ShieldPowerNormalized);
    }

    /// <summary>
    /// This method reduces a set ammount of shield power from the player and calls the
    /// <see cref="OnShieldPowerUsed"/> event.
    /// </summary>
    /// <param name="amount">The ammount of shield power the player lost.</param>
    public void UseShieldPower(float amount = 1f)
    {
        CurrentShieldPower -= amount;
        OnShieldPowerUsed?.Invoke(amount, ShieldPowerNormalized);
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

