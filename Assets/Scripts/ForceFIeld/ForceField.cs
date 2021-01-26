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

    public int maxShieldPower = 100;
    public GameObject sphere;

    [Header("Shield Regeneration")]
    public float shieldRegenerationAmount;
    public float shieldRegenerationSpeed;

    [Header("Shield Consumption")]
    public float shieldConsumptionAmount;
    public float shielConsumptionSpeed;

    [Header("Shield Button")]
    public ForceFieldButton button;

    public delegate void ShieldPowerUsed(float amount, float currentNormalizedShieldPower);
    public event ShieldPowerUsed OnShieldPowerUsed;

    public delegate void ShieldPowerAdded(float amount, float currentNormalizedShieldPower);
    public event ShieldPowerAdded OnShieldPowerAdded;

    public delegate void ShieldPowerConsumed();
    public event ShieldPowerConsumed OnShieldPowerConsumed;

    bool isEnabled;

    void Start()
    {
        button.OnButtonPress += OnShieldButtonPress;
        sphere.SetActive(false);
    }

    void Update()
    {
        if (IsEnabled)
        {
            if (!IsForceFieldEnabled)
            {
                AddShieldPower(shieldRegenerationAmount * Time.deltaTime / shieldRegenerationSpeed);
            }
            else
            {
                UseShieldPower(shieldConsumptionAmount * Time.deltaTime / shielConsumptionSpeed);
            }
        }
    }

    void OnShieldButtonPress()
    {
        IsForceFieldEnabled = !IsForceFieldEnabled;
        sphere.SetActive(IsForceFieldEnabled);
        //GunManager.instance.EnableShooting(!IsForceFieldEnabled);
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
    public void UseShieldPower(float amount)
    {
        CurrentShieldPower -= amount;
        OnShieldPowerUsed?.Invoke(amount, ShieldPowerNormalized);

        if (CurrentShieldPower <= 0)
        {
            sphere.SetActive(false);
            IsForceFieldEnabled = false;
            //GunManager.instance.EnableShooting(true);

            OnShieldPowerConsumed?.Invoke();
        }
    }

    public void EnableForceField(bool value)
    {
        if (!value)
        {
            IsForceFieldEnabled = false;
        }

        IsEnabled = value;
        button.IsEnabled = value;
    }

    /// <summary>
    /// This method calculates the normalized shield power.
    /// </summary>
    /// <returns>The normalized shield power.</returns>
    public float ShieldPowerNormalized
    {
        get => CurrentShieldPower / maxShieldPower;
    }

    public float CurrentShieldPower { get; protected set; }

    public bool IsForceFieldEnabled { set; get; } = false;

    public bool IsEnabled {
        set
        {
            isEnabled = value;
            sphere.SetActive(value);
        }

        get => isEnabled;
    }
}

