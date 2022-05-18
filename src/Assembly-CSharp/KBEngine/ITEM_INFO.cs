using System;

namespace KBEngine
{
	// Token: 0x02000F56 RID: 3926
	public class ITEM_INFO
	{
		// Token: 0x06005E53 RID: 24147 RVA: 0x00261A2C File Offset: 0x0025FC2C
		public override string ToString()
		{
			return "" + string.Format("UUID:{0}\n", this.UUID) + "uuid:" + this.uuid + "\n" + string.Format("itemId:{0}\n", this.itemId) + string.Format("itemCount:{0}\n", this.itemCount) + string.Format("itemIndex:{0}\n", this.itemIndex) + string.Format("Seid:{0}\n", this.Seid);
		}

		// Token: 0x04005B0B RID: 23307
		public ulong UUID;

		// Token: 0x04005B0C RID: 23308
		public string uuid = "";

		// Token: 0x04005B0D RID: 23309
		public int itemId;

		// Token: 0x04005B0E RID: 23310
		public uint itemCount;

		// Token: 0x04005B0F RID: 23311
		public int itemIndex;

		// Token: 0x04005B10 RID: 23312
		public JSONObject Seid = new JSONObject(JSONObject.Type.OBJECT);
	}
}
