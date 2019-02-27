using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    /// <summary>
    /// Fisher-Yates shuffle using System.Random generator.
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    /// <summary>
    /// Fisher-Yates shuffle but for usage with UnityEngine.Random generator
    /// </summary>
    /// <param name="list"></param>
    /// <param name="rng"></param>
    /// <typeparam name="T"></typeparam>
    public static void Shuffle<T>(this IList<T> list, UnityEngine.Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
