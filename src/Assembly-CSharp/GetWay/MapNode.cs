using System;

namespace GetWay
{
	// Token: 0x0200073E RID: 1854
	public class MapNode
	{
		// Token: 0x06003B0F RID: 15119 RVA: 0x001962C9 File Offset: 0x001944C9
		public MapNode(int index, float x, float y)
		{
			this.X = x;
			this.Y = y;
			this.Index = index;
		}

		// Token: 0x0400333F RID: 13119
		public float X;

		// Token: 0x04003340 RID: 13120
		public float Y;

		// Token: 0x04003341 RID: 13121
		public float F;

		// Token: 0x04003342 RID: 13122
		public float G;

		// Token: 0x04003343 RID: 13123
		public float H;

		// Token: 0x04003344 RID: 13124
		public int Index;

		// Token: 0x04003345 RID: 13125
		public MapNode Parent;
	}
}
