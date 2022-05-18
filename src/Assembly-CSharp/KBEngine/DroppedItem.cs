using System;

namespace KBEngine
{
	// Token: 0x0200100C RID: 4108
	public class DroppedItem : DroppedItemBase
	{
		// Token: 0x0600622A RID: 25130 RVA: 0x000042DD File Offset: 0x000024DD
		public override void __init__()
		{
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x000440A5 File Offset: 0x000422A5
		public void pickUpRequest()
		{
			base.cellCall("pickUpRequest", Array.Empty<object>());
		}
	}
}
