using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Maps a float value from one range of values to another.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="y1"></param>
    /// <param name="y2"></param>
    /// <returns></returns>
    public static float Map(this float value, float x1, float x2, float y1, float y2)
    {
        return y1 + (y2 - y1) * (value - x1) / (x2 - x1);
    }
}
