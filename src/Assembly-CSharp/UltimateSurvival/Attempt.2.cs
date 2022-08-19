using System;

namespace UltimateSurvival
{
	// Token: 0x020005CF RID: 1487
	public class Attempt<T>
	{
		// Token: 0x06002FEB RID: 12267 RVA: 0x001595F5 File Offset: 0x001577F5
		public void SetTryer(Attempt<T>.GenericTryerDelegate tryer)
		{
			this.m_Tryer = tryer;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x001595FE File Offset: 0x001577FE
		public void AddListener(Action<T> listener)
		{
			this.m_Listeners = (Action<T>)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x00159617 File Offset: 0x00157817
		public void RemoveListener(Action<T> listener)
		{
			this.m_Listeners = (Action<T>)Delegate.Remove(this.m_Listeners, listener);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x00159630 File Offset: 0x00157830
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

		// Token: 0x04002A5D RID: 10845
		private Attempt<T>.GenericTryerDelegate m_Tryer;

		// Token: 0x04002A5E RID: 10846
		private Action<T> m_Listeners;

		// Token: 0x020014AC RID: 5292
		// (Invoke) Token: 0x0600816D RID: 33133
		public delegate bool GenericTryerDelegate(T arg);
	}
}
