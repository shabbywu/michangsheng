using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F3 RID: 1523
	public class FPSpearAnimator : FPAnimator
	{
		// Token: 0x060030F8 RID: 12536 RVA: 0x0015D9EC File Offset: 0x0015BBEC
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

		// Token: 0x060030F9 RID: 12537 RVA: 0x0015DA98 File Offset: 0x0015BC98
		protected override void OnValidate()
		{
			base.OnValidate();
			if (base.FPObject && base.FPObject.IsEnabled && base.Animator)
			{
				base.Animator.SetFloat("Throw Speed", this.m_ThrowSpeed);
			}
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x0015DAE8 File Offset: 0x0015BCE8
		private void OnStart_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.SetBool("Ready", true);
			}
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x0015DB08 File Offset: 0x0015BD08
		private void OnStop_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.Play("Idle", 0, 0f);
				base.Animator.SetBool("Ready", false);
			}
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x0015DB3E File Offset: 0x0015BD3E
		private void On_Thrown()
		{
			base.Animator.SetBool("Ready", false);
			base.Animator.SetTrigger("Throw");
			this.m_ThrowShake.Shake();
		}

		// Token: 0x04002B32 RID: 11058
		[Header("Spear")]
		[SerializeField]
		private float m_ThrowSpeed = 1f;

		// Token: 0x04002B33 RID: 11059
		[SerializeField]
		private WeaponShake m_ThrowShake;

		// Token: 0x04002B34 RID: 11060
		private FPSpear m_Spear;
	}
}
