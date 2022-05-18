using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x02000A03 RID: 2563
	public class LoopStaggeredGridViewItem : MonoBehaviour
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x0002F3FC File Offset: 0x0002D5FC
		// (set) Token: 0x0600422B RID: 16939 RVA: 0x0002F404 File Offset: 0x0002D604
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

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x0002F40D File Offset: 0x0002D60D
		// (set) Token: 0x0600422D RID: 16941 RVA: 0x0002F415 File Offset: 0x0002D615
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

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x0002F41E File Offset: 0x0002D61E
		// (set) Token: 0x0600422F RID: 16943 RVA: 0x0002F426 File Offset: 0x0002D626
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

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x0002F42F File Offset: 0x0002D62F
		// (set) Token: 0x06004231 RID: 16945 RVA: 0x0002F437 File Offset: 0x0002D637
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

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x0002F440 File Offset: 0x0002D640
		// (set) Token: 0x06004233 RID: 16947 RVA: 0x0002F448 File Offset: 0x0002D648
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

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x0002F451 File Offset: 0x0002D651
		// (set) Token: 0x06004235 RID: 16949 RVA: 0x0002F459 File Offset: 0x0002D659
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

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x0002F462 File Offset: 0x0002D662
		// (set) Token: 0x06004237 RID: 16951 RVA: 0x0002F46A File Offset: 0x0002D66A
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

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x0002F473 File Offset: 0x0002D673
		// (set) Token: 0x06004239 RID: 16953 RVA: 0x0002F47B File Offset: 0x0002D67B
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

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x0002F484 File Offset: 0x0002D684
		// (set) Token: 0x0600423B RID: 16955 RVA: 0x0002F48C File Offset: 0x0002D68C
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

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x0002F495 File Offset: 0x0002D695
		// (set) Token: 0x0600423D RID: 16957 RVA: 0x0002F49D File Offset: 0x0002D69D
		public float ExtraPadding
		{
			get
			{
				return this.mExtraPadding;
			}
			set
			{
				this.mExtraPadding = value;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x0002F4A6 File Offset: 0x0002D6A6
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

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x0002F4CD File Offset: 0x0002D6CD
		// (set) Token: 0x06004240 RID: 16960 RVA: 0x0002F4D5 File Offset: 0x0002D6D5
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

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x0002F4DE File Offset: 0x0002D6DE
		// (set) Token: 0x06004242 RID: 16962 RVA: 0x0002F4E6 File Offset: 0x0002D6E6
		public int ItemIndexInGroup
		{
			get
			{
				return this.mItemIndexInGroup;
			}
			set
			{
				this.mItemIndexInGroup = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x0002F4EF File Offset: 0x0002D6EF
		// (set) Token: 0x06004244 RID: 16964 RVA: 0x0002F4F7 File Offset: 0x0002D6F7
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

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x0002F500 File Offset: 0x0002D700
		// (set) Token: 0x06004246 RID: 16966 RVA: 0x0002F508 File Offset: 0x0002D708
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

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x0002F511 File Offset: 0x0002D711
		// (set) Token: 0x06004248 RID: 16968 RVA: 0x0002F519 File Offset: 0x0002D719
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

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x0002F522 File Offset: 0x0002D722
		// (set) Token: 0x0600424A RID: 16970 RVA: 0x0002F52A File Offset: 0x0002D72A
		public LoopStaggeredGridView ParentListView
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

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x001C7DC0 File Offset: 0x001C5FC0
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

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x001C7E1C File Offset: 0x001C601C
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

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x001C7E78 File Offset: 0x001C6078
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

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600424E RID: 16974 RVA: 0x001C7ED4 File Offset: 0x001C60D4
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

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x001C7F30 File Offset: 0x001C6130
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

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x0002F533 File Offset: 0x0002D733
		public float ItemSizeWithPadding
		{
			get
			{
				return this.ItemSize + this.mPadding;
			}
		}

		// Token: 0x04003AB1 RID: 15025
		private int mItemIndex = -1;

		// Token: 0x04003AB2 RID: 15026
		private int mItemIndexInGroup = -1;

		// Token: 0x04003AB3 RID: 15027
		private int mItemId = -1;

		// Token: 0x04003AB4 RID: 15028
		private float mPadding;

		// Token: 0x04003AB5 RID: 15029
		private float mExtraPadding;

		// Token: 0x04003AB6 RID: 15030
		private bool mIsInitHandlerCalled;

		// Token: 0x04003AB7 RID: 15031
		private string mItemPrefabName;

		// Token: 0x04003AB8 RID: 15032
		private RectTransform mCachedRectTransform;

		// Token: 0x04003AB9 RID: 15033
		private LoopStaggeredGridView mParentListView;

		// Token: 0x04003ABA RID: 15034
		private float mDistanceWithViewPortSnapCenter;

		// Token: 0x04003ABB RID: 15035
		private int mItemCreatedCheckFrameCount;

		// Token: 0x04003ABC RID: 15036
		private float mStartPosOffset;

		// Token: 0x04003ABD RID: 15037
		private object mUserObjectData;

		// Token: 0x04003ABE RID: 15038
		private int mUserIntData1;

		// Token: 0x04003ABF RID: 15039
		private int mUserIntData2;

		// Token: 0x04003AC0 RID: 15040
		private string mUserStringData1;

		// Token: 0x04003AC1 RID: 15041
		private string mUserStringData2;
	}
}
