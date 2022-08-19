using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020006D5 RID: 1749
	public class LoopStaggeredGridViewItem : MonoBehaviour
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x00180184 File Offset: 0x0017E384
		// (set) Token: 0x06003809 RID: 14345 RVA: 0x0018018C File Offset: 0x0017E38C
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

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x00180195 File Offset: 0x0017E395
		// (set) Token: 0x0600380B RID: 14347 RVA: 0x0018019D File Offset: 0x0017E39D
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

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x001801A6 File Offset: 0x0017E3A6
		// (set) Token: 0x0600380D RID: 14349 RVA: 0x001801AE File Offset: 0x0017E3AE
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

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x001801B7 File Offset: 0x0017E3B7
		// (set) Token: 0x0600380F RID: 14351 RVA: 0x001801BF File Offset: 0x0017E3BF
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

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x001801C8 File Offset: 0x0017E3C8
		// (set) Token: 0x06003811 RID: 14353 RVA: 0x001801D0 File Offset: 0x0017E3D0
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

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06003812 RID: 14354 RVA: 0x001801D9 File Offset: 0x0017E3D9
		// (set) Token: 0x06003813 RID: 14355 RVA: 0x001801E1 File Offset: 0x0017E3E1
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

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x001801EA File Offset: 0x0017E3EA
		// (set) Token: 0x06003815 RID: 14357 RVA: 0x001801F2 File Offset: 0x0017E3F2
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

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x001801FB File Offset: 0x0017E3FB
		// (set) Token: 0x06003817 RID: 14359 RVA: 0x00180203 File Offset: 0x0017E403
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

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x0018020C File Offset: 0x0017E40C
		// (set) Token: 0x06003819 RID: 14361 RVA: 0x00180214 File Offset: 0x0017E414
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

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x0018021D File Offset: 0x0017E41D
		// (set) Token: 0x0600381B RID: 14363 RVA: 0x00180225 File Offset: 0x0017E425
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

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x0018022E File Offset: 0x0017E42E
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

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x00180255 File Offset: 0x0017E455
		// (set) Token: 0x0600381E RID: 14366 RVA: 0x0018025D File Offset: 0x0017E45D
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

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x00180266 File Offset: 0x0017E466
		// (set) Token: 0x06003820 RID: 14368 RVA: 0x0018026E File Offset: 0x0017E46E
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

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x00180277 File Offset: 0x0017E477
		// (set) Token: 0x06003822 RID: 14370 RVA: 0x0018027F File Offset: 0x0017E47F
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

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x00180288 File Offset: 0x0017E488
		// (set) Token: 0x06003824 RID: 14372 RVA: 0x00180290 File Offset: 0x0017E490
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

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06003825 RID: 14373 RVA: 0x00180299 File Offset: 0x0017E499
		// (set) Token: 0x06003826 RID: 14374 RVA: 0x001802A1 File Offset: 0x0017E4A1
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

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x001802AA File Offset: 0x0017E4AA
		// (set) Token: 0x06003828 RID: 14376 RVA: 0x001802B2 File Offset: 0x0017E4B2
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

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x001802BC File Offset: 0x0017E4BC
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

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600382A RID: 14378 RVA: 0x00180318 File Offset: 0x0017E518
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

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600382B RID: 14379 RVA: 0x00180374 File Offset: 0x0017E574
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

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600382C RID: 14380 RVA: 0x001803D0 File Offset: 0x0017E5D0
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

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600382D RID: 14381 RVA: 0x0018042C File Offset: 0x0017E62C
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

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x0018046D File Offset: 0x0017E66D
		public float ItemSizeWithPadding
		{
			get
			{
				return this.ItemSize + this.mPadding;
			}
		}

		// Token: 0x04003099 RID: 12441
		private int mItemIndex = -1;

		// Token: 0x0400309A RID: 12442
		private int mItemIndexInGroup = -1;

		// Token: 0x0400309B RID: 12443
		private int mItemId = -1;

		// Token: 0x0400309C RID: 12444
		private float mPadding;

		// Token: 0x0400309D RID: 12445
		private float mExtraPadding;

		// Token: 0x0400309E RID: 12446
		private bool mIsInitHandlerCalled;

		// Token: 0x0400309F RID: 12447
		private string mItemPrefabName;

		// Token: 0x040030A0 RID: 12448
		private RectTransform mCachedRectTransform;

		// Token: 0x040030A1 RID: 12449
		private LoopStaggeredGridView mParentListView;

		// Token: 0x040030A2 RID: 12450
		private float mDistanceWithViewPortSnapCenter;

		// Token: 0x040030A3 RID: 12451
		private int mItemCreatedCheckFrameCount;

		// Token: 0x040030A4 RID: 12452
		private float mStartPosOffset;

		// Token: 0x040030A5 RID: 12453
		private object mUserObjectData;

		// Token: 0x040030A6 RID: 12454
		private int mUserIntData1;

		// Token: 0x040030A7 RID: 12455
		private int mUserIntData2;

		// Token: 0x040030A8 RID: 12456
		private string mUserStringData1;

		// Token: 0x040030A9 RID: 12457
		private string mUserStringData2;
	}
}
