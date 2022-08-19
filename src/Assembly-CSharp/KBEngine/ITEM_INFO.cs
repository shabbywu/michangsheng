using System;

namespace KBEngine
{
	// Token: 0x02000BD3 RID: 3027
	public class ITEM_INFO
	{
		// Token: 0x06005415 RID: 21525 RVA: 0x002340B8 File Offset: 0x002322B8
		public override string ToString()
		{
			return "" + string.Format("UUID:{0}\n", this.UUID) + "uuid:" + this.uuid + "\n" + string.Format("itemId:{0}\n", this.itemId) + string.Format("itemCount:{0}\n", this.itemCount) + string.Format("itemIndex:{0}\n", this.itemIndex) + string.Format("Seid:{0}\n", this.Seid);
		}

		// Token: 0x0400506A RID: 20586
		public ulong UUID;

		// Token: 0x0400506B RID: 20587
		public string uuid = "";

		// Token: 0x0400506C RID: 20588
		public int itemId;

		// Token: 0x0400506D RID: 20589
		public uint itemCount;

		// Token: 0x0400506E RID: 20590
		public int itemIndex;

		// Token: 0x0400506F RID: 20591
		public JSONObject Seid = new JSONObject(JSONObject.Type.OBJECT);
	}
}
