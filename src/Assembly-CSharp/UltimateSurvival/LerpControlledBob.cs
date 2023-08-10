using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class LerpControlledBob
{
	[SerializeField]
	private float m_MaxBobDuration;

	[SerializeField]
	private float m_MaxBobAmount;

	[SerializeField]
	private AnimationCurve m_DurationCurve;

	[SerializeField]
	private AnimationCurve m_DisplacementCurve;

	public float Value { get; private set; }

	public IEnumerator DoBobCycle(float displacement)
	{
		float t2 = 0f;
		float bobDuration = m_MaxBobDuration * m_DurationCurve.Evaluate(displacement);
		float bobAmount = m_MaxBobAmount * m_DisplacementCurve.Evaluate(displacement);
		while (t2 < bobDuration)
		{
			Value = Mathf.Lerp(0f, bobAmount, t2 / bobDuration);
			t2 += Time.deltaTime;
			yield return (object)new WaitForFixedUpdate();
		}
		t2 = 0f;
		while (t2 < bobDuration)
		{
			Value = Mathf.Lerp(bobAmount, 0f, t2 / bobDuration);
			t2 += Time.deltaTime;
			yield return (object)new WaitForFixedUpdate();
		}
		Value = 0f;
	}
}
