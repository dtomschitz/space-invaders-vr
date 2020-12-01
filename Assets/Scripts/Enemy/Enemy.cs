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
    private GameObject playerObject;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        player = Player.instance;
        playerCombat = player.combat as PlayerCombat;
        State = EnemyState.Seek;


        this.meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        meshRenderer.material = highlightMaterial;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        meshRenderer.material = defaultMaterial;
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
