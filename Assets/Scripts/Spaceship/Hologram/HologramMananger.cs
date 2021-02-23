using UnityEngine;

/// <summary>
/// Class <c>HologramMananger</c> is used to enable or disable the holograms.
/// </summary>
public class HologramMananger : MonoBehaviour
{
    public Hologram statsHologram;
    public Hologram killsHologram;
    public Hologram timeHologram;

    /// <summary>
    /// Enables or disables the all game object which are containing the 
    /// different holograms.
    /// </summary>
    public void EnableHolograms(bool value)
    {
        statsHologram.EnableHologram(value);
        killsHologram.EnableHologram(value);
        timeHologram.EnableHologram(value);
    }

    /// <summary>
    /// Enables or disables the all holograms and starts the according animation
    /// for them.
    /// </summary>
    public void ToggleHologram(bool value)
    {
        statsHologram.ToggleHologram(value);
        killsHologram.ToggleHologram(value);
        timeHologram.ToggleHologram(value);
    }
}
