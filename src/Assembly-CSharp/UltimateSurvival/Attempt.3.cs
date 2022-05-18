using System;

namespace UltimateSurvival
{
	// Token: 0x02000893 RID: 2195
	public class Attempt<T, V>
	{
		// Token: 0x06003884 RID: 14468 RVA: 0x00029219 File Offset: 0x00027419
		public void SetTryer(Attempt<T, V>.GenericTryerDelegate tryer)
		{
			this.m_Tryer = tryer;
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x00029222 File Offset: 0x00027422
		public void AddListener(Action<T, V> listener)
		{
			this.m_Listeners = (Action<T, V>)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x0002923B File Offset: 0x0002743B
		public void RemoveListener(Action<T, V> listener)
		{
			this.m_Listeners = (Action<T, V>)Delegate.Remove(this.m_Listeners, listener);
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x00029254 File Offset: 0x00027454
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

		// Token: 0x040032F5 RID: 13045
		private Attempt<T, V>.GenericTryerDelegate m_Tryer;

		// Token: 0x040032F6 RID: 13046
		private Action<T, V> m_Listeners;

		// Token: 0x02000894 RID: 2196
		// (Invoke) Token: 0x0600388A RID: 14474
		public delegate bool GenericTryerDelegate(T arg1, V arg2);
	}
}
