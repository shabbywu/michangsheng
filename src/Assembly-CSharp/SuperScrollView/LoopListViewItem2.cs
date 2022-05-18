using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020009FD RID: 2557
	public class LoopListViewItem2 : MonoBehaviour
	{
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x0002F125 File Offset: 0x0002D325
		// (set) Token: 0x060041CB RID: 16843 RVA: 0x0002F12D File Offset: 0x0002D32D
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

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060041CC RID: 16844 RVA: 0x0002F136 File Offset: 0x0002D336
		// (set) Token: 0x060041CD RID: 16845 RVA: 0x0002F13E File Offset: 0x0002D33E
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

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x0002F147 File Offset: 0x0002D347
		// (set) Token: 0x060041CF RID: 16847 RVA: 0x0002F14F File Offset: 0x0002D34F
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

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x0002F158 File Offset: 0x0002D358
		// (set) Token: 0x060041D1 RID: 16849 RVA: 0x0002F160 File Offset: 0x0002D360
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

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x0002F169 File Offset: 0x0002D369
		// (set) Token: 0x060041D3 RID: 16851 RVA: 0x0002F171 File Offset: 0x0002D371
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

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x0002F17A File Offset: 0x0002D37A
		// (set) Token: 0x060041D5 RID: 16853 RVA: 0x0002F182 File Offset: 0x0002D382
		public float DistanceWithViewPortSnapCenter
		{
			get
			{
				return this.mDistanceWithViewPortSnapCenter;
			}
			set
			{
				this.mDistanceWithViewPortSnapCenter = value;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x0002F18B File Offset: 0x0002D38B
		// (set) Token: 0x060041D7 RID: 16855 RVA: 0x0002F193 File Offset: 0x0002D393
		public float StartPosOffset
		{
			get
			{
				return this.mStartPosOffset;
			}
			set
			{
				this.mStartPosOffset = value;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060041D8 RID: 16856 RVA: 0x0002F19C File Offset: 0x0002D39C
		// (set) Token: 0x060041D9 RID: 16857 RVA: 0x0002F1A4 File Offset: 0x0002D3A4
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

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x0002F1AD File Offset: 0x0002D3AD
		// (set) Token: 0x060041DB RID: 16859 RVA: 0x0002F1B5 File Offset: 0x0002D3B5
		public float Padding
		{
			get
			{
				return this.mPadding;
			}
			set
			{
				this.mPadding = value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x0002F1BE File Offset: 0x0002D3BE
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

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060041DD RID: 16861 RVA: 0x0002F1E5 File Offset: 0x0002D3E5
		// (set) Token: 0x060041DE RID: 16862 RVA: 0x0002F1ED File Offset: 0x0002D3ED
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

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060041DF RID: 16863 RVA: 0x0002F1F6 File Offset: 0x0002D3F6
		// (set) Token: 0x060041E0 RID: 16864 RVA: 0x0002F1FE File Offset: 0x0002D3FE
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

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x0002F207 File Offset: 0x0002D407
		// (set) Token: 0x060041E2 RID: 16866 RVA: 0x0002F20F File Offset: 0x0002D40F
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

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060041E3 RID: 16867 RVA: 0x0002F218 File Offset: 0x0002D418
		// (set) Token: 0x060041E4 RID: 16868 RVA: 0x0002F220 File Offset: 0x0002D420
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

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060041E5 RID: 16869 RVA: 0x0002F229 File Offset: 0x0002D429
		// (set) Token: 0x060041E6 RID: 16870 RVA: 0x0002F231 File Offset: 0x0002D431
		public LoopListView2 ParentListView
		{
			get
			{
				return this.mParentListView;
			}
			set
			{
				this.mParentListView = value;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060041E7 RID: 16871 RVA: 0x001C69D8 File Offset: 0x001C4BD8
		public float TopY
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.TopToBottom)
				{
					return this.CachedRectTransform.anchoredPosition3D.y;
				}
				if (arrangeType == ListItemArrangeType.BottomToTop)
				{
					return this.CachedRectTransform.anchoredPosition3D.y + this.CachedRectTransform.rect.height;
				}
				return 0f;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x001C6A34 File Offset: 0x001C4C34
		public float BottomY
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.TopToBottom)
				{
					return this.CachedRectTransform.anchoredPosition3D.y - this.CachedRectTransform.rect.height;
				}
				if (arrangeType == ListItemArrangeType.BottomToTop)
				{
					return this.CachedRectTransform.anchoredPosition3D.y;
				}
				return 0f;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x001C6A90 File Offset: 0x001C4C90
		public float LeftX
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.LeftToRight)
				{
					return this.CachedRectTransform.anchoredPosition3D.x;
				}
				if (arrangeType == ListItemArrangeType.RightToLeft)
				{
					return this.CachedRectTransform.anchoredPosition3D.x - this.CachedRectTransform.rect.width;
				}
				return 0f;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x001C6AEC File Offset: 0x001C4CEC
		public float RightX
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.LeftToRight)
				{
					return this.CachedRectTransform.anchoredPosition3D.x + this.CachedRectTransform.rect.width;
				}
				if (arrangeType == ListItemArrangeType.RightToLeft)
				{
					return this.CachedRectTransform.anchoredPosition3D.x;
				}
				return 0f;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060041EB RID: 16875 RVA: 0x001C6B48 File Offset: 0x001C4D48
		public float ItemSize
		{
			get
			{
				if (this.ParentListView.IsVertList)
				{
					return this.CachedRectTransform.rect.height;
				}
				return this.CachedRectTransform.rect.width;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x0002F23A File Offset: 0x0002D43A
		public float ItemSizeWithPadding
		{
			get
			{
				return this.ItemSize + this.mPadding;
			}
		}

		// Token: 0x04003A75 RID: 14965
		private int mItemIndex = -1;

		// Token: 0x04003A76 RID: 14966
		private int mItemId = -1;

		// Token: 0x04003A77 RID: 14967
		private LoopListView2 mParentListView;

		// Token: 0x04003A78 RID: 14968
		private bool mIsInitHandlerCalled;

		// Token: 0x04003A79 RID: 14969
		private string mItemPrefabName;

		// Token: 0x04003A7A RID: 14970
		private RectTransform mCachedRectTransform;

		// Token: 0x04003A7B RID: 14971
		private float mPadding;

		// Token: 0x04003A7C RID: 14972
		private float mDistanceWithViewPortSnapCenter;

		// Token: 0x04003A7D RID: 14973
		private int mItemCreatedCheckFrameCount;

		// Token: 0x04003A7E RID: 14974
		private float mStartPosOffset;

		// Token: 0x04003A7F RID: 14975
		private object mUserObjectData;

		// Token: 0x04003A80 RID: 14976
		private int mUserIntData1;

		// Token: 0x04003A81 RID: 14977
		private int mUserIntData2;

		// Token: 0x04003A82 RID: 14978
		private string mUserStringData1;

		// Token: 0x04003A83 RID: 14979
		private string mUserStringData2;
	}
}
