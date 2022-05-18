using System;

namespace UltimateSurvival
{
	// Token: 0x02000896 RID: 2198
	public class Message<T>
	{
		// Token: 0x06003891 RID: 14481 RVA: 0x000292CF File Offset: 0x000274CF
		public void AddListener(Action<T> listener)
		{
			this.m_Listeners = (Action<T>)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000292E8 File Offset: 0x000274E8
		public void RemoveListener(Action<T> callback)
		{
			this.m_Listeners = (Action<T>)Delegate.Remove(this.m_Listeners, callback);
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x00029301 File Offset: 0x00027501
		public void Send(T message)
		{
			if (this.m_Listeners != null)
			{
				this.m_Listeners(message);
			}
		}

		// Token: 0x040032F8 RID: 13048
		private Action<T> m_Listeners;
	}
}
