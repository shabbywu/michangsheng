using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008C1 RID: 2241
	public class FPMeleeAnimator : FPAnimator
	{
		// Token: 0x060039AB RID: 14763 RVA: 0x001A66FC File Offset: 0x001A48FC
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

		// Token: 0x060039AC RID: 14764 RVA: 0x001A67C4 File Offset: 0x001A49C4
		protected override void OnValidate()
		{
			base.OnValidate();
			if (base.FPObject && base.FPObject.IsEnabled && base.Animator)
			{
				base.Animator.SetFloat("Hit Speed", this.m_HitSpeed);
				base.Animator.SetFloat("Miss Speed", this.m_MissSpeed);
			}
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x00029D9D File Offset: 0x00027F9D
		private void On_MeleeAttack(bool hitObject)
		{
			if (!hitObject)
			{
				base.Animator.SetTrigger("Miss");
				return;
			}
			base.Animator.SetTrigger("Hit");
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x001A682C File Offset: 0x001A4A2C
		private void On_MeleeWoosh()
		{
			RaycastData raycastData = base.Player.RaycastData.Get();
			if (!raycastData || raycastData.HitInfo.distance > this.m_Melee.MaxReach)
			{
				this.m_MeleeWooshShake.Shake();
			}
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x00029DC3 File Offset: 0x00027FC3
		private void On_MeleeHit()
		{
			this.m_MeleeHitShake.Shake();
		}

		// Token: 0x040033D7 RID: 13271
		[Header("Melee")]
		[SerializeField]
		private float m_MissSpeed = 2f;

		// Token: 0x040033D8 RID: 13272
		[SerializeField]
		private float m_HitSpeed = 1.5f;

		// Token: 0x040033D9 RID: 13273
		[SerializeField]
		private WeaponShake m_MeleeWooshShake;

		// Token: 0x040033DA RID: 13274
		[SerializeField]
		private WeaponShake m_MeleeHitShake;

		// Token: 0x040033DB RID: 13275
		private FPMelee m_Melee;
	}
}
