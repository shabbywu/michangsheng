using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008BB RID: 2235
	public class FPHitscanAnimator : FPAnimator
	{
		// Token: 0x06003983 RID: 14723 RVA: 0x001A5D6C File Offset: 0x001A3F6C
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

		// Token: 0x06003984 RID: 14724 RVA: 0x001A5E18 File Offset: 0x001A4018
		protected override void OnValidate()
		{
			base.OnValidate();
			if (base.FPObject && base.FPObject.IsEnabled && base.Animator)
			{
				base.Animator.SetFloat("Fire Speed", this.m_FireSpeed);
			}
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x00029BA5 File Offset: 0x00027DA5
		private void OnStart_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.Play("Hold Pose", 0, 0f);
			}
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x00029BCA File Offset: 0x00027DCA
		private void OnStop_Aim()
		{
			if (base.FPObject.IsEnabled)
			{
				base.Animator.Play("Idle", 0, 0f);
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x00029BEF File Offset: 0x00027DEF
		private void On_GunFired()
		{
			base.Animator.SetFloat("Fire Type", (float)Random.Range(0, this.m_FireTypesCount));
			base.Animator.SetTrigger("Fire");
			this.m_FireShake.Shake();
		}

		// Token: 0x040033AC RID: 13228
		[Header("Hitscan")]
		[SerializeField]
		private float m_FireSpeed = 1f;

		// Token: 0x040033AD RID: 13229
		[SerializeField]
		[Clamp(0f, 10f)]
		private int m_FireTypesCount = 1;

		// Token: 0x040033AE RID: 13230
		[SerializeField]
		private WeaponShake m_FireShake;

		// Token: 0x040033AF RID: 13231
		private FPHitscan m_Hitscan;
	}
}
