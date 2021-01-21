using UnityEngine;

public class ForceField : MonoBehaviour
{
    #region Singelton

    public static ForceField instance;

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
    public ForceFieldButton shieldButton;

    public delegate void ShieldPowerUsed(float amount, float currentNormalizedShieldPower);
    public event ShieldPowerUsed OnShieldPowerUsed;

    public delegate void ShieldPowerAdded(float amount, float currentNormalizedShieldPower);
    public event ShieldPowerAdded OnShieldPowerAdded;

    bool isForceFieldEnabled;

    void Start()
    {
        shieldButton.OnButtonPress += OnShieldButtonPress;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!IsForceFieldEnabled)
        {
            AddShieldPower(shieldRegenerationAmount * Time.deltaTime / shieldRegenerationSpeed);
        } else
        {
            UseShieldPower(1f * Time.deltaTime);
        }
    }

    void OnShieldButtonPress()
    {
        IsForceFieldEnabled = !IsForceFieldEnabled;
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

    public float CurrentShieldPower { get; protected set; }

    public bool IsForceFieldEnabled
    {
        set
        {
            isForceFieldEnabled = value;
            gameObject.SetActive(value);
        }

        get => isForceFieldEnabled;
    }
}

