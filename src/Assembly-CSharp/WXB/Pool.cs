using System;
using System.Text;

namespace WXB
{
	// Token: 0x020009B2 RID: 2482
	internal static class Pool
	{
		// Token: 0x06003F34 RID: 16180 RVA: 0x001B8CF8 File Offset: 0x001B6EF8
		public static PD<StringBuilder> GetSB()
		{
			if (Pool.sb_factory == null)
			{
				Pool.sb_factory = new Factory<StringBuilder>(delegate(StringBuilder sb)
				{
					sb.Length = 0;
				});
			}
			return (PD<StringBuilder>)Pool.sb_factory.create();
		}

		// Token: 0x040038C0 RID: 14528
		private static Factory<StringBuilder> sb_factory;
	}
}
