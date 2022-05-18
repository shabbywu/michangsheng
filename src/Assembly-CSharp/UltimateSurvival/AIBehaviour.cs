using System;

namespace UltimateSurvival
{
	// Token: 0x02000888 RID: 2184
	public class AIBehaviour : EntityBehaviour
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x00028FB4 File Offset: 0x000271B4
		public AIEventHandler AI
		{
			get
			{
				if (!this.m_AI)
				{
					this.m_AI = base.GetComponentInParent<AIEventHandler>();
				}
				return this.m_AI;
			}
		}

		// Token: 0x040032BB RID: 12987
		private AIEventHandler m_AI;
	}
}
