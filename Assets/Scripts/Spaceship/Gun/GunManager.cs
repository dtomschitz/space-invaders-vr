using UnityEngine;

/// <summary>
/// Class <c>GunManager</c> is used the handle the left and right gun of the 
/// spaceship.
/// </summary>
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


    /// <summary>
    /// This method enables or disables the entire interaction possibles with all guns.
    /// </summary>
    public void EnableGuns(bool value)
    {
        this.leftGun.IsEnabled = value;
        this.rightGun.IsEnabled = value;
    }

    /// <summary>
    /// This method enables or disables shooting for all guns.
    /// </summary>
    public void EnableShooting(bool value)
    {
        this.leftGun.IsShootingEnabled = value;
        this.rightGun.IsShootingEnabled = value;
    }
}
