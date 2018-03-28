using System;
using UnityEngine;

namespace Tantawowa.Extensions
{
    public static class PrimitiveExtensions
    {
        public static int ClampIndex(this int value, int min, int max)
        {
            if (value > max)
            {
                return 0;
            }

            if (value < min)
            {
                return max;
            }

            return value;
        }

        public static bool IsValidAsType(this string input, Type type)
        {
            var isConverted = false;
            if (type == typeof(string))
            {
                isConverted = true;
            }
            else if (type == typeof(float))
            {
                float f;
                isConverted = float.TryParse(input, out f);
            }
            else if (type == typeof(int))
            {
                int i;
                isConverted = int.TryParse(input, out i);
            }
            else if (type == typeof(bool))
            {
                bool b;
                isConverted = bool.TryParse(input, out b);
            }

            return isConverted;
        }

        public static T ConvertToType<T>(this string input)
        {
            var isConverted = false;
            var type = typeof(T);
            if (type == typeof(string))
            {
                return (T) (object) input;
            }
            else if (type == typeof(float))
            {
                float f;
                isConverted = float.TryParse(input, out f);
                if (isConverted)
                {
                    return (T) (object) f;
                }
            }
            else if (type == typeof(int))
            {
                int i;
                isConverted = int.TryParse(input, out i);
                if (isConverted)
                {
                    return (T) (object) i;
                }
            }
            else if (type == typeof(bool))
            {
                bool b;
                isConverted = bool.TryParse(input, out b);
                if (isConverted)
                {
                    return (T) (object) b;
                }
            }

            return default(T);
        }

        public static Double RoundUpToNearest(this Double passednumber, Double roundto)
        {
            return roundto == 0 ? passednumber : Math.Ceiling(passednumber / roundto) * roundto;
        }

        public static Double RoundDownToNearest(this Double passednumber, Double roundto)
        {
            return roundto == 0 ? passednumber : Math.Floor(passednumber / roundto) * roundto;
        }

        public static float RoundUpToNearest(this float passednumber, float roundto)
        {
            return roundto == 0 ? passednumber : Mathf.Ceil(passednumber / roundto) * roundto;
        }

        public static float RoundDownToNearest(this float passednumber, float roundto)
        {
            return roundto == 0 ? passednumber : Mathf.Floor(passednumber / roundto) * roundto;
        }

        public static int RoundUpToNearest(this int passednumber, int roundto)
        {
            return (int) (roundto == 0 ? passednumber : Mathf.Ceil(passednumber / roundto) * roundto);
        }

        public static int RoundDownToNearest(this int passednumber, int roundto)
        {
            return (int) (roundto == 0 ? passednumber : Mathf.Floor(passednumber / roundto) * roundto);
        }


        public static int Flip(this int value)
        {
            return value == 0 ? 1 : 0;
        }
    }
}