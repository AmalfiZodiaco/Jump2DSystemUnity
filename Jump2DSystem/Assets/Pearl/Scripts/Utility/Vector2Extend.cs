using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extend
{
    //è più eficente di Distance
    public static float DistancePow2(Vector2 pointA, Vector2 pointB)
    {
        return (pointA - pointB).magnitude;
    }

}
