using UnityEngine;

public static class Vector3Extensions
{
    public static Vector2 XZ(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }
    
    public static Vector3 ProjectPlaneY(this Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }
    
    public static void ClampMagnitude(this ref Vector2 v, float maxLength)
    {
        var sqrLength = v.sqrMagnitude;
        if (sqrLength > maxLength * maxLength)
        {
            v = v * (maxLength / Mathf.Sqrt(sqrLength));
        }
    }
    
    public static void ClampMagnitude(this ref Vector3 v, float maxLength)
    {
        var sqrLength = v.sqrMagnitude;
        if (sqrLength > maxLength * maxLength)
        {
            v = v * (maxLength / Mathf.Sqrt(sqrLength));
        }
    }
}
