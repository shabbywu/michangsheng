using System;

namespace WXB
{
	// Token: 0x0200068C RID: 1676
	public interface IEffect
	{
		// Token: 0x0600351E RID: 13598
		void UpdateEffect(Draw draw, float deltaTime);

		// Token: 0x0600351F RID: 13599
		void Release();
	}
}
