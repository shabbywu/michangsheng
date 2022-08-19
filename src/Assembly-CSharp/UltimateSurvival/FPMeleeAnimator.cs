using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005ED RID: 1517
	public class FPMeleeAnimator : FPAnimator
	{
		// Token: 0x060030CD RID: 12493 RVA: 0x0015CFE8 File Offset: 0x0015B1E8
		protected override void Awake()
		{
			base.Awake();
			if (base.FPObject as FPMelee)
			{
				this.m_Melee = (base.FPObject as FPMelee);
				this.m_Melee.MeleeAttack.AddListener(new Action<bool>(this.On_MeleeAttack));
				this.m_Melee.Miss.AddListener(new Action(this.On_MeleeWoosh));
				this.m_Melee.Hit.AddListener(new Action(this.On_MeleeHit));
				base.Animator.SetFloat("Miss Speed", this.m_MissSpeed);
				base.Animator.SetFloat("Hit Speed", this.m_HitSpeed);
				return;
			}
			Debug.LogError("The animator is of type Melee, but no Melee script found on this game object!", this);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0015D0B0 File Offset: 0x0015B2B0
		protected override void OnValidate()
		{
			base.OnValidate();
			if (base.FPObject && base.FPObject.IsEnabled && base.Animator)
			{
				base.Animator.SetFloat("Hit Speed", this.m_HitSpeed);
				base.Animator.SetFloat("Miss Speed", this.m_MissSpeed);
			}
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0015D116 File Offset: 0x0015B316
		private void On_MeleeAttack(bool hitObject)
		{
			if (!hitObject)
			{
				base.Animator.SetTrigger("Miss");
				return;
			}
			base.Animator.SetTrigger("Hit");
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0015D13C File Offset: 0x0015B33C
		private void On_MeleeWoosh()
		{
			RaycastData raycastData = base.Player.RaycastData.Get();
			if (!raycastData || raycastData.HitInfo.distance > this.m_Melee.MaxReach)
			{
				this.m_MeleeWooshShake.Shake();
			}
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0015D188 File Offset: 0x0015B388
		private void On_MeleeHit()
		{
			this.m_MeleeHitShake.Shake();
		}

		// Token: 0x04002B01 RID: 11009
		[Header("Melee")]
		[SerializeField]
		private float m_MissSpeed = 2f;

		// Token: 0x04002B02 RID: 11010
		[SerializeField]
		private float m_HitSpeed = 1.5f;

		// Token: 0x04002B03 RID: 11011
		[SerializeField]
		private WeaponShake m_MeleeWooshShake;

		// Token: 0x04002B04 RID: 11012
		[SerializeField]
		private WeaponShake m_MeleeHitShake;

		// Token: 0x04002B05 RID: 11013
		private FPMelee m_Melee;
	}
}
