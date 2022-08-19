using System;

namespace KBEngine
{
	// Token: 0x02000C6C RID: 3180
	public class Build : BuildBase
	{
		// Token: 0x060057AF RID: 22447 RVA: 0x00004095 File Offset: 0x00002295
		public override void __init__()
		{
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x00246F82 File Offset: 0x00245182
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
