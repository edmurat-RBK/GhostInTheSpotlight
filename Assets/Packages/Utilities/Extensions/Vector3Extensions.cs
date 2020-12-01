using UnityEngine;

public static class Vector3Extensions
{
    public static Vector2 ToVector2(this Vector3 vector)
    {
        return vector;
    }

    public static Vector2 ToVector2(this Vector3 vector, AxisPair pair)
    {
        switch(pair)
        {
            case AxisPair.XY:
                return vector;
            case AxisPair.XZ:
                return new Vector2(vector.x, vector.z);
            case AxisPair.YX:
                return new Vector2(vector.y, vector.x);
            case AxisPair.YZ:
                return new Vector2(vector.y, vector.z);
            case AxisPair.ZX:
                return new Vector2(vector.z, vector.x);
            case AxisPair.ZY:
                return new Vector2(vector.z, vector.y);
            default:
                Debug.LogError("Error, enum never read !");
                return vector;
        }
    }

    public static Vector3 SetX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 SetY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 SetZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector3 AddVector(this Vector3 vector, Vector3 value)
    {
        return new Vector3(vector.x + value.x, vector.y + value.y, vector.z + value.z);
    }

    public static Vector3 AddVector(this Vector3 vector, Vector2 value)
    {
        return new Vector3(vector.x + value.x, vector.y + value.y, vector.z);
    }

    public static Vector3 AddX(this Vector3 vector, float value)
    {
        return new Vector3(vector.x + value, vector.y, vector.z);
    }

    public static Vector3 AddY(this Vector3 vector, float value)
    {
        return new Vector3(vector.x, vector.y + value, vector.z);
    }

    public static Vector3 AddZ(this Vector3 vector, float value)
    {
        return new Vector3(vector.x, vector.y, vector.z + value);
    }

    public static void Print(this Vector3 _vector)
    {
        Debug.Log("(" + _vector.x.ToString("0.0#######") + ", " + _vector.y.ToString("0.0#######") + ", " + _vector.z.ToString("0.0#######") + ")");
    }

    public static Vector3 Inverse(this Vector3 a)
    {
        return new Vector3(1 / a.x, 1 / a.y, 1 / a.z);
    }

    /// <summary>
    /// Returns the square distance between two vector3 positions. Faster that Vector3.distance.
    /// </summary>
    /// <param name="first">first point</param>
    /// <param name="second">second point</param>
    /// <returns>squared distance</returns>
    public static float SqrDistance(this Vector3 first, Vector3 second)
    {
        return (first.x - second.x) * (first.x - second.x) +
        (first.y - second.y) * (first.y - second.y) +
        (first.z - second.z) * (first.z - second.z);
    }

    /// <summary>
    /// Returns the middle point between two vector3 positions.
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static Vector3 MidPoint(this Vector3 first, Vector3 second)
    {
        return new Vector3((first.x + second.x) * 0.5f, (first.y + second.y) * 0.5f, (first.z + second.z) * 0.5f);
    }

    /// <summary>
    /// Absolute value of components
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }

    /// <summary>
    /// Vector3.Project, onto a plane
    /// </summary>
    /// <param name="v"></param>
    /// <param name="planeNormal"></param>
    /// <returns></returns>
    public static Vector3 ProjectOntoPlane(this Vector3 v, Vector3 planeNormal)
    {
        return v - Vector3.Project(v, planeNormal);
    }

    /// <summary>
    /// Returns the normal of the triangle formed by the 3 vectors
    /// </summary>
    /// <param name="vec1"></param>
    /// <param name="vec2"></param>
    /// <param name="vec3"></param>
    /// <returns></returns>
    public static Vector3 Vector3Normal(Vector3 vec1, Vector3 vec2, Vector3 vec3)
    {
        return Vector3.Cross((vec3 - vec1), (vec2 - vec1));
    }

    /// <summary>
    /// Returns the central point in a vector3 array.
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static Vector3 Center(this Vector3[] points)
    {
        Vector3 ret = Vector3.zero;
        foreach (var p in points)
        {
            ret += p;
        }
        ret /= points.Length;
        return ret;
    }
}
