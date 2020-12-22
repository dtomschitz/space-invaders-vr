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
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        GameState.instance.SetState(GameStateType.GameOver);
    }

    protected override void OnDamaged(float damage)
    {
        if (CurrentHealth > 20f)
        {
            OnNearDeath?.Invoke();
        }
    }

    public void HideHands()
    {
        leftHand.Hide();
        rightHand.Hide();
    }

    public void ShowHands()
    {
        rightHand.Show();
        rightHand.Show();
    }
}
