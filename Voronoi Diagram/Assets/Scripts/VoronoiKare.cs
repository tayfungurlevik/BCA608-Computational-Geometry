using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class VoronoiKare : MonoBehaviour
{
    private Mesh mesh;
    private MeshFilter meshFilter;
    public Vector3[] vertices = new Vector3[4];
    public int width=10;
    public Color color;
    public int i, j;
    private MeshCollider collider;
    internal Vector3 center;
    public int SiteIndex;
    private void OnEnable()
    {
        ControlPanel.OnKChanged += ControlPanel_OnKChanged;
        
        collider = GetComponent<MeshCollider>();
    }




    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        CalculateCorners();
        GenerateMesh();

    }





    public void GenerateMesh()
    {
        Triangulator tr = new Triangulator(vertices);
        int[] indices = tr.Triangulate();

        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {

            colors[i] = color;
        }
        Mesh mesh = new Mesh();
        Vector3[] vertexList = new Vector3[vertices.Length];

        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.colors = colors;
        // Set up game object with mesh;

        meshFilter.mesh = mesh;
        collider.sharedMesh = mesh;
    }

    private void ControlPanel_OnKChanged(int obj)
    {
        width = obj;
        Vector3 newPos = new Vector3();
        newPos.x = i * obj;
        newPos.y = j * obj;
        transform.position = newPos;
        center = newPos;
        CalculateCorners();
        GenerateMesh();

    }
    private void CalculateCorners()
    {
        vertices[0] =  (new Vector3(-1, -1, 0)).normalized* width*Mathf.Sqrt(2)/2 ;//Sol alt kose
        vertices[1] = (new Vector3(1, -1, 0)).normalized * width * Mathf.Sqrt(2) / 2;//Sag alt kose
        vertices[2] =  (new Vector3(1, 1, 0)).normalized* width* Mathf.Sqrt(2) / 2;//Sag ust kose
        vertices[3] =  (new Vector3(-1, 1, 0)).normalized * width * Mathf.Sqrt(2) / 2;//Sol ust kose
    }
    private void OnDestroy()
    {
        ControlPanel.OnKChanged -= ControlPanel_OnKChanged;
    }
}
