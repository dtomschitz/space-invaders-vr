using UnityEngine;

public class GunTurret : MonoBehaviour
{
    public GunTurretPosition turretPosition;
    public float defaultLength;

    [Header("Prefabs")]
    public Projectile projectilePrefab;
    public GameObject muzzlePrefab;

    [Header("Default GameObjects")]
    public GunTurretLaser laser;
    public GameObject hand;
    public GameObject firePoint;

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
        //gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, 1);
        gameObject.transform.LookAt(laser.dot.transform);
    }

    void OnShoot()
    {
        if (GameState.instance.IsInTargetAcquisition)
        {
            Projectile projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);
            projectile.transform.localRotation = firePoint.transform.rotation;
        }
    }
}
