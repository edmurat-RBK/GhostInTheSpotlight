using UnityEngine;
using System;
using System.Collections.Generic;

public static class ArrayExtensions
{
	/// <summary>
    /// Returns a random element of the array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
	public static T RandomItem<T> (this T[] array)
	{
		return array [UnityEngine.Random.Range (0, array.Length)];
	}

	/// <summary>
	/// Return the next index of the array, if index is out of bounds, return 0.
	/// </summary>
	/// <returns>The index.</returns>
	/// <param name="array">Array.</param>
	/// <param name="actualIndex">Actual index.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static int NextIndex<T> (this T[] array, int actualIndex)
	{
		actualIndex++;
		return actualIndex < array.Length ? actualIndex : 0;
	}
	
	/// <summary>
    /// Returns all elements of an array in a new list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
	public static List<T> ToList<T> (this T[] array)
	{
		return new List<T> (array);
	}

    /// <summary>
    /// Deletes an amount of elements in the array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
	public static T[] RemoveRange<T> (this T[] array, int index, int count)
	{
		if (count < 0)
			throw new ArgumentOutOfRangeException ("count", " is out of range");
		if (index < 0 || index > array.Length - 1)
			throw new ArgumentOutOfRangeException ("index", " is out of range");

		if (array.Length - count - index < 0)
			throw new ArgumentException ("index and count do not denote a valid range of elements in the array", "");

		var newArray = new T[array.Length - count];

		for (int i = 0, ni = 0; i < array.Length; i++)
		{
			if (i < index || i >= index + count)
			{
				newArray [ni] = array [i];
				ni++;
			}
		}

		return newArray;
	}
}