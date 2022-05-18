using System;

namespace Fungus
{
	// Token: 0x02001354 RID: 4948
	[Serializable]
	public struct BlockReference
	{
		// Token: 0x06007812 RID: 30738 RVA: 0x00051A55 File Offset: 0x0004FC55
		public void Execute()
		{
			if (this.block != null)
			{
				this.block.StartExecution();
			}
		}

		// Token: 0x04006840 RID: 26688
		public Block block;
	}
}
