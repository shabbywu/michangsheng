using System;

namespace UltimateSurvival
{
	// Token: 0x02000895 RID: 2197
	public class Message
	{
		// Token: 0x0600388D RID: 14477 RVA: 0x00029288 File Offset: 0x00027488
		public void AddListener(Action listener)
		{
			this.m_Listeners = (Action)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000292A1 File Offset: 0x000274A1
		public void RemoveListener(Action listener)
		{
			this.m_Listeners = (Action)Delegate.Remove(this.m_Listeners, listener);
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000292BA File Offset: 0x000274BA
		public void Send()
		{
			if (this.m_Listeners != null)
			{
				this.m_Listeners();
			}
		}

		// Token: 0x040032F7 RID: 13047
		private Action m_Listeners;
	}
}
