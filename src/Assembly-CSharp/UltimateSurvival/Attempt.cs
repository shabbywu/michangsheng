using System;

namespace UltimateSurvival
{
	// Token: 0x020005CE RID: 1486
	public class Attempt
	{
		// Token: 0x06002FE6 RID: 12262 RVA: 0x0015958A File Offset: 0x0015778A
		public void SetTryer(TryerDelegate tryer)
		{
			this.m_Tryer = tryer;
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x00159593 File Offset: 0x00157793
		public void AddListener(Action listener)
		{
			this.m_Listeners = (Action)Delegate.Combine(this.m_Listeners, listener);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x001595AC File Offset: 0x001577AC
		public void RemoveListener(Action listener)
		{
			this.m_Listeners = (Action)Delegate.Remove(this.m_Listeners, listener);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x001595C5 File Offset: 0x001577C5
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

		// Token: 0x04002A5B RID: 10843
		private TryerDelegate m_Tryer;

		// Token: 0x04002A5C RID: 10844
		private Action m_Listeners;
	}
}
