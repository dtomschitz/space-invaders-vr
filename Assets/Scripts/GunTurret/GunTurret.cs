using UnityEngine;

public enum GunTurretPosition
{
    LEFT,
    RIGHT
} 

public class GunTurret : MonoBehaviour
{
    public GunTurretPosition position;

    void Start()
    {
        if (position == GunTurretPosition.LEFT)
        {
            Player.instance.controls.OnLeftTriggerButtonPressed += OnShoot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnShoot()
    {
        if (GameState.instance.IsInTargetAcquisition)
        {
            Debug.Log("Shoot");
        }
    }
}
