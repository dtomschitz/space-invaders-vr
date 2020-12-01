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

    public override void OnDeath()
    {
        base.OnDeath();
        GameState.instance.SetState(GameStateType.GameOver);
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
