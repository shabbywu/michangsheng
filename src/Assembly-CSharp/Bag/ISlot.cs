using System;

namespace Bag
{
	// Token: 0x020009B9 RID: 2489
	public interface ISlot
	{
		// Token: 0x0600452D RID: 17709
		void SetSlotData(object data);

		// Token: 0x0600452E RID: 17710
		void SetAccptType(CanSlotType slotType);

		// Token: 0x0600452F RID: 17711
		void SetNull();

		// Token: 0x06004530 RID: 17712
		void InitUI();

		// Token: 0x06004531 RID: 17713
		void UpdateUI();
	}
}
