using UnityEngine;

public class GunTurret : MonoBehaviour
{
    public GunTurretPosition turretPosition;
    public float defaultLength;
    
    [Header("Prefabs")]
    public Projectile projectilePrefab;
    public GameObject muzzlePrefab;

    [Header("Default GameObjects")]
    public GameObject firePoint;

    [Header("Gun Objects")]
    public GameObject gun;

    [Header("Settings")]
    public float rate = .2f;
    public float minY;

    GameObject hand;
    GunTurretLaser laser;

    float shootingTimer;
    bool shooting;

    void Start()
    {
        shootingTimer = 0f;

        InitializeEvents(turretPosition);
        FindGameObjects(turretPosition);
    } 

    void Update()
    {
        float y = gun.transform.position.y;
        if (y > 0 && y >= minY || y < 0 && y <= minY)
        {
            firePoint.transform.LookAt(laser.dot.transform);
            gun.transform.LookAt(laser.dot.transform);
        }

        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && shooting)
        {
            Shoot();
            shootingTimer = rate;
        }
    } 

    void Shoot()
    {
        if (GameState.instance.IsInTargetAcquisition)
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

            AudioManager.instance.PlaySound(Sound.Shoot, firePoint.transform.position);
        }
    }

    void FindGameObjects(GunTurretPosition position) 
    {
        hand = GameObject.FindGameObjectWithTag(position == GunTurretPosition.LEFT ? "LeftHand" : "RightHand");
        laser = GameObject.FindGameObjectWithTag(position == GunTurretPosition.LEFT ? "LeftGunLaser" : "RightGunLaser").GetComponent<GunTurretLaser>();
    }

    void InitializeEvents(GunTurretPosition position)
    {
        if (position == GunTurretPosition.LEFT)
        {
            Player.instance.controls.OnLeftTriggerButtonPressed += () => shooting = true;
            Player.instance.controls.OnLeftTriggerButtonPressCanceled += () => shooting = false;

        }
        else
        {
            Player.instance.controls.OnRightTriggerButtonPressed += () => shooting = true;
            Player.instance.controls.OnRightTriggerButtonPressCanceled += () => shooting = false;
        }
    }

    public GameObject Hand
    {
        get => hand;
    }
}
