using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008C8 RID: 2248
	public class FPSpearAnimator : FPAnimator
	{
		// Token: 0x060039DC RID: 14812 RVA: 0x001A6FD0 File Offset: 0x001A51D0
		protected override void Awake()
		{
			base.Awake();
			base.Player.Aim.AddStartListener(new Action(this.OnStart_Aim));
			base.Player.Aim.AddStopListener(new Action(this.OnStop_Aim));
			if (base.FPObject as FPSpear)
			{
				this.m_Spear = (base.FPObject as FPSpear);
				this.m_Spear.Attack.AddListener(new Action(this.On_Thrown));
				base.Animator.SetFloat("Throw Speed", this.m_ThrowSpeed);
				return;
			}
			Debug.LogError("The animator is of type Spear, but no Spear script found on this game object!", this);
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x001A707C File Offset: 0x001A527C
		protected override void OnValidate()
		{
			base.OnValidate();
			if (base.FPObject && base.FPObject.IsEnabled && base.Animator)
			{
				base.Animator.SetFloat("Throw Speed", this.m_ThrowSpeed);
			}
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x0002A08C File Offset: 0x0002828C
		private void OnStart_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.SetBool("Ready", true);
			}
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x0002A0AC File Offset: 0x000282AC
		private void OnStop_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.Play("Idle", 0, 0f);
				base.Animator.SetBool("Ready", false);
			}
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x0002A0E2 File Offset: 0x000282E2
		private void On_Thrown()
		{
			base.Animator.SetBool("Ready", false);
			base.Animator.SetTrigger("Throw");
			this.m_ThrowShake.Shake();
		}

		// Token: 0x0400340D RID: 13325
		[Header("Spear")]
		[SerializeField]
		private float m_ThrowSpeed = 1f;

		// Token: 0x0400340E RID: 13326
		[SerializeField]
		private WeaponShake m_ThrowShake;

		// Token: 0x0400340F RID: 13327
		private FPSpear m_Spear;
	}
}
