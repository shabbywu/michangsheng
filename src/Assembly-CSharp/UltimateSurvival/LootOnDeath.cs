using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000611 RID: 1553
	public class LootOnDeath : EntityBehaviour
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x00160BCA File Offset: 0x0015EDCA
		// (set) Token: 0x060031A9 RID: 12713 RVA: 0x00160BD2 File Offset: 0x0015EDD2
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

		// Token: 0x060031AA RID: 12714 RVA: 0x00160BDB File Offset: 0x0015EDDB
		private void Start()
		{
			base.Entity.Death.AddListener(new Action(this.On_Death));
			this.m_LootContainer.enabled = false;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x00160C05 File Offset: 0x0015EE05
		private void On_Death()
		{
			this.m_LootContainer.enabled = true;
		}

		// Token: 0x04002BFA RID: 11258
		[SerializeField]
		private LootObject m_LootContainer;
	}
}
