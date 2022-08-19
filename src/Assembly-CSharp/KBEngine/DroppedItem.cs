using System;

namespace KBEngine
{
	// Token: 0x02000C6D RID: 3181
	public class DroppedItem : DroppedItemBase
	{
		// Token: 0x060057B2 RID: 22450 RVA: 0x00004095 File Offset: 0x00002295
		public override void __init__()
		{
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x00246FA6 File Offset: 0x002451A6
		public void pickUpRequest()
		{
			base.cellCall("pickUpRequest", Array.Empty<object>());
		}
	}
}
