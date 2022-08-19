using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005E9 RID: 1513
	public class FPHitscanAnimator : FPAnimator
	{
		// Token: 0x060030AB RID: 12459 RVA: 0x0015C50C File Offset: 0x0015A70C
		protected override void Awake()
		{
			base.Awake();
			base.Player.Aim.AddStartListener(new Action(this.OnStart_Aim));
			base.Player.Aim.AddStopListener(new Action(this.OnStop_Aim));
			if (base.FPObject as FPHitscan)
			{
				this.m_Hitscan = (base.FPObject as FPHitscan);
				this.m_Hitscan.Attack.AddListener(new Action(this.On_GunFired));
				base.Animator.SetFloat("Fire Speed", this.m_FireSpeed);
				return;
			}
			Debug.LogError("The animator is of type Hitscan, but no Hitscan script found on this game object!", this);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x0015C5B8 File Offset: 0x0015A7B8
		protected override void OnValidate()
		{
			base.OnValidate();
			if (base.FPObject && base.FPObject.IsEnabled && base.Animator)
			{
				base.Animator.SetFloat("Fire Speed", this.m_FireSpeed);
			}
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x0015C608 File Offset: 0x0015A808
		private void OnStart_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.Play("Hold Pose", 0, 0f);
			}
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x0015C62D File Offset: 0x0015A82D
		private void OnStop_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.Play("Idle", 0, 0f);
			}
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x0015C652 File Offset: 0x0015A852
		private void On_GunFired()
		{
			base.Animator.SetFloat("Fire Type", (float)Random.Range(0, this.m_FireTypesCount));
			base.Animator.SetTrigger("Fire");
			this.m_FireShake.Shake();
		}

		// Token: 0x04002ADD RID: 10973
		[Header("Hitscan")]
		[SerializeField]
		private float m_FireSpeed = 1f;

		// Token: 0x04002ADE RID: 10974
		[SerializeField]
		[Clamp(0f, 10f)]
		private int m_FireTypesCount = 1;

		// Token: 0x04002ADF RID: 10975
		[SerializeField]
		private WeaponShake m_FireShake;

		// Token: 0x04002AE0 RID: 10976
		private FPHitscan m_Hitscan;
	}
}
