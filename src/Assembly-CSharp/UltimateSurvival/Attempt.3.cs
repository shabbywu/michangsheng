using System;

namespace UltimateSurvival
{
	// Token: 0x020005D0 RID: 1488
	public class Attempt<T, V>
	{
		// Token: 0x06002FF0 RID: 12272 RVA: 0x00159662 File Offset: 0x00157862
		public void SetTryer(Attempt<T, V>.GenericTryerDelegate tryer)
		{
			this.m_Tryer = tryer;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x0015966B File Offset: 0x0015786B
		public void AddListener(Action<T, V> listener)
		{
			this.m_Listeners = (Action<T, V>)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x00159684 File Offset: 0x00157884
		public void RemoveListener(Action<T, V> listener)
		{
			this.m_Listeners = (Action<T, V>)Delegate.Remove(this.m_Listeners, listener);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x0015969D File Offset: 0x0015789D
		public bool Try(T arg1, V arg2)
		{
			if (this.m_Tryer != null && this.m_Tryer(arg1, arg2))
			{
				if (this.m_Listeners != null)
				{
					this.m_Listeners(arg1, arg2);
				}
				return true;
			}
			return false;
		}

		// Token: 0x04002A5F RID: 10847
		private Attempt<T, V>.GenericTryerDelegate m_Tryer;

		// Token: 0x04002A60 RID: 10848
		private Action<T, V> m_Listeners;

		// Token: 0x020014AD RID: 5293
		// (Invoke) Token: 0x06008171 RID: 33137
		public delegate bool GenericTryerDelegate(T arg1, V arg2);
	}
}
