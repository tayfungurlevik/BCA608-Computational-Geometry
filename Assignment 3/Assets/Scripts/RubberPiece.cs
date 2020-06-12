using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class RubberPiece : MonoBehaviour
{
    [SerializeField]
    private float k = 1.0f;
    
    public Vector3 speed;
    public Vector3 startPoint;
    public Vector3 endPoint;
    private Vector3 center;
    private float height = 0.1f;
    private float width = 0.01f;
    public float initialLength = 10.0f;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private MeshCollider meshCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        mesh = meshFilter.mesh;
        
        center = transform.position;
        startPoint =  new Vector3(0, 0, -initialLength / 2);
        endPoint =  new Vector3(0, 0, initialLength / 2);
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Vector3[] vertices = new Vector3[8]
        {
            startPoint,//0
            startPoint+new Vector3(width, 0, 0),//1
            startPoint+new Vector3(0, height, 0),//2
            startPoint+new Vector3(width, height, 0),//3
            endPoint+new Vector3(width, 0, 0),//4
            endPoint+new Vector3(width, height, 0),//5
            endPoint,//6
            startPoint+new Vector3(0,height,0)//7
        };
        mesh.vertices = vertices;
        int[] tris = new int[30]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1,
            1,5,4,
            1,3,5,
            2,7,3,
            3,7,5,
            0,6,2,
            2,6,7,
            0,6,1,
            1,6,4

        };
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveAndScaleRubber();
    }

    private void MoveAndScaleRubber()
    {
        var length = Vector3.Distance(startPoint, endPoint);
        if (length>0.05f)
        {
            transform.position += speed * Time.deltaTime;

            
            length -= k * Time.deltaTime;
            startPoint = new Vector3(0, 0, -length / 2);
            endPoint = new Vector3(0, 0, length / 2);
            GenerateMesh();
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.transform.position);
    }
}
