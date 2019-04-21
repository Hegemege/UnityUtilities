using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities
{
    public static class MonoBehaviourExtensions
    {
        public static string MultiLogDelimiter = ", ";
        public static void Log(this MonoBehaviour mb, params object[] list)
        {
            Debug.Log(string.Join(MonoBehaviourExtensions.MultiLogDelimiter, list));
        }
    }
}
