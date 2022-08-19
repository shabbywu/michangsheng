using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020006CA RID: 1738
	public class LoopGridViewItem : MonoBehaviour
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x001796C9 File Offset: 0x001778C9
		// (set) Token: 0x0600372E RID: 14126 RVA: 0x001796D1 File Offset: 0x001778D1
		public object UserObjectData
		{
			get
			{
				return this.mUserObjectData;
			}
			set
			{
				this.mUserObjectData = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x001796DA File Offset: 0x001778DA
		// (set) Token: 0x06003730 RID: 14128 RVA: 0x001796E2 File Offset: 0x001778E2
		public int UserIntData1
		{
			get
			{
				return this.mUserIntData1;
			}
			set
			{
				this.mUserIntData1 = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x001796EB File Offset: 0x001778EB
		// (set) Token: 0x06003732 RID: 14130 RVA: 0x001796F3 File Offset: 0x001778F3
		public int UserIntData2
		{
			get
			{
				return this.mUserIntData2;
			}
			set
			{
				this.mUserIntData2 = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x001796FC File Offset: 0x001778FC
		// (set) Token: 0x06003734 RID: 14132 RVA: 0x00179704 File Offset: 0x00177904
		public string UserStringData1
		{
			get
			{
				return this.mUserStringData1;
			}
			set
			{
				this.mUserStringData1 = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x0017970D File Offset: 0x0017790D
		// (set) Token: 0x06003736 RID: 14134 RVA: 0x00179715 File Offset: 0x00177915
		public string UserStringData2
		{
			get
			{
				return this.mUserStringData2;
			}
			set
			{
				this.mUserStringData2 = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06003737 RID: 14135 RVA: 0x0017971E File Offset: 0x0017791E
		// (set) Token: 0x06003738 RID: 14136 RVA: 0x00179726 File Offset: 0x00177926
		public int ItemCreatedCheckFrameCount
		{
			get
			{
				return this.mItemCreatedCheckFrameCount;
			}
			set
			{
				this.mItemCreatedCheckFrameCount = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x0017972F File Offset: 0x0017792F
		public RectTransform CachedRectTransform
		{
			get
			{
				if (this.mCachedRectTransform == null)
				{
					this.mCachedRectTransform = base.gameObject.GetComponent<RectTransform>();
				}
				return this.mCachedRectTransform;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600373A RID: 14138 RVA: 0x00179756 File Offset: 0x00177956
		// (set) Token: 0x0600373B RID: 14139 RVA: 0x0017975E File Offset: 0x0017795E
		public string ItemPrefabName
		{
			get
			{
				return this.mItemPrefabName;
			}
			set
			{
				this.mItemPrefabName = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x00179767 File Offset: 0x00177967
		// (set) Token: 0x0600373D RID: 14141 RVA: 0x0017976F File Offset: 0x0017796F
		public int Row
		{
			get
			{
				return this.mRow;
			}
			set
			{
				this.mRow = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600373E RID: 14142 RVA: 0x00179778 File Offset: 0x00177978
		// (set) Token: 0x0600373F RID: 14143 RVA: 0x00179780 File Offset: 0x00177980
		public int Column
		{
			get
			{
				return this.mColumn;
			}
			set
			{
				this.mColumn = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x00179789 File Offset: 0x00177989
		// (set) Token: 0x06003741 RID: 14145 RVA: 0x00179791 File Offset: 0x00177991
		public int ItemIndex
		{
			get
			{
				return this.mItemIndex;
			}
			set
			{
				this.mItemIndex = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x0017979A File Offset: 0x0017799A
		// (set) Token: 0x06003743 RID: 14147 RVA: 0x001797A2 File Offset: 0x001779A2
		public int ItemId
		{
			get
			{
				return this.mItemId;
			}
			set
			{
				this.mItemId = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06003744 RID: 14148 RVA: 0x001797AB File Offset: 0x001779AB
		// (set) Token: 0x06003745 RID: 14149 RVA: 0x001797B3 File Offset: 0x001779B3
		public bool IsInitHandlerCalled
		{
			get
			{
				return this.mIsInitHandlerCalled;
			}
			set
			{
				this.mIsInitHandlerCalled = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06003746 RID: 14150 RVA: 0x001797BC File Offset: 0x001779BC
		// (set) Token: 0x06003747 RID: 14151 RVA: 0x001797C4 File Offset: 0x001779C4
		public LoopGridView ParentGridView
		{
			get
			{
				return this.mParentGridView;
			}
			set
			{
				this.mParentGridView = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x001797CD File Offset: 0x001779CD
		// (set) Token: 0x06003749 RID: 14153 RVA: 0x001797D5 File Offset: 0x001779D5
		public LoopGridViewItem PrevItem
		{
			get
			{
				return this.mPrevItem;
			}
			set
			{
				this.mPrevItem = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x001797DE File Offset: 0x001779DE
		// (set) Token: 0x0600374B RID: 14155 RVA: 0x001797E6 File Offset: 0x001779E6
		public LoopGridViewItem NextItem
		{
			get
			{
				return this.mNextItem;
			}
			set
			{
				this.mNextItem = value;
			}
		}

		// Token: 0x04003003 RID: 12291
		private int mItemIndex = -1;

		// Token: 0x04003004 RID: 12292
		private int mRow = -1;

		// Token: 0x04003005 RID: 12293
		private int mColumn = -1;

		// Token: 0x04003006 RID: 12294
		private int mItemId = -1;

		// Token: 0x04003007 RID: 12295
		private LoopGridView mParentGridView;

		// Token: 0x04003008 RID: 12296
		private bool mIsInitHandlerCalled;

		// Token: 0x04003009 RID: 12297
		private string mItemPrefabName;

		// Token: 0x0400300A RID: 12298
		private RectTransform mCachedRectTransform;

		// Token: 0x0400300B RID: 12299
		private int mItemCreatedCheckFrameCount;

		// Token: 0x0400300C RID: 12300
		private object mUserObjectData;

		// Token: 0x0400300D RID: 12301
		private int mUserIntData1;

		// Token: 0x0400300E RID: 12302
		private int mUserIntData2;

		// Token: 0x0400300F RID: 12303
		private string mUserStringData1;

		// Token: 0x04003010 RID: 12304
		private string mUserStringData2;

		// Token: 0x04003011 RID: 12305
		private LoopGridViewItem mPrevItem;

		// Token: 0x04003012 RID: 12306
		private LoopGridViewItem mNextItem;
	}
}
