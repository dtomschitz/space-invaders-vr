using UnityEngine;

public enum EnemyState
{
    Attack,
    Strafe,
    Avoid,
    Seek
}

public class Enemy : Entity
{
    [Header("Materials")]
    public Material defaultMaterial;
    public Material highlightMaterial;

    public EnemyState State { get; protected set; }

    protected override void Start()
    {
        base.Start();
        State = EnemyState.Seek;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GetComponent<SphereCollider>().enabled = false;
        (Player.instance.combat as PlayerCombat).AddShieldPower(10f);
        Statistics.instance.AddKill();

        //TODO: Add Explosion

        Destroy(gameObject, 1f);
    }
}
