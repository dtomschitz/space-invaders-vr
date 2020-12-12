using UnityEngine;

public class EmergencyLight : MonoBehaviour
{
    void Start()
    {
        Player.instance.OnNearDeath += Enable;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
