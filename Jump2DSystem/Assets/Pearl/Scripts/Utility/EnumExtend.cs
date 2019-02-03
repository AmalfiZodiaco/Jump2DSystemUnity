using System;
using UnityEngine;

namespace Pearl
{
    public static class EnumExtend
    {
        private static int auxInteger;

        public static T GetRandom<T>() where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum);

            auxInteger = UnityEngine.Random.Range(0, Length<T>());
            return (T)Enum.ToObject(typeof(T), auxInteger);
        }

        public static T ParseEnum<T>(string value) where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum);

            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentException)
            {
                return default;
            }
        }

        public static int Length<T>() where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum);

            return Enum.GetValues(typeof(T)).Length;
        }

        public static T Next<T>(T value) where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum);

            auxInteger = Convert.ToInt32(value);
            auxInteger = MathfExtend.ChangeOneInCircle(auxInteger, Length<T>(), true);
            return (T)Enum.ToObject(typeof(T), auxInteger);
        }

        public static T GetInverse<T>(T value) where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum && Enum.GetNames(typeof(T)).Length == 2);

            auxInteger = Convert.ToInt32(value);
            auxInteger = MathfExtend.ChangeOneInCircle(auxInteger, 2, true);
            return (T)Enum.ToObject(typeof(T), auxInteger);
        }
    }

}