using UnityEngine;

namespace UltimateSurvival;

public class FPSpearAnimator : FPAnimator
{
	[Header("Spear")]
	[SerializeField]
	private float m_ThrowSpeed = 1f;

	[SerializeField]
	private WeaponShake m_ThrowShake;

	private FPSpear m_Spear;

	protected override void Awake()
	{
		base.Awake();
		base.Player.Aim.AddStartListener(OnStart_Aim);
		base.Player.Aim.AddStopListener(OnStop_Aim);
		if (Object.op_Implicit((Object)(object)(base.FPObject as FPSpear)))
		{
			m_Spear = base.FPObject as FPSpear;
			m_Spear.Attack.AddListener(On_Thrown);
			base.Animator.SetFloat("Throw Speed", m_ThrowSpeed);
		}
		else
		{
			Debug.LogError((object)"The animator is of type Spear, but no Spear script found on this game object!", (Object)(object)this);
		}
	}

	protected override void OnValidate()
	{
		base.OnValidate();
		if (Object.op_Implicit((Object)(object)base.FPObject) && base.FPObject.IsEnabled && Object.op_Implicit((Object)(object)base.Animator))
		{
			base.Animator.SetFloat("Throw Speed", m_ThrowSpeed);
		}
	}

	private void OnStart_Aim()
	{
		if (base.FPObject.IsEnabled)
		{
			base.Animator.SetBool("Ready", true);
		}
	}

	private void OnStop_Aim()
	{
		if (base.FPObject.IsEnabled)
		{
			base.Animator.Play("Idle", 0, 0f);
			base.Animator.SetBool("Ready", false);
		}
	}

	private void On_Thrown()
	{
		base.Animator.SetBool("Ready", false);
		base.Animator.SetTrigger("Throw");
		m_ThrowShake.Shake();
	}
}
