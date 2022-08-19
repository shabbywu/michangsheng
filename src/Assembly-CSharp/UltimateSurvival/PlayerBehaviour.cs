using System;

namespace UltimateSurvival
{
	// Token: 0x020005C8 RID: 1480
	public class PlayerBehaviour : EntityBehaviour
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x00159107 File Offset: 0x00157307
		public PlayerEventHandler Player
		{
			get
			{
				if (!this.m_Player)
				{
					this.m_Player = base.GetComponent<PlayerEventHandler>();
				}
				if (!this.m_Player)
				{
					this.m_Player = base.GetComponentInParent<PlayerEventHandler>();
				}
				return this.m_Player;
			}
		}

		// Token: 0x04002A27 RID: 10791
		private PlayerEventHandler m_Player;
	}
}
