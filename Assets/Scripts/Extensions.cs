using UnityEngine;
using System.Collections;

public static class Extensions
{
    public static Vector3 ZMask(this Vector3 vec)
    {
        return new Vector3(vec.x, vec.y, 0f);
    }
}
