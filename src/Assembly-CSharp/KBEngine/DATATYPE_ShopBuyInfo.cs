using System;

namespace KBEngine
{
	// Token: 0x02000EB6 RID: 3766
	public class DATATYPE_ShopBuyInfo : DATATYPE_BASE
	{
		// Token: 0x06005AF8 RID: 23288 RVA: 0x00040293 File Offset: 0x0003E493
		public ShopBuyInfo createFromStreamEx(MemoryStream stream)
		{
			return new ShopBuyInfo
			{
				shopuuid = stream.readUint32(),
				buytime = stream.readUint8()
			};
		}

		// Token: 0x06005AF9 RID: 23289 RVA: 0x000402B2 File Offset: 0x0003E4B2
		public void addToStreamEx(Bundle stream, ShopBuyInfo v)
		{
			stream.writeUint32(v.shopuuid);
			stream.writeUint8(v.buytime);
		}
	}
}
