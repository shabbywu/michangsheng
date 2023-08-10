using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class RayImpact
{
	[Range(0f, 1000f)]
	[SerializeField]
	[Tooltip("The damage at close range.")]
	private float m_MaxDamage = 15f;

	[Range(0f, 1000f)]
	[SerializeField]
	[Tooltip("The impact impulse that will be transfered to the rigidbodies at contact.")]
	private float m_MaxImpulse = 15f;

	[SerializeField]
	[Tooltip("How damage and impulse lowers over distance.")]
	private AnimationCurve m_DistanceCurve = new AnimationCurve((Keyframe[])(object)new Keyframe[3]
	{
		new Keyframe(0f, 1f),
		new Keyframe(0.8f, 0.5f),
		new Keyframe(1f, 0f)
	});

	public float GetDamageAtDistance(float distance, float maxDistance)
	{
		return ApplyCurveToValue(m_MaxDamage, distance, maxDistance);
	}

	public float GetImpulseAtDistance(float distance, float maxDistance)
	{
		return ApplyCurveToValue(m_MaxImpulse, distance, maxDistance);
	}

	private float ApplyCurveToValue(float value, float distance, float maxDistance)
	{
		float num = Mathf.Abs(maxDistance);
		float num2 = Mathf.Clamp(distance, 0f, num);
		return value * m_DistanceCurve.Evaluate(num2 / num);
	}
}
