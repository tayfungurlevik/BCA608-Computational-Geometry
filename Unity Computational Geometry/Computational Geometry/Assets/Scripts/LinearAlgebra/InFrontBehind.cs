using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrontBehind : MonoBehaviour
{
    public Transform you;
    public Transform enemy;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 youForward = you.forward;
        Vector3 youToEnemy = enemy.position - you.position;
        float dotProduct = Vector3.Dot(youForward, youToEnemy);
        if (dotProduct >= 0f)
        {
            Debug.Log("Infront");
        }
        else
            Debug.Log("Behind");
    }
}
