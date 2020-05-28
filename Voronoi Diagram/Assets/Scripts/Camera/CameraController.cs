using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    [SerializeField]
    private float Speed;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            camera.orthographicSize += 10;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            camera.orthographicSize -= 10;
        }
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, vertical, 0);
        transform.position += move.normalized * Speed * Time.deltaTime;
    }
}
