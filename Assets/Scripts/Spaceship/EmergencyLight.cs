using UnityEngine;

public class EmergencyLight : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Player.instance.OnNearDeath += () => Toggle(true);
        Player.instance.OnEntityDied += () => Toggle(false);
    }

    public void Toggle(bool active)
    {
        animator.enabled = active;
        if (!active)
        {
            GetComponentInChildren<Light>().intensity = 0;
        }
    }
}
