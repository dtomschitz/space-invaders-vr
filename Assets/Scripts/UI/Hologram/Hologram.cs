using UnityEngine;

public class Hologram : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;

    public GameObject projectorLight;
    public GameObject hologram;
    public Animator animator;

    public void EnableHologram(bool value)
    {
        hologram.SetActive(value);
    }

    public void ToggleHologram(bool value)
    {
        projectorLight.GetComponent<MeshRenderer>().material = value ? lightOnMaterial : lightOffMaterial;
        Debug.Log(value ? "On" : "Off");
        animator.SetTrigger(value ? "On" : "Off");
    }
}
