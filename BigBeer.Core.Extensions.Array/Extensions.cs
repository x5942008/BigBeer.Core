
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
namespace BigBeer.Core.Extensions
{
    public static partial class Extensions
    {

        /// <summary>
        /// 确定指定数组包含的元素是否与指定谓词定义的条件匹配。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Boolean Exists<T>(this T[] array, Predicate<T> match)
        {
            return Array.Exists(array, match);
        }
        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 System.Array 中的第一个匹配元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T Find<T>(this T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }
        /// <summary>
        /// 检索与指定谓词定义的条件匹配的所有元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindAll(array, match);
        }
        /// <summary>
        ///     A T[] extension method that searches for the first index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        /// <summary>
        ///     搜索与指定谓词所定义的条件相匹配的元素，并返回整个 System.Array 中第一个匹配元素的从零开始的索引。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Int32 FindIndex<T>(this T[] array, Int32 startIndex, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, match);
        }

        /// <summary>
        ///    搜索与指定谓词所定义的条件相匹配的元素，并返回 System.Array 中从指定索引到最后一个元素的元素范围内第一个匹配项的从零开始的索引。
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">Number of.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindIndex<T>(this T[] array, Int32 startIndex, Int32 count, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, count, match);
        }
        /// <summary>
        ///    搜索与指定谓词所定义的条件相匹配的一个元素，并返回 System.Array 中从指定的索引开始、包含指定元素个数的元素范围内第一个匹配项的从零开始的索引。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">.</param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T FindLast<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }
        /// <summary>
        ///     搜索与指定谓词所定义的条件相匹配的元素，并返回整个 System.Array 中的最后一个匹配元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="match"></param>
        /// <returns>.</returns>
        public static Int32 FindLastIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLastIndex(array, match);
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 System.Array 中最后一个匹配元素的从零开始的索引。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Int32 FindLastIndex<T>(this T[] array, Int32 startIndex, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, match);
        }

        /// <summary>
        /// 
        /// 搜索与由指定谓词定义的条件相匹配的元素，并返回 System.Array 中从第一个元素到指定索引的元素范围内最后一个匹配项的从零开始的索引。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Int32 FindLastIndex<T>(this T[] array, Int32 startIndex, Int32 count, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, count, match);
        }
        /// <summary>
        /// 对指定数组的每个元素执行指定操作。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            foreach (var a in array)
            {
                action(a);
            }
        }
        /// <summary>
        /// 确定数组中的每个元素是否都与指定谓词定义的条件匹配。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Boolean TrueForAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.TrueForAll(array, match);
        }
        /// <summary>
        /// 将数组中的某个范围的元素设置为每个元素类型的默认值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        public static void ClearAll<T>(this T[] @this)
        {
            Array.Clear(@this, 0, @this.Length);
        }
        /// <summary>
        /// 将数组中的某个位置的元素设置为元素类型的默认值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="at"></param>
        public static void ClearAt<T>(this T[] @this, int at)
        {
            Array.Clear(@this, at, 1);
        }
        /// <summary>
        /// 将数组中的某个范围的元素设置为每个元素类型的默认值。
        /// </summary>
        /// <param name="this"></param>
        public static void ClearAll(this Array @this)
        {
            Array.Clear(@this, 0, @this.Length);
        }
        /// <summary>
        /// 是否包含某索引
        /// </summary>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool WithinIndex(this Array @this, int index)
        {
            return index >= 0 && index < @this.Length;
        }
        /// <summary>
        /// 在每个纬度总搜索值
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 BinarySearch(this Array array, Object value)
        {
            return Array.BinarySearch(array, value);
        }
        /// <summary>
        /// 在纬度范围中总搜索值
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 BinarySearch(this Array array, Int32 index, Int32 length, Object value)
        {
            return Array.BinarySearch(array, index, length, value);
        }
        /// <summary>
        /// 使用指定 System.Collections.IComparer 接口，在整个一维排序数组中搜索值。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static Int32 BinarySearch(this Array array, Object value, IComparer comparer)
        {
            return Array.BinarySearch(array, value, comparer);
        }
        /// <summary>
        /// 使用指定 System.Collections.IComparer 接口，在一维排序数组的某个元素范围中搜索值。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static Int32 BinarySearch(this Array array, Int32 index, Int32 length, Object value, IComparer comparer)
        {
            return Array.BinarySearch(array, index, length, value, comparer);
        }
        /// <summary>
        /// 将数字一定返回的对象设置成对象默认值
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public static void Clear(this Array array, Int32 index, Int32 length)
        {
            Array.Clear(array, index, length);
        }
        /// <summary>
        /// 保证成功的复制
        /// </summary>
        /// <param name="sourceArray"></param>
        /// <param name="sourceIndex"></param>
        /// <param name="destinationArray"></param>
        /// <param name="destinationIndex"></param>
        /// <param name="length"></param>
        public static void ConstrainedCopy(this Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length)
        {
            Array.ConstrainedCopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }
        /// <summary>
        /// 复制数组一定范围的对象到目标数组
        /// </summary>
        /// <param name="sourceArray"></param>
        /// <param name="destinationArray"></param>
        /// <param name="length"></param>
        public static void Copy(this Array sourceArray, Array destinationArray, Int32 length)
        {
            Array.Copy(sourceArray, destinationArray, length);
        }
        public static void Copy(this Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }

        /// <summary>
        /// 在一个一维数组中搜索指定对象，并返回其首个匹配项的索引。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 IndexOf(this Array array, Object value)
        {
            return Array.IndexOf(array, value);
        }
        /// <summary>
        /// 在一个一维数组中搜索指定对象，并返回其首个匹配项的索引。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static Int32 IndexOf(this Array array, Object value, Int32 startIndex)
        {
            return Array.IndexOf(array, value, startIndex);
        }
        /// <summary>
        /// 在一个一维数组中搜索指定对象，并返回其首个匹配项的索引。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Int32 IndexOf(this Array array, Object value, Int32 startIndex, Int32 count)
        {
            return Array.IndexOf(array, value, startIndex, count);
        }
        /// <summary>
        /// 搜索指定的对象，并返回整个一维 System.Array 中最后一个匹配项的索引。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 LastIndexOf(this Array array, Object value)
        {
            return Array.LastIndexOf(array, value);
        }
        /// <summary>
        /// 搜索指定的对象，并返回整个一维 System.Array 中最后一个匹配项的索引。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static Int32 LastIndexOf(this Array array, Object value, Int32 startIndex)
        {
            return Array.LastIndexOf(array, value, startIndex);
        }
        /// <summary>
        /// 搜索指定的对象，并返回整个一维 System.Array 中最后一个匹配项的索引。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Int32 LastIndexOf(this Array array, Object value, Int32 startIndex, Int32 count)
        {
            return Array.LastIndexOf(array, value, startIndex, count);
        }
        /// <summary>
        /// 反转整个一维 System.Array 中元素的顺序。
        /// </summary>
        /// <param name="array"></param>
        public static void Reverse(this Array array)
        {
            Array.Reverse(array);
        }
        /// <summary>
        /// 反转一维 System.Array 中某部分元素的元素顺序
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public static void Reverse(this Array array, Int32 index, Int32 length)
        {
            Array.Reverse(array, index, length);
        }
        /// <summary>
        ///  使用 System.Array 中每个元素的 System.IComparable 实现，对整个一维 System.Array 中的元素进行排序。
        /// </summary>
        /// <param name="array"></param>
        public static void Sort(this Array array)
        {
            Array.Sort(array);
        }
        /// <summary>
        /// 基于第一个 System.Array 中的关键字，使用每个关键字的 System.IComparable 实现，对两个一维 System.Array 对象（一个包含关键字，另一个包含对应的项）进行排序。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="items"></param>
        public static void Sort(this Array array, Array items)
        {
            Array.Sort(array, items);
        }
        /// <summary>
        /// 使用 System.Array 中每个元素的 System.IComparable 实现，对一维 System.Array 中某部分元素进行排序。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public static void Sort(this Array array, Int32 index, Int32 length)
        {
            Array.Sort(array, index, length);
        }
        public static void Sort(this Array array, Array items, Int32 index, Int32 length)
        {
            Array.Sort(array, items, index, length);
        }
        public static void Sort(this Array array, IComparer comparer)
        {
            Array.Sort(array, comparer);
        }
        public static void Sort(this Array array, Array items, IComparer comparer)
        {
            Array.Sort(array, items, comparer);
        }
        public static void Sort(this Array array, Int32 index, Int32 length, IComparer comparer)
        {
            Array.Sort(array, index, length, comparer);
        }
        public static void Sort(this Array array, Array items, Int32 index, Int32 length, IComparer comparer)
        {
            Array.Sort(array, items, index, length, comparer);
        }
        /// <summary>
        /// 将指定数目的字节从起始于特定偏移量的源数组复制到起始于特定偏移量的目标数组。
        /// </summary>
        /// <param name="src"></param>
        /// <param name="srcOffset"></param>
        /// <param name="dst"></param>
        /// <param name="dstOffset"></param>
        /// <param name="count"></param>
        public static void BlockCopy(this Array src, Int32 srcOffset, Array dst, Int32 dstOffset, Int32 count)
        {
            Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
        }
        /// <summary>
        /// 返回指定数组中的字节数。
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Int32 ByteLength(this Array array)
        {
            return Buffer.ByteLength(array);
        }
        /// <summary>
        /// 检索指定数组中指定位置的字节。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Byte GetByte(this Array array, Int32 index)
        {
            return Buffer.GetByte(array, index);
        }
        /// <summary>
        /// 将指定的值分配给指定数组中特定位置处的字节。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public static void SetByte(this Array array, Int32 index, Byte value)
        {
            Buffer.SetByte(array, index, value);
        }
    }
}

