using UnityEngine;

public class EmergencyLight : MonoBehaviour
{
    void Start()
    {
        Player.instance.OnNearDeath += () => Toggle(true);
        Player.instance.OnEntityDied += () => Toggle(false);
    }

    public void Toggle(bool active)
    {
        gameObject.SetActive(active);
    }
}
