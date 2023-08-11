using UnityEngine;

namespace UltimateSurvival;

public class FPBowAnimator : FPAnimator
{
	[Header("Bow")]
	[SerializeField]
	private float m_ReleaseSpeed = 1f;

	[SerializeField]
	private WeaponShake m_ReleaseShake;

	private FPBow m_Bow;

	protected override void Awake()
	{
		base.Awake();
		base.Player.Aim.AddStartListener(OnStart_Aim);
		base.Player.Aim.AddStopListener(OnStop_Aim);
		if (Object.op_Implicit((Object)(object)(base.FPObject as FPBow)))
		{
			m_Bow = base.FPObject as FPBow;
			m_Bow.Attack.AddListener(On_Release);
			base.Animator.SetFloat("Release Speed", m_ReleaseSpeed);
		}
		else
		{
			Debug.LogError((object)"The animator is of type Bow, but no Bow script found on this game object!", (Object)(object)this);
		}
	}

	protected override void OnValidate()
	{
		base.OnValidate();
		if (Object.op_Implicit((Object)(object)base.FPObject) && base.FPObject.IsEnabled && Object.op_Implicit((Object)(object)base.Animator))
		{
			base.Animator.SetFloat("Release Speed", m_ReleaseSpeed);
		}
	}

	private void OnStart_Aim()
	{
		if (base.FPObject.IsEnabled)
		{
			base.Animator.SetBool("Aim", true);
		}
	}

	private void OnStop_Aim()
	{
		if (base.FPObject.IsEnabled)
		{
			base.Animator.SetBool("Aim", false);
		}
	}

	private void On_Release()
	{
		base.Animator.SetBool("Aim", false);
		base.Animator.SetTrigger("Release");
		m_ReleaseShake.Shake();
	}
}
