using System;

namespace UltimateSurvival
{
	// Token: 0x02000890 RID: 2192
	public class Attempt
	{
		// Token: 0x06003876 RID: 14454 RVA: 0x00029141 File Offset: 0x00027341
		public void SetTryer(TryerDelegate tryer)
		{
			this.m_Tryer = tryer;
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x0002914A File Offset: 0x0002734A
		public void AddListener(Action listener)
		{
			this.m_Listeners = (Action)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x00029163 File Offset: 0x00027363
		public void RemoveListener(Action listener)
		{
			this.m_Listeners = (Action)Delegate.Remove(this.m_Listeners, listener);
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x0002917C File Offset: 0x0002737C
		public bool Try()
		{
			if (this.m_Tryer == null || this.m_Tryer())
			{
				if (this.m_Listeners != null)
				{
					this.m_Listeners();
				}
				return true;
			}
			return false;
		}

		// Token: 0x040032F1 RID: 13041
		private TryerDelegate m_Tryer;

		// Token: 0x040032F2 RID: 13042
		private Action m_Listeners;
	}
}
