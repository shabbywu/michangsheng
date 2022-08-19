using System;

namespace KBEngine
{
	// Token: 0x02000B3F RID: 2879
	public class DATATYPE_ShopBuyInfo : DATATYPE_BASE
	{
		// Token: 0x060050CE RID: 20686 RVA: 0x00220DBA File Offset: 0x0021EFBA
		public ShopBuyInfo createFromStreamEx(MemoryStream stream)
		{
			return new ShopBuyInfo
			{
				shopuuid = stream.readUint32(),
				buytime = stream.readUint8()
			};
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x00220DD9 File Offset: 0x0021EFD9
		public void addToStreamEx(Bundle stream, ShopBuyInfo v)
		{
			stream.writeUint32(v.shopuuid);
			stream.writeUint8(v.buytime);
		}
	}
}
