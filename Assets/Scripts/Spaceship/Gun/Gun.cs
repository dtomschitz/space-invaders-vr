using UnityEngine;

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

    void Update()
    {
        if (IsEnabled)
        {
            firePoint.transform.LookAt(laser.dot.transform);
            gun.transform.LookAt(laser.dot.transform);
        }
    }

    void OnGameStateChanged(GameStateType newState)
    {
        if (newState != GameStateType.PreInGame || newState != GameStateType.InGame)
        {
            gameObject.transform.position = defaultPosition;
            gameObject.transform.rotation = defaultRotation;
        }
    }

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

    public bool IsShootingEnabled
    {
        set
        {
            isShootingEnabled = value;
            laser.dot.SetActive(value);
        }

        get => isShootingEnabled;
    }

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
