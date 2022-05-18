using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x02000A01 RID: 2561
	public class GridViewLayoutParam
	{
		// Token: 0x060041F2 RID: 16882 RVA: 0x001C6B8C File Offset: 0x001C4D8C
		public bool CheckParam()
		{
			if (this.mColumnOrRowCount <= 0)
			{
				Debug.LogError("mColumnOrRowCount shoud be > 0");
				return false;
			}
			if (this.mItemWidthOrHeight <= 0f)
			{
				Debug.LogError("mItemWidthOrHeight shoud be > 0");
				return false;
			}
			if (this.mCustomColumnOrRowOffsetArray != null && this.mCustomColumnOrRowOffsetArray.Length != this.mColumnOrRowCount)
			{
				Debug.LogError("mGroupOffsetArray.Length != mColumnOrRowCount");
				return false;
			}
			return true;
		}

		// Token: 0x04003A8E RID: 14990
		public int mColumnOrRowCount;

		// Token: 0x04003A8F RID: 14991
		public float mItemWidthOrHeight;

		// Token: 0x04003A90 RID: 14992
		public float mPadding1;

		// Token: 0x04003A91 RID: 14993
		public float mPadding2;

		// Token: 0x04003A92 RID: 14994
		public float[] mCustomColumnOrRowOffsetArray;
	}
}
