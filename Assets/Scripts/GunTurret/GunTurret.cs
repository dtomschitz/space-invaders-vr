using UnityEngine;

public class GunTurret : MonoBehaviour
{
    public GunTurretPosition turretPosition;
    public float defaultLength;
    
    public float minY;

    [Header("Prefabs")]
    public Projectile projectilePrefab;
    public GameObject muzzlePrefab;

    [Header("Default GameObjects")]
    public GunTurretLaser laser;
    public GameObject hand;
    public GameObject firePoint;

    [Header("Gun Objects")]
    public GameObject gun;
    public GameObject anchor;

    void Start()
    {
        if (turretPosition == GunTurretPosition.LEFT)
        {
            Player.instance.controls.OnLeftTriggerButtonPressed += OnShoot;
        }
        else
        {
            Player.instance.controls.OnRightTriggerButtonPressed += OnShoot;
        }
    }

    void Update()
    {
        float dotY = laser.dot.transform.position.y;
        if (dotY > minY)
        {
            firePoint.transform.LookAt(laser.dot.transform);
            gun.transform.LookAt(laser.dot.transform);
        }
    }

    void OnShoot()
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
        }
    }
}
