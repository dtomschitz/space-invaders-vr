using UnityEngine;

/// <summary>
/// Class <c>EntityStats</c> is used as the base class for all statistic
/// releavant things for the entity such as the health and damage. 
/// </summary>
public class EntityStats : MonoBehaviour
{
    public float maxHealth;
    public float damage;

    public float CurrentHealth { get; set; }
    public event System.Action OnDeath;

    public delegate void Damaged(float damage);
    public event Damaged OnDamaged;

    public delegate void Healed(float amount);
    public event Healed OnHealed;

    protected virtual void Start()
    {
        this.CurrentHealth = maxHealth;
    }


    /// <summary>
    /// This method gets called if the entity took damage and will update the
    /// health of the entity based on the given damage ammount. It will also
    /// fire the <see cref="OnDamaged"/> event. The event <see cref="OnDeath"/>
    /// will get fired aswell if the health dropped below 0.
    /// </summary>
    /// <param name="damage">The damage which the entity took.</param>
    /// <param name="item">The item with which the damage where dealed.</param>
    public virtual void Damage(float damage)
    {
        Debug.Log(damage);
        damage = Mathf.Clamp(damage, 0, maxHealth);
        Debug.Log(damage);

        Debug.Log(CurrentHealth);

        CurrentHealth -= damage;
        OnDamaged?.Invoke(damage);

        Debug.Log(CurrentHealth);

        if (IsDead) OnDeath?.Invoke();
    }

    /// <summary>
    /// This method gets called if the entity got healed and will then update
    /// the health of the entity based on the given amount of live points. It will
    /// also fire the <see cref="OnHealed"/> event.
    /// </summary>
    /// <param name="amount">The ammount of healt points the player received.</param>
    public virtual void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        OnHealed?.Invoke(amount);
    }

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
