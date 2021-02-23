using UnityEngine;

/// <summary>
/// Class <c>EmergencyLight</c> is used to enable or disable the emergency light 
/// which is used to inform the player about his health if it drops below a 
/// specific point.
/// </summary>
public class EmergencyLight : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Player.instance.OnNearDeath += () => Toggle(true);
        Player.instance.OnEntityDied += () => Toggle(false);
    }

    /// <summary>
    /// Enables or disables the emergency light.
    /// </summary>
    public void Toggle(bool active)
    {
        animator.enabled = active;
        if (!active)
        {
            GetComponentInChildren<Light>().intensity = 0;
        }
    }
}
