using System;

namespace UltimateSurvival
{
	// Token: 0x020005C6 RID: 1478
	public class AIBehaviour : EntityBehaviour
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x001590AC File Offset: 0x001572AC
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

		// Token: 0x04002A25 RID: 10789
		private AIEventHandler m_AI;
	}
}
