using UnityEngine;
using System.Collections;

/// <summary>
/// Les noms des fonctions sont explicites et j'ai la flemme de les commenter
/// </summary>
public static class Vector2Extensions
{
	public static Vector2 SetX (this Vector2 vector, float x)
	{
		return new Vector2 (x, vector.y);
	}

	public static Vector2 SetY (this Vector2 vector, float y)
	{
		return new Vector2 (vector.x, y);
	}

	public static Vector2 AddX (this Vector2 vector, float x)
	{
		return new Vector2 (vector.x + x, vector.y);
	}

	public static Vector2 AddY (this Vector2 vector, float y)
	{
		return new Vector2 (vector.x, vector.y + y);
	}

	public static Vector2 Rotate (this Vector2 vector, float degrees)
	{
		float sin = Mathf.Sin (degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos (degrees * Mathf.Deg2Rad);
		
		float tx = vector.x;
		float ty = vector.y;
		vector.x = (cos * tx) - (sin * ty);
		vector.y = (sin * tx) + (cos * ty);
		return vector;
	}

	public static Vector3 ToVector3 (this Vector2 vector, float z = 0.0f)
	{
		return new Vector3 (vector.x, vector.y, z);
	}
}
