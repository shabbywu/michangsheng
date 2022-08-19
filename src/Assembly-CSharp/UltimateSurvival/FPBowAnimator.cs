using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005E7 RID: 1511
	public class FPBowAnimator : FPAnimator
	{
		// Token: 0x0600309B RID: 12443 RVA: 0x0015BD98 File Offset: 0x00159F98
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

		// Token: 0x0600309C RID: 12444 RVA: 0x0015BE44 File Offset: 0x0015A044
		protected override void OnValidate()
		{
			base.OnValidate();
			if (base.FPObject && base.FPObject.IsEnabled && base.Animator)
			{
				base.Animator.SetFloat("Release Speed", this.m_ReleaseSpeed);
			}
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x0015BE94 File Offset: 0x0015A094
		private void OnStart_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.SetBool("Aim", true);
			}
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x0015BEB4 File Offset: 0x0015A0B4
		private void OnStop_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.SetBool("Aim", false);
			}
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0015BED4 File Offset: 0x0015A0D4
		private void On_Release()
		{
			base.Animator.SetBool("Aim", false);
			base.Animator.SetTrigger("Release");
			this.m_ReleaseShake.Shake();
		}

		// Token: 0x04002ABF RID: 10943
		[Header("Bow")]
		[SerializeField]
		private float m_ReleaseSpeed = 1f;

		// Token: 0x04002AC0 RID: 10944
		[SerializeField]
		private WeaponShake m_ReleaseShake;

		// Token: 0x04002AC1 RID: 10945
		private FPBow m_Bow;
	}
}
