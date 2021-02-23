using UnityEngine;

/// <summary>
/// Class <c>Gun</c> is used for handling the player actions which are 
/// associated with on of the guns. The class implements all the needed methods
/// for implementing the Gun functionailities as well as the update methods for
/// the position and rotation.
/// </summary>
public class Gun : MonoBehaviour
{
    [Header("Settings")]
    public GunPosition position;
    public float defaultLength;
    
    [Header("Prefabs")]
    public Projectile projectilePrefab;
    public GameObject muzzlePrefab;

    [Header("Gun Objects")]
    public GameObject gun;
    public GunLaser laser;
    public GameObject firePoint;

    Vector3 defaultPosition;
    Quaternion defaultRotation;

    bool isShootingEnabled = true;
    bool isEnabled = false;


    /// <summary>
    /// Sets the default postions and subsribes to the trigger event based on the 
    /// set position of the gun.
    /// </summary>
    void Start()
    {
        if (position == GunPosition.LEFT)
        {
            Player.instance.controls.OnLeftTriggerButtonPressed += Shoot;
        }
        else
        {
            Player.instance.controls.OnRightTriggerButtonPressed += Shoot;
        }

        GameState.instance.OnGameStateChanged += OnGameStateChanged;
        defaultPosition = gameObject.transform.position;
        defaultRotation = gameObject.transform.rotation;
    } 

    /// <summary>
    /// Rotates the specific gun to the position the player is aiming with the
    /// associated controller.
    /// </summary>
    void Update()
    {
        if (IsEnabled)
        {
            firePoint.transform.LookAt(laser.dot.transform);
            gun.transform.LookAt(laser.dot.transform);
        }
    }

    /// <summary>
    /// This method gets called if the game state has been changed. In case the
    /// state is not set to PreInGame or InGame both positions of the guns will
    /// get resetted. 
    /// </summary>
    /// <param name="newState">The new game state.</param>
    void OnGameStateChanged(GameStateType newState)
    {
        if (newState != GameStateType.PreInGame || newState != GameStateType.InGame)
        {
            gameObject.transform.position = defaultPosition;
            gameObject.transform.rotation = defaultRotation;
        }
    }

    /// <summary>
    /// This method gets called if the player has pressed the according trigger.
    /// If the game state is currently set to InGame or IsPreInGame and the 
    /// shooting is enabled a new projectile as well as the muzzle flash will 
    /// get instantiated. 
    /// </summary>
    void Shoot()
    {
        if (IsShootingEnabled && (GameState.instance.IsInGame || GameState.instance.IsPreInGame))
        {   
            if (muzzlePrefab != null)
            {
                GameObject muzzleFlash = Instantiate(muzzlePrefab, firePoint.transform.position, Quaternion.identity);
                Destroy(muzzleFlash, 3f);
            }

            if (projectilePrefab != null)
            {
                Projectile projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);
                projectile.transform.localRotation = firePoint.transform.rotation;
            }

            AudioManager.instance.PlaySound(Sound.Projectile, firePoint.transform.position);
        }
    }

    /// <summary>
    /// This method checks if shooting is enabled for the specific gun.
    /// </summary>
    /// <returns>True if shooting is enaled; otherwise, false.</returns>
    public bool IsShootingEnabled
    {
        set
        {
            isShootingEnabled = value;
            laser.dot.SetActive(value);
        }

        get => isShootingEnabled;
    }

    /// <summary>
    /// This method checks if the entire interaction with the gun is enabled or disabled.
    /// </summary>
    /// <returns>True if it is enaled; otherwise, false.</returns>
    public bool IsEnabled
    {
        set
        {
            isEnabled = value;
            laser.IsEnabled = value;
        }

        get => isEnabled;
    }
}
