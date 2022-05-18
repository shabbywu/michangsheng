using System;

namespace WXB
{
	// Token: 0x0200099E RID: 2462
	public interface IEffect
	{
		// Token: 0x06003ED9 RID: 16089
		void UpdateEffect(Draw draw, float deltaTime);

		// Token: 0x06003EDA RID: 16090
		void Release();
	}
}
