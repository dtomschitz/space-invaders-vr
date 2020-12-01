public class Player : Entity
{
    #region Singelton

    public static Player instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public override void OnDeath()
    {
        base.OnDeath();
        GameState.instance.SetState(GameStateType.GameOver);
    }


}
