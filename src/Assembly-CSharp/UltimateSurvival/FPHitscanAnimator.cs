using UnityEngine;

namespace UltimateSurvival;

public class FPHitscanAnimator : FPAnimator
{
	[Header("Hitscan")]
	[SerializeField]
	private float m_FireSpeed = 1f;

	[SerializeField]
	[Clamp(0f, 10f)]
	private int m_FireTypesCount = 1;

	[SerializeField]
	private WeaponShake m_FireShake;

	private FPHitscan m_Hitscan;

	protected override void Awake()
	{
		base.Awake();
		base.Player.Aim.AddStartListener(OnStart_Aim);
		base.Player.Aim.AddStopListener(OnStop_Aim);
		if (Object.op_Implicit((Object)(object)(base.FPObject as FPHitscan)))
		{
			m_Hitscan = base.FPObject as FPHitscan;
			m_Hitscan.Attack.AddListener(On_GunFired);
			base.Animator.SetFloat("Fire Speed", m_FireSpeed);
		}
		else
		{
			Debug.LogError((object)"The animator is of type Hitscan, but no Hitscan script found on this game object!", (Object)(object)this);
		}
	}

	protected override void OnValidate()
	{
		base.OnValidate();
		if (Object.op_Implicit((Object)(object)base.FPObject) && base.FPObject.IsEnabled && Object.op_Implicit((Object)(object)base.Animator))
		{
			base.Animator.SetFloat("Fire Speed", m_FireSpeed);
		}
	}

	private void OnStart_Aim()
	{
		if (base.FPObject.IsEnabled)
		{
			base.Animator.Play("Hold Pose", 0, 0f);
		}
	}

	private void OnStop_Aim()
	{
		if (base.FPObject.IsEnabled)
		{
			base.Animator.Play("Idle", 0, 0f);
		}
	}

	private void On_GunFired()
	{
		base.Animator.SetFloat("Fire Type", (float)Random.Range(0, m_FireTypesCount));
		base.Animator.SetTrigger("Fire");
		m_FireShake.Shake();
	}
}
