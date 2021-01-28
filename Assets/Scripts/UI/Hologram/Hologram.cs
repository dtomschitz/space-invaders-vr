using UnityEngine;

public class Hologram : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;

    public GameObject projectorLight;
    public GameObject hologram;

    public void ToggleHologram(bool value)
    {
        projectorLight.GetComponent<MeshRenderer>().material = value ? lightOnMaterial : lightOffMaterial;
        gameObject.SetActive(value);
    }
}
