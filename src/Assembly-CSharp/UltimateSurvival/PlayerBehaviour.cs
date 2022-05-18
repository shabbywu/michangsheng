using System;

namespace UltimateSurvival
{
	// Token: 0x0200088A RID: 2186
	public class PlayerBehaviour : EntityBehaviour
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x0002900F File Offset: 0x0002720F
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

		// Token: 0x040032BD RID: 12989
		private PlayerEventHandler m_Player;
	}
}
