using UnityEngine;

public static class Vector2Extensions
{
    public static Vector3 ToVector3(this Vector2 vector)
    {
        Vector3 v = new Vector3();
        v.x = vector.x;
        v.y = vector.y;
        v.z = 0;
        return v;
    }
}
