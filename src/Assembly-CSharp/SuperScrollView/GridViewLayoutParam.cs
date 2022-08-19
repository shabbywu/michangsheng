using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020006D3 RID: 1747
	public class GridViewLayoutParam
	{
		// Token: 0x060037D0 RID: 14288 RVA: 0x0017ECC8 File Offset: 0x0017CEC8
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

		// Token: 0x04003076 RID: 12406
		public int mColumnOrRowCount;

		// Token: 0x04003077 RID: 12407
		public float mItemWidthOrHeight;

		// Token: 0x04003078 RID: 12408
		public float mPadding1;

		// Token: 0x04003079 RID: 12409
		public float mPadding2;

		// Token: 0x0400307A RID: 12410
		public float[] mCustomColumnOrRowOffsetArray;
	}
}
