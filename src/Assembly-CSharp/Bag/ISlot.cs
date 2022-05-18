using System;

namespace Bag
{
	// Token: 0x02000D41 RID: 3393
	public interface ISlot
	{
		// Token: 0x06005090 RID: 20624
		void SetSlotData(object data);

		// Token: 0x06005091 RID: 20625
		void SetAccptType(CanSlotType slotType);

		// Token: 0x06005092 RID: 20626
		void SetNull();

		// Token: 0x06005093 RID: 20627
		void InitUI();

		// Token: 0x06005094 RID: 20628
		void UpdateUI();
	}
}
