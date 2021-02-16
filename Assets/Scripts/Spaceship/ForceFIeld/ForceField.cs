using System.Collections;
using UnityEngine;

/// <summary>
/// Class <c>ForceField</c> is used to handle the interaction with the player
/// as well as the stats of it.
/// </summary>
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

    Coroutine soundCoroutine;

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

    /// <summary>
    /// This method enables or disables the force field if the player pressed
    /// the button on the desk.
    /// </summary>
    void OnShieldButtonPress()
    {
        ToggleForceField(!IsForceFieldEnabled);
    }

    /// <summary>
    /// This method enables or disables the force field if the player pressed
    /// the button on the desk. In case the start or stop sound is currently 
    ///playing it will get stopped and restarted.
    /// </summary>
    void ToggleForceField(bool active)
    {
        IsForceFieldEnabled = active;
        sphere.SetActive(active);

        if (soundCoroutine != null) StopCoroutine(soundCoroutine);
        soundCoroutine = StartCoroutine(ToggleForceFieldSound(IsForceFieldEnabled));
    }

    /// <summary>
    /// This method plays back the start or stop sound of the force field based
    /// on the given value.
    /// </summary>
    IEnumerator ToggleForceFieldSound(bool active)
    {
        if (active)
        {
            AudioManager.instance.PlaySound(Sound.ForceFieldStart, gameObject.transform.position);
            yield return new WaitForSeconds(2f);
            AudioManager.instance.PlaySound(Sound.ForceField, gameObject.transform.position);
        }
        else
        {
            AudioManager.instance.StopSound(Sound.ForceField);
            AudioManager.instance.PlaySound(Sound.ForceFieldStop, gameObject.transform.position);
        }
    }

    /// <summary>
    /// This method adds a set ammount of shield power to the player and calls 
    /// the <see cref="OnForceFieldPowerAdded"/> event.
    /// </summary>
    /// <param name="amount">The ammount of shield power the player received.</param>
    public void AddForceFieldPower(float amount)
    {
        CurrentForceFieldPower += amount;
        CurrentForceFieldPower = Mathf.Clamp(CurrentForceFieldPower, 0f, maxForceFieldPower);
        OnForceFieldPowerAdded?.Invoke(amount, ForceFieldPowerNormalized);
    }

    /// <summary>
    /// This method reduces a set ammount of shield power from the player and 
    /// calls the <see cref="OnForceFieldPowerUsed"/> event.
    /// </summary>
    /// <param name="amount">The ammount of shield power the player lost.</param>
    public void UseForceFieldPower(float amount)
    {
        CurrentForceFieldPower -= amount;
        OnForceFieldPowerUsed?.Invoke(amount, ForceFieldPowerNormalized);

        if (CurrentForceFieldPower <= 0)
        {
            ToggleForceField(false);
            OnForceFieldPowerConsumed?.Invoke();
        }
    }

    /// <summary>
    /// Resets the current force field power back to the default max value.
    /// </summary>
    public void Reset()
    {
        CurrentForceFieldPower = maxForceFieldPower;
    }

    /// <summary>
    /// Enables or disables the entire interaction possibilties that the player 
    /// has with the force field.
    /// </summary>
    public void EnableForceField(bool value)
    {
        if (!value && IsForceFieldEnabled)
        {
            ToggleForceField(false);
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


    /// <summary>
    /// This method checks if the force field is currently enabled while the 
    /// player is playing.
    /// </summary>
    /// <returns>True if the force field is enabled; otherwise, false.</returns>
    public bool IsForceFieldEnabled { set; get; } = false;

    /// <summary>
    /// This method checks if the interaction with the force field is allowed.
    /// </summary>
    /// <returns>True if the interaction is permissible; otherwise, false.</returns>
    public bool IsEnabled { set; get; }
}

