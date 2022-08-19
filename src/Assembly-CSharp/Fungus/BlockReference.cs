using System;

namespace Fungus
{
	// Token: 0x02000EBC RID: 3772
	[Serializable]
	public struct BlockReference
	{
		// Token: 0x06006A99 RID: 27289 RVA: 0x0029367F File Offset: 0x0029187F
		public void Execute()
		{
			if (this.block != null)
			{
				this.block.StartExecution();
			}
		}

		// Token: 0x040059F9 RID: 23033
		public Block block;
	}
}
