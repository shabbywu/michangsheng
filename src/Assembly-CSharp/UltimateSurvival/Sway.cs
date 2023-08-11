using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class Sway
{
	[SerializeField]
	private bool Enabled;

	[SerializeField]
	private Vector2 Magnitude;

	[SerializeField]
	private float LerpSpeed;

	public Vector2 Value { get; private set; }

	public Sway GetClone()
	{
		return (Sway)MemberwiseClone();
	}

	public void CalculateSway(Vector2 input, float deltaTime)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (Enabled)
		{
			Vector2 magnitude = Magnitude;
			((Vector2)(ref magnitude)).Scale(input);
			Value = Vector2.Lerp(Value, magnitude, deltaTime * LerpSpeed);
		}
	}
}
