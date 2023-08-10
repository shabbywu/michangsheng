using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class GenericShake
{
	[Header("Shake")]
	[SerializeField]
	private float m_Magnitude = 15f;

	[SerializeField]
	private float m_MinMagnitudeScale = 0.5f;

	[SerializeField]
	private float m_Roughness = 3f;

	[SerializeField]
	private Vector3 m_PositionInfluence = new Vector3(0.01f, 0.01f, 0.01f);

	[SerializeField]
	private Vector3 m_RotationInfluence = new Vector3(0.8f, 0.5f, 0.5f);

	[SerializeField]
	private float m_FadeInTime = 0.2f;

	[SerializeField]
	private float m_FadeOutTime = 0.3f;

	public void Shake(float scale)
	{
		MonoSingleton<FPCameraController>.Instance.Shake(GetShakeInstance(scale));
	}

	private ShakeInstance GetShakeInstance(float scale)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		ShakeInstance obj = new ShakeInstance(m_Magnitude, m_Roughness, m_FadeInTime, m_FadeOutTime)
		{
			PositionInfluence = m_PositionInfluence,
			RotationInfluence = m_RotationInfluence
		};
		obj.ScaleMagnitude *= Mathf.Max(scale, m_MinMagnitudeScale);
		return obj;
	}
}
