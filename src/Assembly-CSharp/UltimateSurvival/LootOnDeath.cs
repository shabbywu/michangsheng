using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008F4 RID: 2292
	public class LootOnDeath : EntityBehaviour
	{
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x0002AAB9 File Offset: 0x00028CB9
		// (set) Token: 0x06003AC6 RID: 15046 RVA: 0x0002AAC1 File Offset: 0x00028CC1
		public LootObject LootContainer
		{
			get
			{
				return this.m_LootContainer;
			}
			set
			{
				this.m_LootContainer = value;
			}
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x0002AACA File Offset: 0x00028CCA
		private void Start()
		{
			base.Entity.Death.AddListener(new Action(this.On_Death));
			this.m_LootContainer.enabled = false;
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x0002AAF4 File Offset: 0x00028CF4
		private void On_Death()
		{
			this.m_LootContainer.enabled = true;
		}

		// Token: 0x0400350E RID: 13582
		[SerializeField]
		private LootObject m_LootContainer;
	}
}
