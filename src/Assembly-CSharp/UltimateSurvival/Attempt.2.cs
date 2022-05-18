using System;

namespace UltimateSurvival
{
	// Token: 0x02000891 RID: 2193
	public class Attempt<T>
	{
		// Token: 0x0600387B RID: 14459 RVA: 0x000291AC File Offset: 0x000273AC
		public void SetTryer(Attempt<T>.GenericTryerDelegate tryer)
		{
			this.m_Tryer = tryer;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000291B5 File Offset: 0x000273B5
		public void AddListener(Action<T> listener)
		{
			this.m_Listeners = (Action<T>)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000291CE File Offset: 0x000273CE
		public void RemoveListener(Action<T> listener)
		{
			this.m_Listeners = (Action<T>)Delegate.Remove(this.m_Listeners, listener);
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000291E7 File Offset: 0x000273E7
		public bool Try(T arg)
		{
			if (this.m_Tryer != null && this.m_Tryer(arg))
			{
				if (this.m_Listeners != null)
				{
					this.m_Listeners(arg);
				}
				return true;
			}
			return false;
		}

		// Token: 0x040032F3 RID: 13043
		private Attempt<T>.GenericTryerDelegate m_Tryer;

		// Token: 0x040032F4 RID: 13044
		private Action<T> m_Listeners;

		// Token: 0x02000892 RID: 2194
		// (Invoke) Token: 0x06003881 RID: 14465
		public delegate bool GenericTryerDelegate(T arg);
	}
}
