using System;

namespace UltimateSurvival
{
	// Token: 0x020005D2 RID: 1490
	public class Message<T>
	{
		// Token: 0x06002FF9 RID: 12281 RVA: 0x00159718 File Offset: 0x00157918
		public void AddListener(Action<T> listener)
		{
			this.m_Listeners = (Action<T>)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x00159731 File Offset: 0x00157931
		public void RemoveListener(Action<T> callback)
		{
			this.m_Listeners = (Action<T>)Delegate.Remove(this.m_Listeners, callback);
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x0015974A File Offset: 0x0015794A
		public void Send(T message)
		{
			if (this.m_Listeners != null)
			{
				this.m_Listeners(message);
			}
		}

		// Token: 0x04002A62 RID: 10850
		private Action<T> m_Listeners;
	}
}
