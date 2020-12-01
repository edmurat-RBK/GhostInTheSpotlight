using UnityEngine;
using System.Collections;

public static class FloatExtensions
{
    /// <summary>
    /// Returns true if the value is between the two boundaries
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lower"></param>
    /// <param name="greater"></param>
    /// <returns></returns>
    public static bool isBetween(this float value, float lower, float greater, ClusingType clusing)
    {
        switch(clusing)
        {
            case ClusingType.EE:
                if (value > lower && value < greater)
                {
                    return true;
                }
                return false;

            case ClusingType.II:
                if (value >= lower && value <= greater)
                {
                    return true;
                }
                return false;

            case ClusingType.EI:
                if (value > lower && value <= greater)
                {
                    return true;
                }
                return false;

            case ClusingType.IE:
                if (value >= lower && value < greater)
                {
                    return true;
                }
                return false;
            default:
                Debug.LogError("Error, enum never read !");
                return false;
        }
    }

    /// <summary>
    /// Remaps current value in a new interval.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="from1"></param>
    /// <param name="to1"></param>
    /// <param name="from2"></param>
    /// <param name="to2"></param>
    /// <returns></returns>
	public static float Remap (this float value, float from1, float to1, float from2, float to2)
	{
		return Mathf.Clamp ((value - from1) / (to1 - from1) * (to2 - from2) + from2, Mathf.Min (from2, to2), Mathf.Max (from2, to2));
	}

    /// <summary>
    /// Remaps current value as percent.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="from1"></param>
    /// <param name="to1"></param>
    /// <returns></returns>
	public static float RemapPercent (this float value, float from1, float to1)
	{
		return  Mathf.Clamp ((value - from1) / (to1 - from1) * 100, 0, 100);
	}

    /// <summary>
    /// Avoids rotation to exceed 360 degrees.
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
	public static float RotationNormalizedDeg (this float rotation)
	{
		rotation = rotation % 360f;
		if (rotation < 0)
			rotation += 360f;
		return rotation;
	}

    /// <summary>
    /// Permet de normaliser une rotation en radiants et éviter de dépasser PI
    /// Avoids rotation in radians to exceed PI.
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
	public static float RotationNormalizedRad (this float rotation)
	{
		rotation = rotation % Mathf.PI;
		if (rotation < 0)
			rotation += Mathf.PI;
		return rotation;
	}
}
