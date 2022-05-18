using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008B7 RID: 2231
	public class FPBowAnimator : FPAnimator
	{
		// Token: 0x0600396D RID: 14701 RVA: 0x001A563C File Offset: 0x001A383C
		protected override void Awake()
		{
			base.Awake();
			base.Player.Aim.AddStartListener(new Action(this.OnStart_Aim));
			base.Player.Aim.AddStopListener(new Action(this.OnStop_Aim));
			if (base.FPObject as FPBow)
			{
				this.m_Bow = (base.FPObject as FPBow);
				this.m_Bow.Attack.AddListener(new Action(this.On_Release));
				base.Animator.SetFloat("Release Speed", this.m_ReleaseSpeed);
				return;
			}
			Debug.LogError("The animator is of type Bow, but no Bow script found on this game object!", this);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x001A56E8 File Offset: 0x001A38E8
		protected override void OnValidate()
		{
			base.OnValidate();
			if (base.FPObject && base.FPObject.IsEnabled && base.Animator)
			{
				base.Animator.SetFloat("Release Speed", this.m_ReleaseSpeed);
			}
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x00029ACF File Offset: 0x00027CCF
		private void OnStart_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.SetBool("Aim", true);
			}
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x00029AEF File Offset: 0x00027CEF
		private void OnStop_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.SetBool("Aim", false);
			}
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x00029B0F File Offset: 0x00027D0F
		private void On_Release()
		{
			base.Animator.SetBool("Aim", false);
			base.Animator.SetTrigger("Release");
			this.m_ReleaseShake.Shake();
		}

		// Token: 0x04003386 RID: 13190
		[Header("Bow")]
		[SerializeField]
		private float m_ReleaseSpeed = 1f;

		// Token: 0x04003387 RID: 13191
		[SerializeField]
		private WeaponShake m_ReleaseShake;

		// Token: 0x04003388 RID: 13192
		private FPBow m_Bow;
	}
}
