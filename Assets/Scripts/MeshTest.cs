using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    [SerializeField] private int gridSize = 5;
    [SerializeField] private float gridScale = 1f;
    [SerializeField] private int perlinSeed = 0;
    [SerializeField] private float perlinScale = 1f;
    [SerializeField] private float maxHeight = 5f;

    private Vector2 perlinOffset;

    private void Start()
    {
        perlinOffset = CalculatePerlinOffset(perlinSeed);

        Mesh mesh = new Mesh();

        Vector3[] verts = new Vector3[(gridSize + 1) * (gridSize + 1)];
        for (int x = 0; x < gridSize + 1; x++)
        {
            for (int z = 0; z < gridSize + 1; z++)
            {
                int idx = IndexOf(x, z);
                float height = GetNoise(x * gridScale, z * gridScale) * maxHeight;
                verts[idx] = new Vector3(x * gridScale, height, z * gridScale);
            }
        }
        mesh.vertices = verts;

        int[] tris = new int[gridSize * gridSize * 6];
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                int idx = (gridSize * x + z) * 6;
                tris[idx] = IndexOf(x, z);
                tris[idx + 1] = IndexOf(x, z + 1);
                tris[idx + 2] = IndexOf(x + 1, z);
                tris[idx + 3] = IndexOf(x + 1, z);
                tris[idx + 4] = IndexOf(x, z + 1);
                tris[idx + 5] = IndexOf(x + 1, z + 1);
            }
        }
        mesh.triangles = tris;

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private int IndexOf(int x, int z)
    {
        return (gridSize + 1) * x + z;
    }

    private float GetNoise(float x, float z)
    {
        return Mathf.PerlinNoise(x * perlinScale + perlinOffset.x, z * perlinScale + perlinOffset.y);
    }

    private Vector2 CalculatePerlinOffset(int seed)
    {
        float s = ((seed + 37f) * 73f) % 113f;
        float x = s + 0.369f;
        float y = (113f - s) + 0.476f;
        return new Vector2(x, y);
    }
}
