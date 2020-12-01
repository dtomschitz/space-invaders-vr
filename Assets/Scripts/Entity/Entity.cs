using System;
using UnityEngine;

/// <summary>
/// Class <c>Entity</c> is used as the base class for all entities such as the
/// player or the enemies. It contains all the necessary classes in
/// order to handle animations, stats and the combat relevant stuff.
///
/// The class also subscribes to the methods <see cref="EntityStats.OnDamaged"/>
/// and <see cref="EntityStats.OnDeath"/> which can then be overridden by the
/// inheriting classes.
///
/// In this class the method <see cref="OnHit(Entity)"/> is
/// declared aswell which will be called if the entity got hit by another one.
/// </summary>
public class Entity : MonoBehaviour
{
    [Header("Entity Base")]
    public EntityStats stats;
    public EntityCombat combat;

    protected virtual void Start()
    {
        if (stats == null) throw new NullReferenceException("Entity stats paramter cannot be null");
        if (combat == null) throw new NullReferenceException("Entity combat paramter cannot be null");

        stats.OnDamaged += OnDamaged;
        stats.OnDeath += OnDeath;
    }

    /// <summary>
    /// This method gets called if the entity got hit by another one.
    /// <para>
    /// It will then check whether the entity is already dead or not. If it is
    /// not dead, the stats will be updated and the hit animation will be played.
    /// </para>
    /// </summary>
    /// <param name="offender">The entity which attacked</param>
    public virtual void OnHit(Entity offender)
    {
        if (stats.IsDead) return;
        offender.combat.Attack(stats);
    }

    /// <summary>
    /// This method gets called if the entity got attacked and the damage applied.
    /// </summary>
    /// <param name="damage">The ammount of damage the entity received.</param>
    public virtual void OnDamaged(float damage)
    {
    }

    /// <summary>
    /// This method gets called if the entity died.
    /// </summary>
    public virtual void OnDeath()
    {
        if (stats.IsDead) return;
    }
}
