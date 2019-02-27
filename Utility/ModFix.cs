using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModFix
{
    /// <summary>
    /// Modulo operation that supports negative integers more intuitively.
    /// </summary>
    /// <returns>Returns x mod m</returns>
    public static int Mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }
}
