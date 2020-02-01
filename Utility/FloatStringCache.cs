using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace UnityUtilities
{
    public static class FloatStringCache
    {
        private static Dictionary<int, Dictionary<int, string>> Cache;

        public static string Get(float value, int decimals, char decimalPoint = '.')
        {
            if (Cache == null)
            {
                Cache = new Dictionary<int, Dictionary<int, string>>();
            }

            if (!Cache.ContainsKey(decimals))
            {
                Cache[decimals] = new Dictionary<int, string>();
            }

            var decimalBase = Mathf.Pow(10, decimals);
            var truncatedInteger = Mathf.FloorToInt(value * decimalBase);
            if (Cache[decimals].ContainsKey(truncatedInteger))
            {
                return Cache[decimals][truncatedInteger];
            }

            var format = string.Format("F{0}", decimals);
            var addition = (truncatedInteger / decimalBase).ToString(format);
            addition = addition.Replace(',', decimalPoint);
            Cache[decimals][truncatedInteger] = addition;
            return addition;
        }
    }
}
