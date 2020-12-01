public static class IntExtensions
{
    /// <summary>
    /// Returns true if the value is between the two boundaries
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lower"></param>
    /// <param name="greater"></param>
    /// <returns></returns>
    public static bool isBetween(this int value, int lower, int greater, ClusingType clusing)
    {
        float temp = (float)value;
        return temp.isBetween(lower, greater, clusing);
    }
}