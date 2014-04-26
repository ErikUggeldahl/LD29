using UnityEngine;
using System.Collections;

public class TerrainEdgeCollider : MonoBehaviour
{
    [SerializeField]
    int vertexCount;

    EdgeCollider2D collider;
    Mesh mesh;

    void Start()
    {
        collider = GetComponent<EdgeCollider2D>();
        mesh = GetComponent<MeshFilter>().mesh;

        SetupVerts();
    }
	
    void SetupVerts()
    {
        var points = new Vector2[vertexCount + 1];

        for (int i = 0; i < vertexCount; i++)
            points[i] = mesh.vertices[i];

        points[vertexCount] = mesh.vertices[0];

        collider.points = points;
    }
}
