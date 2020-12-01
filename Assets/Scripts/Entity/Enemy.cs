using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    
    public Material defaultMaterial;
    public Material highlightMaterial;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        meshRenderer.material = highlightMaterial;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        meshRenderer.material = defaultMaterial;
    }
}
