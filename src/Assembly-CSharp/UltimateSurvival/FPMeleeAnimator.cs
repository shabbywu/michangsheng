using UnityEngine;

namespace UltimateSurvival;

public class FPMeleeAnimator : FPAnimator
{
	[Header("Melee")]
	[SerializeField]
	private float m_MissSpeed = 2f;

	[SerializeField]
	private float m_HitSpeed = 1.5f;

	[SerializeField]
	private WeaponShake m_MeleeWooshShake;

	[SerializeField]
	private WeaponShake m_MeleeHitShake;

	private FPMelee m_Melee;

	protected override void Awake()
	{
		base.Awake();
		if (Object.op_Implicit((Object)(object)(base.FPObject as FPMelee)))
		{
			m_Melee = base.FPObject as FPMelee;
			m_Melee.MeleeAttack.AddListener(On_MeleeAttack);
			m_Melee.Miss.AddListener(On_MeleeWoosh);
			m_Melee.Hit.AddListener(On_MeleeHit);
			base.Animator.SetFloat("Miss Speed", m_MissSpeed);
			base.Animator.SetFloat("Hit Speed", m_HitSpeed);
		}
		else
		{
			Debug.LogError((object)"The animator is of type Melee, but no Melee script found on this game object!", (Object)(object)this);
		}
	}

	protected override void OnValidate()
	{
		base.OnValidate();
		if (Object.op_Implicit((Object)(object)base.FPObject) && base.FPObject.IsEnabled && Object.op_Implicit((Object)(object)base.Animator))
		{
			base.Animator.SetFloat("Hit Speed", m_HitSpeed);
			base.Animator.SetFloat("Miss Speed", m_MissSpeed);
		}
	}

	private void On_MeleeAttack(bool hitObject)
	{
		if (!hitObject)
		{
			base.Animator.SetTrigger("Miss");
		}
		else
		{
			base.Animator.SetTrigger("Hit");
		}
	}

	private void On_MeleeWoosh()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		RaycastData raycastData = base.Player.RaycastData.Get();
		if ((bool)raycastData)
		{
			RaycastHit hitInfo = raycastData.HitInfo;
			if (!(((RaycastHit)(ref hitInfo)).distance > m_Melee.MaxReach))
			{
				return;
			}
		}
		m_MeleeWooshShake.Shake();
	}

	private void On_MeleeHit()
	{
		m_MeleeHitShake.Shake();
	}
}
