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


    private Player player;
    private PlayerCombat playerCombat;
    protected override void Start()
    {
        base.Start();

        player = Player.instance;
        playerCombat = player.combat as PlayerCombat;
        State = EnemyState.Seek;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GetComponent<CapsuleCollider>().enabled = false;
        playerCombat.AddShieldPower(10f);
        Statistics.instance.AddKill();
        Destroy(gameObject, 2f);
    }
}
