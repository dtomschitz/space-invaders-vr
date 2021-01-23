using UnityEngine;

public class GunManager : MonoBehaviour
{
    #region Singelton

    public static GunManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public Gun leftGun;
    public Gun rightGun;

    public void EnableGuns(bool value)
    {
        this.leftGun.IsEnabled = value;
        this.rightGun.IsEnabled = value;
    }

    public void EnableShooting(bool value)
    {
        this.leftGun.IsShootingEnabled = value;
        this.rightGun.IsShootingEnabled = value;
    }
}
