using PixelsForGlory.ComputationalSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiSite : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshCollider collider;
    public List<Vector2> vertices;
    public int voronoiIndex;
    public Color color;
    public Vector3 siteCenter;
    
    
    
    // Start is called before the first frame update

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.KeypadPlus))
    //    {
    //        noktaScale += 1;
    //        centerSprite.localScale = new Vector3(noktaScale, noktaScale, 1);
    //    }
    //    if (Input.GetKeyDown(KeyCode.KeypadMinus))
    //    {
    //        noktaScale -= 1;
    //        centerSprite.localScale = new Vector3(noktaScale, noktaScale, 1);
    //    }
    //}
    public void GenerateSiteMesh()
    {
        meshFilter = GetComponent<MeshFilter>();
        collider = GetComponent<MeshCollider>();
        Triangulator tr = new Triangulator(vertices);
        int[] indices = tr.Triangulate();

        Color[] colors = new Color[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {

            colors[i] = color;
        }
        Mesh mesh = new Mesh();
        Vector3[] vertexList = new Vector3[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            vertexList[i] = vertices[i].ToVector3();
        }
        mesh.vertices = vertexList;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.colors = colors;
        // Set up game object with mesh;

        meshFilter.mesh = mesh;
        collider.sharedMesh = mesh;
        
    }
    
}
