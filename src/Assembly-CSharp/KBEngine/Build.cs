using System;

namespace KBEngine
{
	// Token: 0x0200100B RID: 4107
	public class Build : BuildBase
	{
		// Token: 0x06006227 RID: 25127 RVA: 0x000042DD File Offset: 0x000024DD
		public override void __init__()
		{
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x00044081 File Offset: 0x00042281
		public override object getDefinedProperty(string name)
		{
			if (name == "BuildId")
			{
				return this.BuildId;
			}
			return null;
		}
	}
}
