using System.ComponentModel;

/// <summary>
/// 3D axis
/// </summary>
public enum Axis3D { X, Y, Z};

/// <summary>
/// 2D axis
/// </summary>
public enum Axis2D { X, Y};

/// <summary>
/// 3D pairs of axis
/// </summary>
public enum AxisPair { XY, XZ, YX, YZ, ZX, ZY};

/// <summary>
/// Clusing type.
/// </summary>
public enum	ClusingType
{
	[Description ("Inclusive mini and maxi")]
	II,
	[Description ("Inclusive mini and Exclusive maxi")]
	IE,
	[Description ("Exclusive mini and Inclusive maxi")]
	EI,
	[Description ("Exclusive mini and maxi")]
	EE
}

public enum LayersEnum : int
{
	Default = 0,
	TransparentFX = 1,
	IgnoreRaycast = 2,
	Water = 4,
	UI = 5
}
