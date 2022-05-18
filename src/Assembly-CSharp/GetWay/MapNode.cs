using System;

namespace GetWay
{
	// Token: 0x02000AD6 RID: 2774
	public class MapNode
	{
		// Token: 0x060046C5 RID: 18117 RVA: 0x000327A2 File Offset: 0x000309A2
		public MapNode(int index, float x, float y)
		{
			this.X = x;
			this.Y = y;
			this.Index = index;
		}

		// Token: 0x04003EDB RID: 16091
		public float X;

		// Token: 0x04003EDC RID: 16092
		public float Y;

		// Token: 0x04003EDD RID: 16093
		public float F;

		// Token: 0x04003EDE RID: 16094
		public float G;

		// Token: 0x04003EDF RID: 16095
		public float H;

		// Token: 0x04003EE0 RID: 16096
		public int Index;

		// Token: 0x04003EE1 RID: 16097
		public MapNode Parent;
	}
}
