using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008CE RID: 2254
	public abstract class FPWeaponBase : FPObject
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x0002A259 File Offset: 0x00028459
		public Message Attack
		{
			get
			{
				return this.m_Attack;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060039F6 RID: 14838 RVA: 0x0002A261 File Offset: 0x00028461
		public bool UseWhileNearObjects
		{
			get
			{
				return this.m_UseWhileNearObjects;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x0002A269 File Offset: 0x00028469
		// (set) Token: 0x060039F8 RID: 14840 RVA: 0x0002A271 File Offset: 0x00028471
		public bool CanBeUsed { get; set; }

		// Token: 0x060039F9 RID: 14841 RVA: 0x0002A27A File Offset: 0x0002847A
		public override void On_Draw(SavableItem correspondingItem)
		{
			base.On_Draw(correspondingItem);
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x00004050 File Offset: 0x00002250
		public virtual bool TryAttackOnce(Camera camera)
		{
			return false;
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x00004050 File Offset: 0x00002250
		public virtual bool TryAttackContinuously(Camera camera)
		{
			return false;
		}

		// Token: 0x04003427 RID: 13351
		private Message m_Attack = new Message();

		// Token: 0x04003428 RID: 13352
		[SerializeField]
		[Tooltip("Can this weapon be used while too close to other objects? (e.g. a wall)")]
		private bool m_UseWhileNearObjects = true;
	}
}
