using UnityEngine;

public class PointPlaceholder:MonoBehaviour
{
    
    public int scale = 50;
    public Color color;
    public int Index;
    private void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            scale++;
            transform.localScale = new Vector3(scale, scale, scale);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            scale--;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }


}