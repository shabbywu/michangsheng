using System;

namespace UltimateSurvival
{
	// Token: 0x020005D1 RID: 1489
	public class Message
	{
		// Token: 0x06002FF5 RID: 12277 RVA: 0x001596D1 File Offset: 0x001578D1
		public void AddListener(Action listener)
		{
			this.m_Listeners = (Action)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x001596EA File Offset: 0x001578EA
		public void RemoveListener(Action listener)
		{
			this.m_Listeners = (Action)Delegate.Remove(this.m_Listeners, listener);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x00159703 File Offset: 0x00157903
		public void Send()
		{
			if (this.m_Listeners != null)
			{
				this.m_Listeners();
			}
		}

		// Token: 0x04002A61 RID: 10849
		private Action m_Listeners;
	}
}
