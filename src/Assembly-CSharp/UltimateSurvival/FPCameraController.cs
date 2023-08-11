using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public class FPCameraController : MonoSingleton<FPCameraController>
{
	[Tooltip("The default position influence of all shakes created on the fly.")]
	[SerializeField]
	private Vector3 m_DefaultPosInfluence = new Vector3(0.15f, 0.15f, 0.15f);

	[Tooltip("The default rotation influence of all shakes created on the fly.")]
	[SerializeField]
	private Vector3 m_DefaultRotInfluence = new Vector3(1f, 1f, 1f);

	[Header("Headbobs")]
	[SerializeField]
	private PlayerEventHandler m_Player;

	[SerializeField]
	private TrigonometricBob m_WalkHeadbob;

	[SerializeField]
	private TrigonometricBob m_RunHeadbob;

	[Header("Shakes")]
	[SerializeField]
	private GenericShake m_DamageShake;

	[SerializeField]
	private WeaponShake m_LandShake;

	[SerializeField]
	private float m_LandThreeshold = 3f;

	private Vector3 m_PositionAddShake;

	private Vector3 m_RotationAddShake;

	private Vector3 m_PositionAddBob;

	private Vector3 m_RotationAddBob;

	private List<ShakeInstance> m_ShakeInstances = new List<ShakeInstance>();

	public ShakeInstance Shake(ShakeInstance shake)
	{
		m_ShakeInstances.Add(shake);
		return shake;
	}

	public ShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
		shakeInstance.PositionInfluence = m_DefaultPosInfluence;
		shakeInstance.RotationInfluence = m_DefaultRotInfluence;
		m_ShakeInstances.Add(shakeInstance);
		return shakeInstance;
	}

	public ShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime, Vector3 posInfluence, Vector3 rotInfluence)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
		shakeInstance.PositionInfluence = posInfluence;
		shakeInstance.RotationInfluence = rotInfluence;
		m_ShakeInstances.Add(shakeInstance);
		return shakeInstance;
	}

	public ShakeInstance StartShake(float magnitude, float roughness, float fadeInTime)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness);
		shakeInstance.PositionInfluence = m_DefaultPosInfluence;
		shakeInstance.RotationInfluence = m_DefaultRotInfluence;
		shakeInstance.StartFadeIn(fadeInTime);
		m_ShakeInstances.Add(shakeInstance);
		return shakeInstance;
	}

	public ShakeInstance StartShake(float magnitude, float roughness, float fadeInTime, Vector3 posInfluence, Vector3 rotInfluence)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness);
		shakeInstance.PositionInfluence = posInfluence;
		shakeInstance.RotationInfluence = rotInfluence;
		shakeInstance.StartFadeIn(fadeInTime);
		m_ShakeInstances.Add(shakeInstance);
		return shakeInstance;
	}

	private void Awake()
	{
		m_Player.ChangeHealth.AddListener(OnSuccess_PlayerHealthChanged);
		m_Player.Land.AddListener(On_PlayerLanded);
	}

	private void OnSuccess_PlayerHealthChanged(HealthEventData healthEventData)
	{
		if (healthEventData.Delta < 0f && healthEventData.Delta < -8f)
		{
			m_DamageShake.Shake(Mathf.Abs(healthEventData.Delta / 100f));
		}
	}

	private void On_PlayerLanded(float landSpeed)
	{
		if (landSpeed > m_LandThreeshold)
		{
			m_LandShake.Shake();
		}
	}

	private void LateUpdate()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		m_PositionAddShake = Vector3.zero;
		m_RotationAddShake = Vector3.zero;
		for (int i = 0; i < m_ShakeInstances.Count && i < m_ShakeInstances.Count; i++)
		{
			ShakeInstance shakeInstance = m_ShakeInstances[i];
			if (shakeInstance.CurrentState == ShakeState.Inactive && shakeInstance.DeleteOnInactive)
			{
				m_ShakeInstances.RemoveAt(i);
				i--;
			}
			else if (shakeInstance.CurrentState != ShakeState.Inactive)
			{
				m_PositionAddShake += Vector3.Scale(shakeInstance.UpdateShake(), shakeInstance.PositionInfluence);
				m_RotationAddShake += Vector3.Scale(shakeInstance.UpdateShake(), shakeInstance.RotationInfluence);
			}
		}
		Vector3 val = m_Player.Velocity.Get();
		float magnitude = ((Vector3)(ref val)).magnitude;
		if (m_Player.Walk.Active)
		{
			m_PositionAddBob = m_WalkHeadbob.CalculateBob(magnitude, Time.deltaTime);
		}
		else
		{
			m_PositionAddBob = m_WalkHeadbob.Cooldown(Time.deltaTime);
		}
		if (m_Player.Run.Active)
		{
			m_PositionAddBob += m_RunHeadbob.CalculateBob(magnitude, Time.deltaTime);
		}
		else
		{
			m_PositionAddBob += m_RunHeadbob.Cooldown(Time.deltaTime);
		}
		((Component)this).transform.localPosition = m_PositionAddShake + m_PositionAddBob;
		((Component)this).transform.localEulerAngles = m_RotationAddShake;
	}
}
