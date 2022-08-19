using System;

namespace CaiYao
{
	// Token: 0x0200072A RID: 1834
	public class ItemData
	{
		// Token: 0x06003A75 RID: 14965 RVA: 0x0019182F File Offset: 0x0018FA2F
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

		// Token: 0x04003298 RID: 12952
		public int ItemId;

		// Token: 0x04003299 RID: 12953
		public int ItemNum;

		// Token: 0x0400329A RID: 12954
		public int AddNum;

		// Token: 0x0400329B RID: 12955
		public int AddTime;

		// Token: 0x0400329C RID: 12956
		public bool HasEnemy;

		// Token: 0x0400329D RID: 12957
		public int FirstEnemyId;

		// Token: 0x0400329E RID: 12958
		public int ScondEnemyId;
	}
}
