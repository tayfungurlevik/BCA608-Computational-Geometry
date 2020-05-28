using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonMesh : MonoBehaviour
{
    public PolygonInfo polygonInfo;
    private Segment[] segments;
    private Vector3[] points;
    private MeshFilter meshFilter;
    
    // Start is called before the first frame update
    void Start()
    {
        segments = polygonInfo.Segments;
        points = new Vector3[segments.Length];
        for (int i = 0; i < segments.Length; i++)
        {
            points[i] = segments[i].StartPoint;
        }
        Triangulator tr = new Triangulator(points);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[points.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(points[i].x, points[i].y, 0);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = msh;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
