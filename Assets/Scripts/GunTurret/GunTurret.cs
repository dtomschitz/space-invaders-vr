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

    [Header("Settings")]
    public float rate = 10f;

    private float shootingTimer;
    private bool shooting;

    void Start()
    {
        shootingTimer = 0f;

        if (turretPosition == GunTurretPosition.LEFT)
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

    void Update()
    {
        shootingTimer -= Time.deltaTime;

        //firePoint.transform.LookAt(laser.dot.transform);
        gun.transform.LookAt(laser.dot.transform);

        if (shootingTimer <= 0 && shooting)
        {
            Shoot();
            shootingTimer = .2f;
        }
    } 

    void Shoot()
    {
        Debug.Log("DWad");

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
}
