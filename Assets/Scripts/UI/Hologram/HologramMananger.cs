using UnityEngine;

public class HologramMananger : MonoBehaviour
{
    public Hologram statsHologram;
    public Hologram killsHologram;
    public Hologram timeHologram;

    public void ToggleHologram(bool value)
    {
        statsHologram.ToggleHologram(value);
        killsHologram.ToggleHologram(value);
        timeHologram.ToggleHologram(value);
    }
}
