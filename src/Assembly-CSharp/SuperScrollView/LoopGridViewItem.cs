using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020009F7 RID: 2551
	public class LoopGridViewItem : MonoBehaviour
	{
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x0002ECF0 File Offset: 0x0002CEF0
		// (set) Token: 0x0600414E RID: 16718 RVA: 0x0002ECF8 File Offset: 0x0002CEF8
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

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x0002ED01 File Offset: 0x0002CF01
		// (set) Token: 0x06004150 RID: 16720 RVA: 0x0002ED09 File Offset: 0x0002CF09
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

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x0002ED12 File Offset: 0x0002CF12
		// (set) Token: 0x06004152 RID: 16722 RVA: 0x0002ED1A File Offset: 0x0002CF1A
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

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x0002ED23 File Offset: 0x0002CF23
		// (set) Token: 0x06004154 RID: 16724 RVA: 0x0002ED2B File Offset: 0x0002CF2B
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

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x0002ED34 File Offset: 0x0002CF34
		// (set) Token: 0x06004156 RID: 16726 RVA: 0x0002ED3C File Offset: 0x0002CF3C
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

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x0002ED45 File Offset: 0x0002CF45
		// (set) Token: 0x06004158 RID: 16728 RVA: 0x0002ED4D File Offset: 0x0002CF4D
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

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x0002ED56 File Offset: 0x0002CF56
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

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x0002ED7D File Offset: 0x0002CF7D
		// (set) Token: 0x0600415B RID: 16731 RVA: 0x0002ED85 File Offset: 0x0002CF85
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

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600415C RID: 16732 RVA: 0x0002ED8E File Offset: 0x0002CF8E
		// (set) Token: 0x0600415D RID: 16733 RVA: 0x0002ED96 File Offset: 0x0002CF96
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

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x0002ED9F File Offset: 0x0002CF9F
		// (set) Token: 0x0600415F RID: 16735 RVA: 0x0002EDA7 File Offset: 0x0002CFA7
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

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x0002EDB0 File Offset: 0x0002CFB0
		// (set) Token: 0x06004161 RID: 16737 RVA: 0x0002EDB8 File Offset: 0x0002CFB8
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

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x0002EDC1 File Offset: 0x0002CFC1
		// (set) Token: 0x06004163 RID: 16739 RVA: 0x0002EDC9 File Offset: 0x0002CFC9
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

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x0002EDD2 File Offset: 0x0002CFD2
		// (set) Token: 0x06004165 RID: 16741 RVA: 0x0002EDDA File Offset: 0x0002CFDA
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

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06004166 RID: 16742 RVA: 0x0002EDE3 File Offset: 0x0002CFE3
		// (set) Token: 0x06004167 RID: 16743 RVA: 0x0002EDEB File Offset: 0x0002CFEB
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

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x0002EDF4 File Offset: 0x0002CFF4
		// (set) Token: 0x06004169 RID: 16745 RVA: 0x0002EDFC File Offset: 0x0002CFFC
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

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x0002EE05 File Offset: 0x0002D005
		// (set) Token: 0x0600416B RID: 16747 RVA: 0x0002EE0D File Offset: 0x0002D00D
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

		// Token: 0x04003A13 RID: 14867
		private int mItemIndex = -1;

		// Token: 0x04003A14 RID: 14868
		private int mRow = -1;

		// Token: 0x04003A15 RID: 14869
		private int mColumn = -1;

		// Token: 0x04003A16 RID: 14870
		private int mItemId = -1;

		// Token: 0x04003A17 RID: 14871
		private LoopGridView mParentGridView;

		// Token: 0x04003A18 RID: 14872
		private bool mIsInitHandlerCalled;

		// Token: 0x04003A19 RID: 14873
		private string mItemPrefabName;

		// Token: 0x04003A1A RID: 14874
		private RectTransform mCachedRectTransform;

		// Token: 0x04003A1B RID: 14875
		private int mItemCreatedCheckFrameCount;

		// Token: 0x04003A1C RID: 14876
		private object mUserObjectData;

		// Token: 0x04003A1D RID: 14877
		private int mUserIntData1;

		// Token: 0x04003A1E RID: 14878
		private int mUserIntData2;

		// Token: 0x04003A1F RID: 14879
		private string mUserStringData1;

		// Token: 0x04003A20 RID: 14880
		private string mUserStringData2;

		// Token: 0x04003A21 RID: 14881
		private LoopGridViewItem mPrevItem;

		// Token: 0x04003A22 RID: 14882
		private LoopGridViewItem mNextItem;
	}
}
