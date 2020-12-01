using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Helper
{
	/// <summary>
	/// Gets the center of two points
	/// </summary>
	/// <param name="vec1"></param>
	/// <param name="vec2"></param>
	/// <returns></returns>
	public static Vector3 Center (Vector3 vec1, Vector3 vec2)
	{
		return new Vector3 ((vec1.x + vec2.x) / 2, (vec1.y + vec2.y) / 2, (vec1.z + vec2.z) / 2);
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="dir1"></param>
	/// <param name="dir2"></param>
	/// <param name="axis"></param>
	/// <returns></returns>
	public static float AngleAroundAxis (Vector3 dir1, Vector3 dir2, Vector3 axis)
	{
		dir1 = dir1 - Vector3.Project (dir1, axis);
		dir2 = dir2 - Vector3.Project (dir2, axis);

		float angle = Vector3.Angle (dir1, dir2);
		return angle * (Vector3.Dot (axis, Vector3.Cross (dir1, dir2)) < 0 ? -1 : 1);
	}

	/// <summary>
	/// Returns a random direction in a cone. a spread of 0 is straight, 0.5 is 180*
	/// </summary>
	/// <param name="spread"></param>
	/// <param name="forward">must be unit</param>
	/// <returns></returns>
	public static Vector3 RandomDirection (float spread, Vector3 forward)
	{
		return Vector3.Slerp (forward, Random.onUnitSphere, spread);
	}

	/// <summary>
	/// test if a Vector3 is close to another Vector3 (due to floating point inprecision)
	/// compares the square of the distance to the square of the range as this
	/// avoids calculating a square root which is much slower than squaring the range
	/// </summary>
	/// <param name="val"></param>
	/// <param name="about"></param>
	/// <param name="range"></param>
	/// <returns></returns>
	public static bool Approx (Vector3 val, Vector3 about, float range)
	{
		return ((val - about).sqrMagnitude < range * range);
	}

	/// <summary>
	/// Find a point on the infinite line nearest to point
	/// </summary>
	/// <param name="lineStart"></param>
	/// <param name="lineEnd"></param>
	/// <param name="point"></param>
	/// <returns></returns>
	public static Vector3 NearestPoint (Vector3 lineStart, Vector3 lineEnd, Vector3 point)
	{
		Vector3 lineDirection = Vector3.Normalize (lineEnd - lineStart);
		float closestPoint = Vector3.Dot ((point - lineStart), lineDirection) / Vector3.Dot (lineDirection, lineDirection);
		return lineStart + (closestPoint * lineDirection);
	}

	/// <summary>
	/// find a point on the line segment nearest to point
	/// </summary>
	/// <param name="lineStart"></param>
	/// <param name="lineEnd"></param>
	/// <param name="point"></param>
	/// <returns></returns>
	public static Vector3 NearestPointStrict (Vector3 lineStart, Vector3 lineEnd, Vector3 point)
	{
		Vector3 fullDirection = lineEnd - lineStart;
		Vector3 lineDirection = Vector3.Normalize (fullDirection);
		float closestPoint = Vector3.Dot ((point - lineStart), lineDirection) / Vector3.Dot (lineDirection, lineDirection);
		return lineStart + (Mathf.Clamp (closestPoint, 0.0f, Vector3.Magnitude (fullDirection)) * lineDirection);
	}

	/// <summary>
	/// Calculates the intersection line segment between 2 lines (not segments).
	/// Returns false if no solution can be found.
	/// </summary>
	/// <returns></returns>
	public static bool CalculateLineLineIntersection (Vector3 line1Point1, Vector3 line1Point2,
	                                                 Vector3 line2Point1, Vector3 line2Point2, out Vector3 resultSegmentPoint1, out Vector3 resultSegmentPoint2)
	{
		// Algorithm is ported from the C algorithm of 
		// Paul Bourke at http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline3d/
		resultSegmentPoint1 = new Vector3 (0, 0, 0);
		resultSegmentPoint2 = new Vector3 (0, 0, 0);

		var p1 = line1Point1;
		var p2 = line1Point2;
		var p3 = line2Point1;
		var p4 = line2Point2;
		var p13 = p1 - p3;
		var p43 = p4 - p3;

		if (p4.sqrMagnitude < float.Epsilon)
		{
			return false;
		}
		var p21 = p2 - p1;
		if (p21.sqrMagnitude < float.Epsilon)
		{
			return false;
		}

		var d1343 = p13.x * p43.x + p13.y * p43.y + p13.z * p43.z;
		var d4321 = p43.x * p21.x + p43.y * p21.y + p43.z * p21.z;
		var d1321 = p13.x * p21.x + p13.y * p21.y + p13.z * p21.z;
		var d4343 = p43.x * p43.x + p43.y * p43.y + p43.z * p43.z;
		var d2121 = p21.x * p21.x + p21.y * p21.y + p21.z * p21.z;

		var denom = d2121 * d4343 - d4321 * d4321;
		if (Mathf.Abs (denom) < float.Epsilon)
		{
			return false;
		}
		var numer = d1343 * d4321 - d1321 * d4343;

		var mua = numer / denom;
		var mub = (d1343 + d4321 * (mua)) / d4343;

		resultSegmentPoint1.x = p1.x + mua * p21.x;
		resultSegmentPoint1.y = p1.y + mua * p21.y;
		resultSegmentPoint1.z = p1.z + mua * p21.z;
		resultSegmentPoint2.x = p3.x + mub * p43.x;
		resultSegmentPoint2.y = p3.y + mub * p43.y;
		resultSegmentPoint2.z = p3.z + mub * p43.z;

		return true;
	}

	/// <summary>
	/// Direct speedup of <seealso cref="Vector3.Lerp"/>
	/// </summary>
	/// <param name="v1"></param>
	/// <param name="v2"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	/*public static Vector3 Lerp(Vector3 v1, Vector3 v2, float value)
	{
		if (value > 1.0f)
			return v2;
		if (value < 0.0f)
			return v1;
		return new Vector3(v1.x + (v2.x - v1.x) * value,
			v1.y + (v2.y - v1.y) * value,
			v1.z + (v2.z - v1.z) * value);
	}*/

	public static Vector3 Sinerp (Vector3 from, Vector3 to, float value)
	{
		value = Mathf.Sin (value * Mathf.PI * 0.5f);
		return Vector3.Lerp (from, to, value);
	}
}
