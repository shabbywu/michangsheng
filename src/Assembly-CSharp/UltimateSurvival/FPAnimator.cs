using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005E5 RID: 1509
	[RequireComponent(typeof(FPObject))]
	public class FPAnimator : PlayerBehaviour
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x0015B9B5 File Offset: 0x00159BB5
		public Animator Animator
		{
			get
			{
				return this.m_Animator;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x0015B9BD File Offset: 0x00159BBD
		public FPObject FPObject
		{
			get
			{
				return this.m_Object;
			}
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x0015B9C8 File Offset: 0x00159BC8
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

		// Token: 0x06003090 RID: 12432 RVA: 0x0015BA80 File Offset: 0x00159C80
		protected virtual void OnValidate()
		{
			if (this.FPObject && this.FPObject.IsEnabled && this.Animator)
			{
				this.m_Animator.SetFloat("Draw Speed", this.m_DrawSpeed);
				this.m_Animator.SetFloat("Holster Speed", this.m_HolsterSpeed);
			}
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x0015BAE0 File Offset: 0x00159CE0
		private void On_Draw()
		{
			this.OnValidate();
			if (this.m_Animator)
			{
				this.m_Animator.SetTrigger("Draw");
			}
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0015BB05 File Offset: 0x00159D05
		private void On_Holster()
		{
			if (this.m_Animator)
			{
				this.m_Animator.SetTrigger("Holster");
			}
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0015BB24 File Offset: 0x00159D24
		private void OnStop_Sleep()
		{
			if (this.FPObject.IsEnabled)
			{
				this.OnValidate();
			}
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x0015BB24 File Offset: 0x00159D24
		private void On_Respawn()
		{
			if (this.FPObject.IsEnabled)
			{
				this.OnValidate();
			}
		}

		// Token: 0x04002AB1 RID: 10929
		[SerializeField]
		private Animator m_Animator;

		// Token: 0x04002AB2 RID: 10930
		[Header("General")]
		[SerializeField]
		private float m_DrawSpeed = 1f;

		// Token: 0x04002AB3 RID: 10931
		[SerializeField]
		private float m_HolsterSpeed = 1f;

		// Token: 0x04002AB4 RID: 10932
		private FPObject m_Object;

		// Token: 0x04002AB5 RID: 10933
		private bool m_Initialized;

		// Token: 0x020014B9 RID: 5305
		public enum ObjectType
		{
			// Token: 0x04006D04 RID: 27908
			Normal,
			// Token: 0x04006D05 RID: 27909
			Melee,
			// Token: 0x04006D06 RID: 27910
			Throwable,
			// Token: 0x04006D07 RID: 27911
			Hitscan,
			// Token: 0x04006D08 RID: 27912
			Bow
		}
	}
}
