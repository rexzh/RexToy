using System;
using System.Collections.Generic;
using System.Text;

namespace RexToy
{
    public static class ArrayExtension
    {
        public static string Join(this Array array, string delimeter = "")
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
                b.Append(array.GetValue(i).ToString()).Append(delimeter);
            b.RemoveEnd(delimeter);
            return b.ToString();
        }

        public static string Join<T>(this T[] array, Func<T, string> func, string delimeter = "")
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
                b.Append(func(array[i])).Append(delimeter);
            b.RemoveEnd(delimeter);
            return b.ToString();
        }

        public static string Join(this Array array, char delimeter)
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
                b.Append(array.GetValue(i).ToString()).Append(delimeter);
            b.RemoveEnd(delimeter);
            return b.ToString();
        }

        public static string Join<T>(this T[] array, Func<T, string> func, char delimeter)
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
                b.Append(func(array[i])).Append(delimeter);
            b.RemoveEnd(delimeter);
            return b.ToString();
        }

        public static T[] Slice<T>(this T[] arr, int start, int end)
        {
            if (start > end)
                throw new ArgumentException("[start] must less than [end].");
            T[] frag = new T[end - start];
            for (int i = start; i < end; i++)
            {
                frag[i - start] = (T)arr.GetValue(i);
            }
            return frag;
        }

        /// <summary>
        /// Return a new array which contains all elements exclude the one at specified index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] Reduce<T>(this T[] arr, int index)
        {
            if (index >= arr.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Too large.");

            T[] result = new T[arr.Length - 1];
            arr.Slice<T>(0, index).CopyTo(result, 0);
            arr.Slice<T>(index + 1, arr.Length).CopyTo(result, index);
            return result;
        }

        /// <summary>
        /// Return a new array which contains all elements exclude those at specified index and range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] Reduce<T>(this T[] arr, int index, int length)
        {
            T[] result = new T[arr.Length - length];
            arr.Slice<T>(0, index).CopyTo(result, 0);
            arr.Slice<T>(index + length, arr.Length).CopyTo(result, index);
            return result;
        }

        /// <summary>
        /// Return a new array which contains all elements in current array, and add the additional elements of other array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static T[] Combine<T>(this T[] arr, T[] other)
        {
            T[] result = new T[arr.Length + other.Length];
            arr.CopyTo(result, 0);
            other.CopyTo(result, arr.Length);
            return result;
        }

        /// <summary>
        /// Return a new array which contains all elements in current array, and add the additional element at begin.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static T[] Combine<T>(this T element, T[] other)
        {
            T[] result = new T[other.Length + 1];
            result[0] = element;
            other.CopyTo(result, 1);
            return result;
        }

        /// <summary>
        /// Return a new array which contains all elements in current array, and add the additional element at end.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T[] Combine<T>(this T[] arr, T element)
        {
            T[] result = new T[arr.Length + 1];
            arr.CopyTo(result, 0);
            result[arr.Length] = element;
            return result;
        }

        /// <summary>
        /// Return a new sorted array, contains same element as current array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[] Sort<T>(this T[] arr)
        {
            T[] result = new T[arr.Length];
            arr.CopyTo(result, 0);
            Array.Sort<T>(result);
            return result;
        }
    }
}
