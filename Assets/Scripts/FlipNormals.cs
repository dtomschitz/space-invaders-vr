using System.Linq;
using UnityEngine;

public class FlipNormals : MonoBehaviour
{
    void Start()
    {
        /*Mesh mesh = this.GetComponent<MeshFilter>().mesh;

        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -1 * normals[i];
        }

        mesh.normals = normals;

        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] triangles = mesh.GetTriangles(i);
            for (int j = 0; j < triangles.Length; j += 3)
            {
                int temp = triangles[i];
                triangles[j] = triangles[j + 1];
                triangles[j + 1] = temp;
            }

            mesh.SetTriangles(triangles, i);
        }*/

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }
}
