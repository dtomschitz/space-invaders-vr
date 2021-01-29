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

    public int maxForceFieldPower = 100;
    public GameObject sphere;

    [Header("Shield Regeneration")]
    public float shieldRegenerationAmount;
    public float shieldRegenerationSpeed;

    [Header("Shield Consumption")]
    public float shieldConsumptionAmount;
    public float shielConsumptionSpeed;

    [Header("Shield Button")]
    public ForceFieldButton button;

    public delegate void ForceFieldPowerUsed(float amount, float currentNormalizedShieldPower);
    public event ForceFieldPowerUsed OnForceFieldPowerUsed;

    public delegate void ForceFieldPowerAdded(float amount, float currentNormalizedShieldPower);
    public event ForceFieldPowerAdded OnForceFieldPowerAdded;

    public delegate void ForceFieldPowerConsumed();
    public event ForceFieldPowerConsumed OnForceFieldPowerConsumed;

    void Start()
    {
        button.OnButtonPress += OnShieldButtonPress;
        CurrentForceFieldPower = maxForceFieldPower;
        sphere.SetActive(false);
    }

    void Update()
    {
        if (IsEnabled)
        {
            if (!IsForceFieldEnabled)
            {
                AddForceFieldPower(shieldRegenerationAmount * Time.deltaTime / shieldRegenerationSpeed);
            }
            else
            {
                UseForceFieldPower(shieldConsumptionAmount * Time.deltaTime / shielConsumptionSpeed);
            }
        }
    }

    void OnShieldButtonPress()
    {
        IsForceFieldEnabled = !IsForceFieldEnabled;
        sphere.SetActive(IsForceFieldEnabled);
        AudioManager.instance.PlaySound(Sound.ForceField, gameObject.transform.position);
    }


    /// <summary>
    /// This method adds a set ammount of shield power to the player and calls the
    /// <see cref="OnForceFieldPowerAdded"/> event.
    /// </summary>
    /// <param name="amount">The ammount of shield power the player received.</param>
    public void AddForceFieldPower(float amount)
    {
        CurrentForceFieldPower += amount;
        CurrentForceFieldPower = Mathf.Clamp(CurrentForceFieldPower, 0f, maxForceFieldPower);
        OnForceFieldPowerAdded?.Invoke(amount, ForceFieldPowerNormalized);
    }

    /// <summary>
    /// This method reduces a set ammount of shield power from the player and calls the
    /// <see cref="OnForceFieldPowerUsed"/> event.
    /// </summary>
    /// <param name="amount">The ammount of shield power the player lost.</param>
    public void UseForceFieldPower(float amount)
    {
        CurrentForceFieldPower -= amount;
        OnForceFieldPowerUsed?.Invoke(amount, ForceFieldPowerNormalized);

        if (CurrentForceFieldPower <= 0)
        {
            sphere.SetActive(false);
            IsForceFieldEnabled = false;
            OnForceFieldPowerConsumed?.Invoke();
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
    public float ForceFieldPowerNormalized
    {
        get => CurrentForceFieldPower / maxForceFieldPower;
    }

    public float CurrentForceFieldPower { get; protected set; }

    public bool IsForceFieldEnabled { set; get; } = false;

    public bool IsEnabled { set; get; }
}

