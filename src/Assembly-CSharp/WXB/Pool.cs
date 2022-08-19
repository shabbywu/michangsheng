using System;
using System.Text;

namespace WXB
{
	// Token: 0x0200069E RID: 1694
	internal static class Pool
	{
		// Token: 0x06003576 RID: 13686 RVA: 0x00170C90 File Offset: 0x0016EE90
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

		// Token: 0x04002F07 RID: 12039
		private static Factory<StringBuilder> sb_factory;
	}
}
