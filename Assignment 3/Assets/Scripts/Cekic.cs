using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cekic : MonoBehaviour
{
    private new Camera camera;
    [SerializeField]
    private LayerMask tahtaLayeri;
    [SerializeField]
    private GameObject civiPrefab;
    private void Start()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        //Sol mouse tiklandiginda
        if (Input.GetMouseButtonDown(0))
        {
            //Cameradan isin firlat
            Ray ray=camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo,100, tahtaLayeri))
            {
                //Debug.Log(hitInfo.point);
                Instantiate(civiPrefab, hitInfo.point, Quaternion.identity);
            }
        }
    }
}
