using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class TrigonometricBob
{
	private const float HORIZONTAL_SPEED = 1f;

	private const float VERTICAL_SPEED = 2f;

	[SerializeField]
	private bool m_Enabled = true;

	[SerializeField]
	[Range(0.1f, 20f)]
	[Tooltip("How fast is the animation overall.")]
	private float m_Speed = 0.18f;

	[SerializeField]
	[Range(0.1f, 20f)]
	[Tooltip("How fast it blends out, when it's no longer used (so the transition between walk and run bobs are smooth for example).")]
	private float m_CooldownSpeed = 5f;

	[SerializeField]
	[Range(0f, 100f)]
	private float m_AmountX = 0.2f;

	[SerializeField]
	[Range(0f, 100f)]
	private float m_AmountY = 0.2f;

	[SerializeField]
	[Tooltip("You can control how fast the animation plays at different time intervals (time = 0 is the beginning, time = 1 is the end).")]
	private AnimationCurve m_Curve = new AnimationCurve((Keyframe[])(object)new Keyframe[2]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	private float m_Time;

	private Vector3 m_Vector;

	public float Time => m_Time;

	public TrigonometricBob GetClone()
	{
		return (TrigonometricBob)MemberwiseClone();
	}

	public Vector3 CalculateBob(float moveSpeed, float deltaTime)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!m_Enabled)
		{
			return Vector3.zero;
		}
		float num = Mathf.Sin(1f * m_Time * (float)Math.PI * 2f);
		float num2 = 1f - Mathf.Cos(2f * m_Time * (float)Math.PI * 2f);
		float num3 = m_Time + m_Speed * moveSpeed * deltaTime * m_Curve.Evaluate(m_Time);
		m_Time = Mathf.Repeat(num3, 1f);
		m_Vector.x = num * m_AmountX;
		m_Vector.y = num2 * m_AmountY;
		return m_Vector;
	}

	public Vector3 Cooldown(float deltaTime)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!m_Enabled)
		{
			return Vector3.zero;
		}
		m_Time = Mathf.Lerp(m_Time, GetBestCooldownValue(m_Time), deltaTime * m_CooldownSpeed);
		float num = Mathf.Sin(1f * m_Time * (float)Math.PI * 2f);
		float num2 = 1f - Mathf.Cos(2f * m_Time * (float)Math.PI * 2f);
		m_Vector.x = num * m_AmountX;
		m_Vector.y = num2 * m_AmountY;
		return m_Vector;
	}

	private float GetBestCooldownValue(float time)
	{
		float result = 0f;
		if (m_Time > 0.25f && m_Time < 0.5f)
		{
			result = 0.5f;
		}
		else if (m_Time > 0.5f && m_Time < 0.75f)
		{
			result = 0.5f;
		}
		else if (m_Time > 0.75f)
		{
			result = 1f;
		}
		return result;
	}
}
