using UnityEngine;

/// <summary>
/// Class <c>Entity</c> is used as the base class for all entities such as the
/// player or the enemies. It contains all the necessary methods in  order to 
/// handle animations, stats and the combat relevant stuff.
/// </summary>
public class Entity : MonoBehaviour
{
    [Header("Entity Stats")]
    public float maxHealth;
    public float damage;

    public delegate void EntityDied();
    public event EntityDied OnEntityDied;

    public delegate void EntityDamaged(float damage, float currentNormalizedHealth);
    public event EntityDamaged OnEntityDamaged;

    public delegate void EntityHealed(float amount, float currentNormalizedHealth);
    public event EntityHealed OnEntityHealed;

    protected virtual void Start()
    {
        CurrentHealth = maxHealth;
    }

    /// <summary>
    /// This method can be called in order to damage the entity. It will update the
    /// health of the entity based on the given damage ammount. It will also
    /// call the <see cref="OnDamaged"/> method as well as the <see cref="OnDeath"/>
    /// method if the health dropped below 0.
    /// </summary>
    /// <param name="damage">The damage which the entity took.</param>
    public void Damage(float damage)
    {
        damage = Mathf.Clamp(damage, 0, maxHealth);
        CurrentHealth -= damage;
        OnDamaged(damage);

        if (IsDead) OnDeath();
    }

    /// <summary>
    /// This method gets called if the entity got attacked and the damage applied.
    /// </summary>
    /// <param name="damage">The ammount of damage the entity received.</param>
    protected virtual void OnDamaged(float damage)
    {
        OnEntityDamaged?.Invoke(damage, HealthNormalized);
    }

    /// <summary>
    /// This method gets called if the entity died.
    /// </summary>
    protected virtual void OnDeath()
    {
        OnEntityDied?.Invoke();
    }

    /// <summary>
    /// This method can heal the entity based on the given amount of live points. 
    /// It will also fire the <see cref="OnEntityHealed"/> event.
    /// </summary>
    /// <param name="amount">The ammount of health points the player received.</param>
    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        OnEntityHealed?.Invoke(amount, HealthNormalized);
    }

    /// <summary>
    /// This method will call the <see cref="Damage(float)"/> of the attacked entity.
    /// </summary>
    /// <param name="entity">The attacked entity.</param>
    public void Attack(Entity entity)
    {
        entity.Damage(damage);
    }

    /// <summary>
    /// Returns the current health of the entity.
    /// </summary>
    public float CurrentHealth { get; set; }

    /// <summary>
    /// Returns the normalized health.
    /// </summary>
    public float HealthNormalized
    {
        get { return CurrentHealth / maxHealth; }
    }

    /// <summary>
    /// Determinates whether the health of the entity is full or not. If its true
    /// the method will return true; otherwise false.
    /// </summary>
    public bool HasFullLife
    {
        get { return CurrentHealth == maxHealth; }
    }

    /// <summary>
    /// Determinates whether the entity is dead or not. If the entity is dead
    /// the method will return true; otherwise false.
    /// </summary>
    public bool IsDead
    {
        get { return CurrentHealth <= 0; }
    }
}
