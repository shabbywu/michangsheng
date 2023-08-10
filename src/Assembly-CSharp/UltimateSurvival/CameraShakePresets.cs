using UnityEngine;

namespace UltimateSurvival;

public static class CameraShakePresets
{
	public static ShakeInstance Bump => new ShakeInstance(2.5f, 4f, 0.1f, 0.75f)
	{
		PositionInfluence = Vector3.one * 0.15f,
		RotationInfluence = Vector3.one
	};

	public static ShakeInstance Explosion => new ShakeInstance(5f, 10f, 0f, 1.5f)
	{
		PositionInfluence = Vector3.one * 0.25f,
		RotationInfluence = new Vector3(4f, 1f, 1f)
	};

	public static ShakeInstance Earthquake => new ShakeInstance(0.6f, 3.5f, 2f, 10f)
	{
		PositionInfluence = Vector3.one * 0.25f,
		RotationInfluence = new Vector3(1f, 1f, 4f)
	};

	public static ShakeInstance BadTrip => new ShakeInstance(10f, 0.15f, 5f, 10f)
	{
		PositionInfluence = new Vector3(0f, 0f, 0.15f),
		RotationInfluence = new Vector3(2f, 1f, 4f)
	};

	public static ShakeInstance HandheldCamera => new ShakeInstance(1f, 0.25f, 5f, 10f)
	{
		PositionInfluence = Vector3.zero,
		RotationInfluence = new Vector3(1f, 0.5f, 0.5f)
	};

	public static ShakeInstance Vibration => new ShakeInstance(0.4f, 20f, 2f, 2f)
	{
		PositionInfluence = new Vector3(0f, 0.15f, 0f),
		RotationInfluence = new Vector3(1.25f, 0f, 4f)
	};

	public static ShakeInstance RoughDriving => new ShakeInstance(1f, 2f, 1f, 1f)
	{
		PositionInfluence = Vector3.zero,
		RotationInfluence = Vector3.one
	};
}
