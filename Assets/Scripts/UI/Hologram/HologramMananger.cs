using System;
using UnityEngine;

public class HologramMananger : MonoBehaviour
{
    public ForceFieldBar forceFieldBar;
    public HealthBar healthBar;

    void Start()
    {
        if (forceFieldBar == null) throw new NullReferenceException("ForceFieldBar class cannot be null");
        if (healthBar == null) throw new NullReferenceException("HealthBar class cannot be null");
    }

    /// <summary>
    /// Enables or disables the force field mana bar.
    /// </summary>
    /// <param name="active"></param>
    public void ShowForceFieldBar(bool active) => forceFieldBar.gameObject.SetActive(active);

    /// <summary>
    /// Enables or disables the health bar.
    /// </summary>
    /// <param name="active"></param>
    public void ShowHealthBar(bool active) => healthBar.gameObject.SetActive(active);

}
