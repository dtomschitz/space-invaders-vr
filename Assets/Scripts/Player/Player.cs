using UnityEngine;

public class Player : Entity
{
    #region Singelton

    public static Player instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public PlayerControls controls;

    [Header("Hands")]
    public Hand leftHand;
    public Hand rightHand;

    public delegate void NearDeath();
    public event NearDeath OnNearDeath;

    protected override void Start()
    {
        base.Start();
        AudioManager.instance.PlaySound(Sound.Engine, gameObject.transform.position);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        ForceField.instance.IsForceFieldEnabled = false;
        GameState.instance.SetState(GameStateType.GameOver);
    }

    protected override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);

        if (CurrentHealth <= 20f)
        {
            OnNearDeath?.Invoke();
        }
    }

    public void EnableHands(bool value)
    {
        leftHand.EnableHand(value);
        rightHand.EnableHand(value);
    }
}
