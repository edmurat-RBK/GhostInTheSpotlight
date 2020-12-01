using UnityEngine;

public static class QuaternionExtensions
{
    /// <summary>
    /// Applies a power to the quaternion.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="power"></param>
    /// <returns></returns>
    public static Quaternion Pow(this Quaternion input, float power)
    {
        float inputMagnitude = input.Magnitude();
        Vector3 nHat = new Vector3(input.x, input.y, input.z).normalized;
        Quaternion vectorBit = new Quaternion(nHat.x, nHat.y, nHat.z, 0)
    .ScalarMultiply(power * Mathf.Acos(input.w / inputMagnitude))
        .Exp();
        return vectorBit.ScalarMultiply(Mathf.Pow(inputMagnitude, power));
    }

    /// <summary>
    /// Applies an exponential to the quaternion.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Quaternion Exp(this Quaternion input)
    {
        float inputA = input.w;
        Vector3 inputV = new Vector3(input.x, input.y, input.z);
        float outputA = Mathf.Exp(inputA) * Mathf.Cos(inputV.magnitude);
        Vector3 outputV = Mathf.Exp(inputA) * (inputV.normalized * Mathf.Sin(inputV.magnitude));
        return new Quaternion(outputV.x, outputV.y, outputV.z, outputA);
    }

    /// <summary>
    /// Return the magnitude of the quaternion.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static float Magnitude(this Quaternion input)
    {
        return Mathf.Sqrt(input.x * input.x + input.y * input.y + input.z * input.z + input.w * input.w);
    }

    /// <summary>
    /// Return the scalar multiplication of the quaternion.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="scalar"></param>
    /// <returns></returns>
    public static Quaternion ScalarMultiply(this Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }
}