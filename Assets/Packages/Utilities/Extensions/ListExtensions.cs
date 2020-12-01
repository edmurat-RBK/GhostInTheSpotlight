using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public static class ListExtensions
{
	/// <summary>
    /// Changes the index of an element of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="oldIndex"></param>
    /// <param name="newIndex"></param>
	public static void Move<T> (this List<T> list, int oldIndex, int newIndex)
	{
        if(newIndex.isBetween(0, list.Count, ClusingType.IE))
        {
            T aux = list[newIndex];
            list[newIndex] = list[oldIndex];
            list[oldIndex] = aux;
        }
	}

    /// <summary>
    /// Deletes all empty index in the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
	public static void RemoveNull<T> (this List<T> list)
	{
		list.RemoveAll (delegate (T o)
		{
			return o == null;
		}
		);
	}

    /// <summary>
    /// Returns the list without its empty elements
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
	public static List<T> GetNonNull<T> (this List<T> list)
	{
		return list.Where (value => value != null).ToList ();
	}

	/// <summary>
	/// Shuffle the list in place using the Fisher-Yates method.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	public static void ShuffleThis<T> (this IList<T> list)
	{  
		System.Random rng = new System.Random ();  
		int n = list.Count;  
		while (n > 1)
		{  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list [k];  
			list [k] = list [n];  
			list [n] = value;  
		}  
	}

	/// <summary>
	/// Return a random item from the list.
	/// Sampling with replacement.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static T RandomItem<T> (this IList<T> list)
	{
		if (list.Count == 0)
			throw new System.IndexOutOfRangeException ("Cannot select a random item from an empty list");
		return list [UnityEngine.Random.Range (0, list.Count)];
	}

	/// <summary>
	/// Removes a random item from the list, returning that item.
	/// Sampling without replacement.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static T RemoveRandom<T> (this IList<T> list)
	{
		if (list.Count == 0)
			throw new System.IndexOutOfRangeException ("Cannot remove a random item from an empty list");
		int index = UnityEngine.Random.Range (0, list.Count);
		T item = list [index];
		list.RemoveAt (index);
		return item;
	}
}

