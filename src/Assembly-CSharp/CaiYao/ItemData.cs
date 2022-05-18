using System;

namespace CaiYao
{
	// Token: 0x02000A82 RID: 2690
	public class ItemData
	{
		// Token: 0x0600451F RID: 17695 RVA: 0x00031729 File Offset: 0x0002F929
		public ItemData(int itemId, int itemNum, int addNum, int addTime, bool hasEnemy, int firstEnemyId, int scondEnemyId)
		{
			this.ItemId = itemId;
			this.ItemNum = itemNum;
			this.AddNum = addNum;
			this.AddTime = addTime;
			this.HasEnemy = hasEnemy;
			this.FirstEnemyId = firstEnemyId;
			this.ScondEnemyId = scondEnemyId;
		}

		// Token: 0x04003D39 RID: 15673
		public int ItemId;

		// Token: 0x04003D3A RID: 15674
		public int ItemNum;

		// Token: 0x04003D3B RID: 15675
		public int AddNum;

		// Token: 0x04003D3C RID: 15676
		public int AddTime;

		// Token: 0x04003D3D RID: 15677
		public bool HasEnemy;

		// Token: 0x04003D3E RID: 15678
		public int FirstEnemyId;

		// Token: 0x04003D3F RID: 15679
		public int ScondEnemyId;
	}
}
