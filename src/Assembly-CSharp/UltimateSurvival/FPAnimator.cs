using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008B4 RID: 2228
	[RequireComponent(typeof(FPObject))]
	public class FPAnimator : PlayerBehaviour
	{
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000299C9 File Offset: 0x00027BC9
		public Animator Animator
		{
			get
			{
				return this.m_Animator;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x000299D1 File Offset: 0x00027BD1
		public FPObject FPObject
		{
			get
			{
				return this.m_Object;
			}
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x001A5364 File Offset: 0x001A3564
		protected virtual void Awake()
		{
			this.m_Object = base.GetComponent<FPObject>();
			this.m_Object.Draw.AddListener(new Action(this.On_Draw));
			this.m_Object.Holster.AddListener(new Action(this.On_Holster));
			base.Player.Sleep.AddStopListener(new Action(this.OnStop_Sleep));
			base.Player.Respawn.AddListener(new Action(this.On_Respawn));
			this.m_Animator.SetFloat("Draw Speed", this.m_DrawSpeed);
			this.m_Animator.SetFloat("Holster Speed", this.m_HolsterSpeed);
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x001A541C File Offset: 0x001A361C
		protected virtual void OnValidate()
		{
			if (this.FPObject && this.FPObject.IsEnabled && this.Animator)
			{
				this.m_Animator.SetFloat("Draw Speed", this.m_DrawSpeed);
				this.m_Animator.SetFloat("Holster Speed", this.m_HolsterSpeed);
			}
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000299D9 File Offset: 0x00027BD9
		private void On_Draw()
		{
			this.OnValidate();
			if (this.m_Animator)
			{
				this.m_Animator.SetTrigger("Draw");
			}
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000299FE File Offset: 0x00027BFE
		private void On_Holster()
		{
			if (this.m_Animator)
			{
				this.m_Animator.SetTrigger("Holster");
			}
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x00029A1D File Offset: 0x00027C1D
		private void OnStop_Sleep()
		{
			if (this.FPObject.IsEnabled)
			{
				this.OnValidate();
			}
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x00029A1D File Offset: 0x00027C1D
		private void On_Respawn()
		{
			if (this.FPObject.IsEnabled)
			{
				this.OnValidate();
			}
		}

		// Token: 0x04003372 RID: 13170
		[SerializeField]
		private Animator m_Animator;

		// Token: 0x04003373 RID: 13171
		[Header("General")]
		[SerializeField]
		private float m_DrawSpeed = 1f;

		// Token: 0x04003374 RID: 13172
		[SerializeField]
		private float m_HolsterSpeed = 1f;

		// Token: 0x04003375 RID: 13173
		private FPObject m_Object;

		// Token: 0x04003376 RID: 13174
		private bool m_Initialized;

		// Token: 0x020008B5 RID: 2229
		public enum ObjectType
		{
			// Token: 0x04003378 RID: 13176
			Normal,
			// Token: 0x04003379 RID: 13177
			Melee,
			// Token: 0x0400337A RID: 13178
			Throwable,
			// Token: 0x0400337B RID: 13179
			Hitscan,
			// Token: 0x0400337C RID: 13180
			Bow
		}
	}
}
