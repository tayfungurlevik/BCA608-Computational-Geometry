using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    
    public SimplePolygon poligon;
    public PolygonInfo polygonInfo;
    private Vector3[] koseler;
    private void OnEnable()
    {
        poligon.OnPolygonGenerated += Poligon_OnPolygonGenerated;
    }

    private void Poligon_OnPolygonGenerated(Vector3[] obj,Vector3 q,Segment[] segments)
    {
        koseler = obj;
        transform.position = q;
    }

    private void Start()
    {

        koseler = poligon.points.ToArray();
        
        
    }
    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 movementInput = new Vector3(horizontal, vertical,0);
        transform.position += movementInput.normalized * speed*Time.deltaTime;
        //for (int i = 0; i < koseler.Length; i++)
        //{
        //    Debug.DrawLine(transform.position, koseler[i],Color.blue);
        //}
    }
    private void OnDisable()
    {
        
    }
}
