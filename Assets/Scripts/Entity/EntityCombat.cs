using System;
using UnityEngine;

/// <summary>
/// Class <c>EntityCombat</c> is the base class which handels combat related
/// stuff such as changing the combat state and so the behvaior of the entity or
/// updating the shield power.
/// </summary>
public class EntityCombat : MonoBehaviour
{
    public EntityCombatState State { get; protected set; }

    private EntityStats stats;

    protected virtual void Start()
    {
        stats = GetComponent<EntityStats>();
        if (stats == null) throw new NullReferenceException("Entity stats paramter cannot be null");

        State = EntityCombatState.Idle;
    }

    /// <summary>
    /// This method will update the entity stats of the attacked entity based on
    /// the stats of the given entity.
    /// </summary>
    /// <param name="stats">The stats of the opponent to attack.</param>
    public void Attack(Entity entity)
    {
        entity.stats.Damage(this.stats.damage);
    }

    /// <summary>
    /// This method will update the entity stats of the attacked entity based on
    /// the current set damage.
    /// </summary>
    /// <param name="stats">The stats of the opponent to attack.</param>
    public void Attack(EntityStats stats)
    {
        stats.Damage(this.stats.damage);
    }

    /// <summary>
    /// Sets the new combat state
    /// </summary>
    public void SetState(EntityCombatState state)
    {
        if (state == State || !GameState.instance.IsInGame) return;
        State = state;
    }

    public bool IsShooting
    {
        get { return State == EntityCombatState.Shooting; }
    }

    public bool IsBlocking
    {
        get { return State == EntityCombatState.Blocking; }
    }
}
