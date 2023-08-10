using UnityEngine;

namespace UltimateSurvival;

public class ClampAttribute : PropertyAttribute
{
	public readonly Vector2 ClampLimits;

	public ClampAttribute(float min, float max)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		ClampLimits = new Vector2(min, max);
	}
}
