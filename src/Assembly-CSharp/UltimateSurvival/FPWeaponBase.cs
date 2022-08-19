using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F7 RID: 1527
	public abstract class FPWeaponBase : FPObject
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x0015DE04 File Offset: 0x0015C004
		public Message Attack
		{
			get
			{
				return this.m_Attack;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x0015DE0C File Offset: 0x0015C00C
		public bool UseWhileNearObjects
		{
			get
			{
				return this.m_UseWhileNearObjects;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x0015DE14 File Offset: 0x0015C014
		// (set) Token: 0x0600310E RID: 12558 RVA: 0x0015DE1C File Offset: 0x0015C01C
		public bool CanBeUsed { get; set; }

		// Token: 0x0600310F RID: 12559 RVA: 0x0015DE25 File Offset: 0x0015C025
		public override void On_Draw(SavableItem correspondingItem)
		{
			base.On_Draw(correspondingItem);
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual bool TryAttackOnce(Camera camera)
		{
			return false;
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual bool TryAttackContinuously(Camera camera)
		{
			return false;
		}

		// Token: 0x04002B44 RID: 11076
		private Message m_Attack = new Message();

		// Token: 0x04002B45 RID: 11077
		[SerializeField]
		[Tooltip("Can this weapon be used while too close to other objects? (e.g. a wall)")]
		private bool m_UseWhileNearObjects = true;
	}
}
